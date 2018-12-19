using AppEvaMovil.ViewModels.CatGenerales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEvaMovil.Views.CatGenerales
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViExportarWebApi : ContentPage
	{
		public FicViExportarWebApi ()
		{
			InitializeComponent ();
            BindingContext = App.FicVmLocator.FicVmExportarWebApi;
        }//CONSTRUCTOR

        protected async override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmExportarWebApi;
            if (FicViewModel != null)
            {
                FicViewModel.OnAppearing();
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW

    }//CLASS
}//NAMESPACE