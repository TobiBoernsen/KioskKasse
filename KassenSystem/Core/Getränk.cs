using System.Linq;

namespace KassenSystem.Core
{
    public enum Size
    {
        Small,
        Medium,
        Large
    }

    /// <summary>
    /// Getränk erbt von Product und fügt Größen Optionen hinzu.
    /// </summary>
    public class Getränk : Product
    {
        public Size Size;
        double PriceSmall;
        double PriceLarge;
        public new double Price { get => CalculatePrice(); }

        public new string Name { get => CreateName(); }

        private string BaseName;
        public Getränk(string t_name, double t_baseprice, double t_Price_Small , double t_Price_Large ) : base(t_name, t_baseprice)
        {
            BaseName = t_name;
            Size = Size.Small;
            PriceSmall = t_Price_Small;
            PriceLarge = t_Price_Large;
        }

        /// <summary>
        /// Bestimmt den Preis anhand der Größe
        /// </summary>
        /// <returns></returns>
        private double CalculatePrice()
        {
            switch (Size)
            {
                case Size.Small:
                    return Count * PriceSmall;
                case Size.Medium:
                    return Count * BasePrice;
                case Size.Large:
                    return Count * PriceLarge;
            }
            return Count * BasePrice;
        }

        /// <summary>
        /// Erstellt einen namen mitm dem passenden suffix
        /// </summary>
        /// <returns></returns>
        private string CreateName()
        {
            switch (Size)
            {
                case Size.Small:
                    return BaseName + " Small";
                case Size.Medium:
                    return BaseName + " Medium";
                case Size.Large:
                    return BaseName + " Large";
            }
            return BaseName + " Medium";
        }

        /// <summary>
        /// Fügt das Getränk zur rechnung hinzu.
        /// </summary>
        public new void AddToBill(Size size)
        {
            var manager = ProductManager.GetManager();
            Size = size;
            CreateName();
            CalculatePrice();
            var matches = manager.BillProducts.Where(P => P.Name == Name);
            if (matches.Count() != 0)
            {
                var first = matches.First();
                if (first != null)
                {
                    first.Count+= Count;
                    WindowManager.RechnungsManagerWindow.UpdateDisplay();

                    return;
                }
            }
            var Pro = new Product(Name, Price/Count);
            Pro.Count = Count;
            manager.BillProducts.Add(Pro);
            WindowManager.RechnungsManagerWindow.UpdateDisplay();

        }
    }
}
