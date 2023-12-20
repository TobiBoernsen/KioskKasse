using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KassenSystem
{
    /// <summary>
    /// Interaktionslogik für Abrechnung.xaml
    /// </summary>
    public partial class Abrechnung : Window
    {
        
        private double Gesamtkosten;
        public Abrechnung()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Öffnet ein Fenster zum Berechnen des Wechselgeldes.
        /// </summary>
        /// <param name="t_value">Gesamtkosten der Abbrechnung </param>
        public Abrechnung(double t_value)
        {
            Gesamtkosten = t_value;
            InitializeComponent();
            GesamtPreisText.Text = Gesamtkosten.ToString();
            EingezahlterBetrag.Text = "";
            Wechselgeld.Text = "";
        }

        /// <summary>
        /// Funktion die auf alle Knöpfe gelegt wurde und 
        /// die Eingaben zu verwaltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var B = sender as Button;
            string S = B.Content.ToString();
            switch (S)
            {
                case "C":
                    if (!string.IsNullOrEmpty(EingezahlterBetrag.Text))
                    {
                       EingezahlterBetrag.Text = EingezahlterBetrag.Text.Remove(EingezahlterBetrag.Text.Length - 1);
                    }
                    break;
                case "CE":
                    EingezahlterBetrag.Text = "";
                    break;
                case "0":
                    AddZero();
                    break;
                case ".":
                    AddPoint();
                    break;
                case "End":
                    CloseWindow();
                    break;
                default:
                    if (!CheckNachkommastellen(EingezahlterBetrag.Text))
                        break;
                    if (EingezahlterBetrag.Text == "0")
                        EingezahlterBetrag.Text = S;
                    else
                        EingezahlterBetrag.Text += S;
                    break;
            }
            UpdateWechselGeld();
        }

        /// <summary>
        /// Fügt korrekt eine 0 hinzu und befolgt alle regeln
        /// </summary>
        private void AddZero()
        {
            if (EingezahlterBetrag.Text.Contains(","))
            {
                EingezahlterBetrag.Text += "0";
            }
            if (string.IsNullOrEmpty( EingezahlterBetrag.Text))
            {
                EingezahlterBetrag.Text = "0";
            }
            if (!string.IsNullOrEmpty(EingezahlterBetrag.Text) &&
                EingezahlterBetrag.Text != "0")
            {
                EingezahlterBetrag.Text += "0";
            }
        }

        /// <summary>
        /// Fügt ein Komma als Trennzeichen hinzu
        /// </summary>
        private void AddPoint()
        {
            if (EingezahlterBetrag.Text.Contains(","))
                return;
            if (string.IsNullOrEmpty(EingezahlterBetrag.Text))
            {
                EingezahlterBetrag.Text = "0,";
                return;
            }
            EingezahlterBetrag.Text += ",";
        }

        /// <summary>
        /// Schließt das Fenster
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        /// <summary>
        /// Funktion die das Wechselgeld berechnet
        /// </summary>
        private void UpdateWechselGeld()
        {
            if (double.TryParse(EingezahlterBetrag.Text,out double Value))
            {
                if (Value < Gesamtkosten)
                    Wechselgeld.Text = "Eingezahlterbetrag zu klein.";
                else
                {
                    double newvalue = (Gesamtkosten - Value) * -1;
                    Wechselgeld.Text = newvalue.ToString("N2");
                }
            }
        }

        /// <summary>
        /// Verhindert das mehr als 2 Ziffern hinter ein Komma hinzugefügt werden kann
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        private bool CheckNachkommastellen(string S)
        {
            if (!S.Contains(","))
                return true;
            var index = S.IndexOf(",");
            if (index > S.Length - 3)
                return true;
            return false;
        }

    }
}
