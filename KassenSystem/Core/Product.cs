using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace KassenSystem.Core
{
    /// <summary>
    /// Object welches ein Produkt darstellt
    /// </summary>
    public class Product
    {
        public string Name { get; }
        public double Price { get => Count * BasePrice; }
        public int Count { get; set; }
        protected double BasePrice;

        public Product(string t_name, double t_baseprice)
        {
            Name = t_name;
            Count = 1;
            BasePrice = t_baseprice;
        }

        /// <summary>
        /// Setzt das Produkt als ausgewähltes Produkt im MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetAsSelectedProduct(object sender ,System.EventArgs e) {
            WindowManager.MainWindow.AddProductToBill(this);
        }

        /// <summary>
        /// Fügt das Produkt zu Rechnung hinzu
        /// </summary>
        public void AddToBill()
        {
            var manager = ProductManager.GetManager();
            var matches = manager.BillProducts.Where(P => P.Name == Name);
            if (matches.Count() != 0)
            {
                var first = matches.First();
                if (first != null)
                {
                    first.Count+=Count;
                    WindowManager.RechnungsManagerWindow.UpdateDisplay();

                    return;
                }
            }
            manager.BillProducts.Add(this);
            WindowManager.RechnungsManagerWindow.UpdateDisplay();
            
        }
    }
}
