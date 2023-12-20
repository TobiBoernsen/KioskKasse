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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KassenSystem.Steuerelemente
{
    /// <summary>
    /// Interaktionslogik für RechnungsManager.xaml
    /// </summary>
    public partial class RechnungsManager : UserControl
    {
        public RechnungsManager()
        {
            InitializeComponent();
            var Man = Core.ProductManager.GetManager();
            ProductList.ItemsSource = Man.BillProducts;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Erhöht die Anzahl des ausgewählten Produktes um 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncreaseCount(object sender, RoutedEventArgs e)
        {
            var Sel = ProductList.SelectedItem;
            if (Sel != null)
            {
                var P = Sel as Core.Product;
                if (P != null)
                {
                    P.Count++;
                }
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Updated die liste und berechnet die Kosten neu
        /// </summary>
        public void UpdateDisplay() { 

            ProductList.Items.Refresh();
            double Kosten = 0.0;
            foreach (var Item in ProductList.Items)
            {
                var Product = Item as Core.Product;
                Kosten += Product.Price;
            }
            GesamtKosten.Text = Kosten.ToString();
        }

        /// <summary>
        /// Veringert die Anzahl des ausgewählten produktes,
        /// wenn die Anzahl auf 0 sinkt wird dieses gelöscht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecreaseCount(object sender, RoutedEventArgs e)
        {

            var Sel = ProductList.SelectedItem;
            if (Sel != null)
            {
                var P = Sel as Core.Product;
                
                if (P != null)
                {
                    if (P.Count == 1)
                    {
                        RemoveProduct(P);
                        return;
                    }
                    P.Count--;
                }
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Löscht das ausgewählte Produkt von der Rechnung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var Sel = ProductList.SelectedItem;
            if (Sel != null)
            {
                var P = Sel as Core.Product;
                if (P != null)
                {
                    RemoveProduct(P);
                }
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Löscht das übergebene Produkt von der Liste
        /// </summary>
        /// <param name="product"></param>
        private void RemoveProduct(Core.Product product)
        {
            Core.ProductManager.GetManager().BillProducts.Remove(product);
            UpdateDisplay();
        }

        /// <summary>
        /// Öffnet den Abrechnungs dialog und resettet anschließend die Abrechnungsliste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double Kosten = 0.0;
            foreach (var Item in ProductList.Items)
            {
                var Product = Item as Core.Product;
                Kosten += Product.Price;
            }
            Abrechnung abrechnung = new Abrechnung(Kosten);
            abrechnung.ShowDialog();
            var Man = Core.ProductManager.GetManager();
            Man.BillProducts.Clear();
            ProductList.ItemsSource = Man.BillProducts;
            UpdateDisplay();
            // TODO: Rechnung mit passender Steuer in Datenbank schreiben.
        }
    }
}
