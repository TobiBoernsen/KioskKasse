using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassenSystem
{
    /// <summary>
    /// Bewahrt alle Fenster auf sodass auf diese leichter und von allen Orten drauf zugegriffen werden kann.
    /// </summary>
    static class WindowManager
    {
        static public MainWindow MainWindow;
        static public Steuerelemente.MenuDisplay EssenWindow;
        static public Steuerelemente.MenuDisplay GetränkeWindow;
        static public Steuerelemente.MenuDisplay BeilagenWindow;
        static public Steuerelemente.MenuDisplay DessertWindow;
        static public Steuerelemente.RechnungsManager RechnungsManagerWindow;
    }
}
