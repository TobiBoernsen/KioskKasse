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
    public enum Menus
    {
        e_Beilagen,
        e_Desserts,
        e_Getränke,
        e_Essen
    }
    /// <summary>
    /// Interaktionslogik für MenuDisplay.xaml
    /// </summary>
    public partial class MenuDisplay : UserControl
    {
        public Menus Menus { get; set; }

        public MenuDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Erstellt für alle Übergebenen Produkte Knöpfe
        /// </summary>
        /// <param name="products"></param>
        public void Init(List<Core.Product> products)
        {
            int count = 1;
            int i = 0;
            int j = 0;
            foreach (var item in products)
            {
                Button button = new Button();
                button.Content = item.Name;
                button.Name = "button" + count.ToString();
                button.Click += item.SetAsSelectedProduct;
                Grid.SetColumn(button, j % 5);
                Grid.SetRow(button, i);
                DGrid.Children.Add(button);
                j++;
                if (j % 5 == 0)
                {
                    i++;
                }
                count++;
            }

        }
    }
}
