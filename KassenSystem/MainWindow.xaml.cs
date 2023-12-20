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

namespace KassenSystem
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core.Size Size = Core.Size.Medium;
        public Core.Product SelectedProduct { get; private set; }

        /// <summary>
        /// Fügt das Übergebene Produkt zur Liste hinzu
        /// Und unterscheidet dabei zwischen Getränken und anderen Produkten
        /// </summary>
        /// <param name="t_pro"></param>
        public void AddProductToBill ( Core.Product t_pro)
        {
            if (t_pro != null)
            {
                SelectedProduct = t_pro;
                CurrentProductText.Text = SelectedProduct.Name;
                var GProduct = SelectedProduct as Core.Getränk;
                if (GProduct != null)
                {
                    CurrentProductText.Text = GProduct.Name;
                    GProduct.AddToBill(Size);
                    SizeSmall.Visibility = Visibility.Visible;
                    SizeMedium.Visibility = Visibility.Visible;
                    SizeLarge.Visibility = Visibility.Visible;
                }
                else
                {
                    t_pro.AddToBill();
                    SizeSmall.Visibility = Visibility.Hidden;
                    SizeMedium.Visibility = Visibility.Hidden;
                    SizeLarge.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                CurrentProductText.Text = "";
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            Essen.Menus = Steuerelemente.Menus.e_Essen;
            Getränke.Menus = Steuerelemente.Menus.e_Getränke;
            Dessert.Menus = Steuerelemente.Menus.e_Desserts;
            Beilagen.Menus = Steuerelemente.Menus.e_Beilagen;

            // Blendet alle Fenster aus bis auf das Essens Fenster
            Essen.Visibility = Visibility.Visible;
            Getränke.Visibility = Visibility.Hidden;
            Dessert.Visibility = Visibility.Hidden;
            Beilagen.Visibility = Visibility.Hidden;

            // Fügt die verschiedenen Fenster zum window manager hinzu sodass diese besser verwaltete werden können
            WindowManager.MainWindow = this;
            WindowManager.EssenWindow = Essen;
            WindowManager.GetränkeWindow = Getränke;
            WindowManager.DessertWindow = Dessert;
            WindowManager.BeilagenWindow = Beilagen;
            WindowManager.RechnungsManagerWindow = m_RechnungsManager;

            // An dieser stelle werden die Produkte in die verschiedenen Fenster geladen und Knöpfe dynamisch erstellt.
            Essen.Init(Core.ProductManager.GetManager().Essen);
            Getränke.Init(Core.ProductManager.GetManager().Getränke);
            Dessert.Init(Core.ProductManager.GetManager().Dessert);
            Beilagen.Init(Core.ProductManager.GetManager().Beilagen);

            // Sichtbarkeit für die Größen auswahl der Getränke ausgeblenden
            SizeSmall.Visibility = Visibility.Hidden;
            SizeMedium.Visibility = Visibility.Hidden;
            SizeLarge.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Verwaltet die wechsel zwischen den einzelnen menüs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuToggle(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            string ButtonContent = B.Content.ToString();
            // Sichtbarkeiten zurücksetzte => Unsichtbar
            Essen.Visibility = Visibility.Hidden;
            Getränke.Visibility = Visibility.Hidden;
            Dessert.Visibility = Visibility.Hidden;
            Beilagen.Visibility = Visibility.Hidden;
            switch (ButtonContent)
            {
                case "Beilagen":
                    Beilagen.Visibility = Visibility.Visible;
                    break;
                case "Getränke":
                    {
                        // Getränke Fenster einblenden und die sichtbarkeit  der größen auswahl setzten
                        Getränke.Visibility = Visibility.Visible;
                        SizeSmall.Visibility = Visibility.Visible;
                        SizeMedium.Visibility = Visibility.Visible;
                        SizeLarge.Visibility = Visibility.Visible;
                        switch (Size)
                        {
                            case Core.Size.Small:
                                Size = Core.Size.Small;
                                SizeSmall.IsChecked = true;
                                SizeMedium.IsChecked = false;
                                SizeLarge.IsChecked = false;
                                break;
                            case Core.Size.Medium:
                                Size = Core.Size.Medium;
                                SizeSmall.IsChecked = false;
                                SizeMedium.IsChecked = true;
                                SizeLarge.IsChecked = false;
                                break;
                            case Core.Size.Large:
                                Size = Core.Size.Large;
                                SizeSmall.IsChecked = false;
                                SizeMedium.IsChecked = false;
                                SizeLarge.IsChecked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "Essen":
                    Essen.Visibility = Visibility.Visible;
                    break;
                case "Dessert":
                    Dessert.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            AddProductToBill(null);
        }

        

        /// <summary>
        /// Stellt die Größe des ausgewählten Produktes auf Klein
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSizeSmall(object sender, RoutedEventArgs e)
        {
            var GProduct = SelectedProduct as Core.Getränk;
            if (GProduct != null)
            {
                GProduct.Size = Core.Size.Small;
                CurrentProductText.Text = GProduct.Name;
            }
        }

        /// <summary>
        /// Stellt die Größe des ausgewählten Produktes auf Mittel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSizeMedium(object sender, RoutedEventArgs e)
        {
            var GProduct = SelectedProduct as Core.Getränk;
            if (GProduct != null)
            {
                GProduct.Size = Core.Size.Medium;
                CurrentProductText.Text = GProduct.Name;
            }
        }

        /// <summary>
        /// Stellt die Größe des ausgewählten Produktes auf Groß
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSizeLarge(object sender, RoutedEventArgs e)
        {
            var GProduct = SelectedProduct as Core.Getränk;
            if (GProduct != null)
            {
                GProduct.Size = Core.Size.Large;
                CurrentProductText.Text = GProduct.Name;
            }
        }

        /// <summary>
        /// Regelt die Auswahl zwischen den verschiedenen Getränkegrößen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeSmall_Checked(object sender, RoutedEventArgs e)
        {
            Size = Core.Size.Small;
            SizeSmall.IsChecked = true;
            SizeMedium.IsChecked = false;
            SizeLarge.IsChecked = false;
        }

        /// <summary>
        /// Regelt die Auswahl zwischen den verschiedenen Getränkegrößen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeMedium_Checked(object sender, RoutedEventArgs e)
        {
            Size = Core.Size.Medium;
            SizeSmall.IsChecked = false;
            SizeMedium.IsChecked = true;
            SizeLarge.IsChecked = false;
        }

        /// <summary>
        /// Regelt die Auswahl zwischen den verschiedenen Getränkegrößen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeLarge_Checked(object sender, RoutedEventArgs e)
        {
            Size = Core.Size.Large;
            SizeSmall.IsChecked = false;
            SizeMedium.IsChecked = false;
            SizeLarge.IsChecked = true;
        }
    }
}
