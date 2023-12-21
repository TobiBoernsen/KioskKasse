using System.Linq;

namespace KassenSystem.Core
{
    // Definition einer Aufzählung für die Größen der Getränke.
    public enum Size
    {
        Small,
        Medium,
        Large
    }

    /// <summary>
    /// Die Klasse 'Getränk' erweitert die Klasse 'Product' um die Eigenschaft der Größe.
    /// </summary>
    public class Getränk : Product
    {
        // Deklaration von Eigenschaften für die Größe und Preise
        public Size Size; // Größe des Getränks
        double PriceSmall; // Preis für ein kleines Getränk
        double PriceLarge; // Preis für ein großes Getränk

        // Überschreibt den Preis aus der Basisklasse 'Product' und berechnet ihn basierend auf der Größe.
        public new double Price { get => CalculatePrice(); }

        // Überschreibt den Namen aus der Basisklasse 'Product' und fügt die Größeninformation hinzu.
        public new string Name { get => CreateName(); }

        private string BaseName; // Grundname des Getränks ohne Größenangabe

        // Konstruktor für das Getränk, initialisiert Basiswerte
        public Getränk(string t_name, double t_baseprice, double t_Price_Small , double t_Price_Large ) 
            : base(t_name, t_baseprice)
        {
            BaseName = t_name; // Setzt den Basisnamen
            Size = Size.Small; // Standardgröße ist 'Small'
            PriceSmall = t_Price_Small; // Setzt den Preis für ein kleines Getränk
            PriceLarge = t_Price_Large; // Setzt den Preis für ein großes Getränk
        }

        /// <summary>
        /// Berechnet den Preis basierend auf der Größe des Getränks.
        /// </summary>
        private double CalculatePrice()
        {
            switch (Size)
            {
                case Size.Small:
                    return Count * PriceSmall; // Preis für kleine Größe
                case Size.Medium:
                    return Count * BasePrice; // Preis für mittlere Größe (Standardpreis)
                case Size.Large:
                    return Count * PriceLarge; // Preis für große Größe
            }
            return Count * BasePrice; // Standardpreis, falls keine Größe zutrifft
        }

        /// <summary>
        /// Erstellt einen Namen für das Getränk, der die Größe beinhaltet.
        /// </summary>
        private string CreateName()
        {
            switch (Size)
            {
                case Size.Small:
                    return BaseName + " Small"; // Fügt "Small" zum Basisnamen hinzu
                case Size.Medium:
                    return BaseName + " Medium"; // Fügt "Medium" zum Basisnamen hinzu
                case Size.Large:
                    return BaseName + " Large"; // Fügt "Large" zum Basisnamen hinzu
            }
            return BaseName + " Medium"; // Standardfall, falls keine Größe zutrifft
        }

        /// <summary>
        /// Fügt das Getränk mit der gewählten Größe zur Rechnung hinzu.
        /// </summary>
        public new void AddToBill(Size size)
        {
            var manager = ProductManager.GetManager(); // Ruft den Produktmanager auf
            Size = size; // Setzt die gewählte Größe
            CreateName(); // Aktualisiert den Namen basierend auf der gewählten Größe
            CalculatePrice(); // Berechnet den Preis basierend auf der gewählten Größe

            // Überprüft, ob ein Produkt mit demselben Namen bereits in der Rechnung existiert
            var matches = manager.BillProducts.Where(P => P.Name == Name);
            if (matches.Count() != 0)
            {
                var first = matches.First();
                if (first != null)
                {
                    first.Count+= Count; // Erhöht die Anzahl, falls das Produkt bereits vorhanden ist
                    WindowManager.RechnungsManagerWindow.UpdateDisplay(); // Aktualisiert das Rechnungsfenster

                    return;
                }
            }
            // Erstellt ein neues Produkt und fügt es der Rechnung hinzu
            var Pro = new Product(Name, Price/Count);
            Pro.Count = Count;
            manager.BillProducts.Add(Pro);
            WindowManager.RechnungsManagerWindow.UpdateDisplay(); // Aktualisiert das Rechnungsfenster
        }
    }
}
