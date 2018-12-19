using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AppEvaMovil.Interfaces.SQLite;
using AppEvaMovil.Droid.SQLite;

using Xamarin.Forms;

[assembly: Dependency(typeof(FicConfigSQLiteDROID))]
namespace AppEvaMovil.Droid.SQLite
{
    class FicConfigSQLiteDROID :IFicConfigSQLite
    {
        public string FicGetDataBasePath() {
            var FicPathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            var FicDirectorioDB = FicPathFile.Path;
            FicDirectorioDB = FicDirectorioDB + "/cocacolaNay/";
            string FicPathDB = Path.Combine(FicDirectorioDB, FicAppSettings.FicDataBaseName);
            return FicPathDB;
        }//TRAE LA RUTA FISICA DONDE ESTARA LA BASE DE DATOS SQLite        
    }//CLASS
}//NAMESPACE