using System.Collections.Generic;

namespace KassenSystem.Core
{
    /// <summary>
    /// Klasse die das laden der Produkte aus der Datenbank übernimmt
    /// </summary>
    public sealed class ProductManager
    {

        private static ProductManager Instance = null;

        /// <summary>
        /// Funktion um auf den Manager zuzugreifen
        /// Productmanager ist ein Singleton somit kann immer nur eine Instanz davon existieren
        /// und von allen Code stellen daruaf zugegriffen werden.
        /// </summary>
        /// <returns></returns>
        public static ProductManager GetManager()
        {
            if (Instance == null)
                Instance = new ProductManager();
            return Instance;
        }

        public List<Product> BillProducts = new List<Product>();

        /// <summary>
        /// Klasse die alle Produkte in Listen verwaltet und es Ermöglicht sie aus der Datenbank zu laden
        /// </summary>
        ProductManager() 
        {
            Essen = new List<Product>();
            Beilagen = new List<Product>();
            Getränke = new List<Product>();
            Dessert = new List<Product>();

            PopulateGetränkeProducts();
            PopulateEssensProducts();
            PopulateDessetsProducts();
            PopulateBeilagenProducts();

        }

        public List<Product> Essen { get; private set; }
        public List<Product> Beilagen { get; private set; }
        public List<Product> Getränke { get; private set; }
        public List<Product> Dessert { get; private set; }

        /// <summary>
        /// Lädt Produkte aus der Datenbank und Spechert diese in einer List<>
        /// </summary>
        void PopulateEssensProducts()
        {
            //TODO : Datanbank anbindung hinzufügen und Produkte Laden
            for (int i = 0; i < 25; i++)
            {
                Essen.Add(new Product("Essen#" + i.ToString(), 20));
            }
        }

        /// <summary>
        /// Lädt Produkte aus der Datenbank und Spechert diese in einer List<>
        /// </summary>
        void PopulateGetränkeProducts()
        {
            //TODO : Datanbank anbindung hinzufügen und Produkte Laden
            for (int i = 0; i < 25; i++)
            {
                Getränke.Add(new Getränk("Getränke#" + i.ToString(), 20.50 , 10.99 ,30.11));
            }

        }

        /// <summary>
        /// Lädt Produkte aus der Datenbank und Spechert diese in einer List<>
        /// </summary>
        void PopulateBeilagenProducts()
        {
            //TODO : Datanbank anbindung hinzufügen und Produkte Laden
            for (int i = 0; i < 25; i++)
            {
                Beilagen.Add(new Product("Beilagen#" + i.ToString(), 20));
            }
        }

        /// <summary>
        /// Lädt Produkte aus der Datenbank und Spechert diese in einer List<>
        /// </summary>
        void PopulateDessetsProducts()
        {
            //TODO : Datanbank anbindung hinzufügen und Produkte Laden
            for (int i = 0; i < 25; i++)
            {
                Dessert.Add(new Product("Dessert#" + i.ToString(), 20));
            }
        }


    }
}
