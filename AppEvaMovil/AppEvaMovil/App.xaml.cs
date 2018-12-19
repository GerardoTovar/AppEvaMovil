using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppEvaMovil.Views;
using AppEvaMovil.Views.CatGenerales;
using AppEvaMovil.ViewModels.Base;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppEvaMovil
{
    public partial class App : Application
    {
        private static FicViewModelLocator FicLocalVmLocator;

        public static FicViewModelLocator FicVmLocator {
            get
            {
                return FicLocalVmLocator = FicLocalVmLocator ?? new FicViewModelLocator();
            }
        }

        public App()
        {
            InitializeComponent();
            //MANDAMOS NUESTRO MAESTRO DETALLE COMO NUESTRO MAIN PAGE
            MainPage = new Views.Navegacion.FicMasterPage();
            //MainPage = new FicViCatEdificiosList();
            //MainPage = new MainPage();
        }
       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
