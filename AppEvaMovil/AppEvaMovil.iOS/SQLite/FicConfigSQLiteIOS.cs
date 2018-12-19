using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AppEvaMovil.Interfaces.SQLite;
using AppEvaMovil.iOS.SQLite;

using Foundation;
using UIKit;

using Xamarin.Forms;

[assembly:Dependency(typeof(FicConfigSQLiteIOS))]
namespace AppEvaMovil.iOS.SQLite
{
    class FicConfigSQLiteIOS : IFicConfigSQLite
    {
        public string FicGetDataBasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }            
            return Path.Combine(libFolder, FicAppSettings.FicDataBaseName);
        }//TRAER LA RUTA FISICA DE IOS DONDE SE GUARDA  LA BD DE SQLITE        
    }//CLASS
}//NAMESPACE