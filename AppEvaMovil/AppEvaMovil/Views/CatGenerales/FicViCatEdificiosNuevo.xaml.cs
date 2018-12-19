using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppEvaMovil.ViewModels.CatGenerales;

namespace AppEvaMovil.Views.CatGenerales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatEdificiosNuevo : ContentPage
    {
        private object FicLoParameter;
        public FicViCatEdificiosNuevo()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatEdificiosNuevo;
        }
        public FicViCatEdificiosNuevo(object FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmCatEdificiosNuevo;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
           (BindingContext as FicVmCatEdificiosNuevo).Edificio.Prioridad = Int16.Parse(e.Value.ToString());
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmCatEdificiosNuevo;
            if (viewModel != null)
            {
              viewModel.OnAppearing(FicLoParameter);
            }

        }


        protected override void OnDisappearing()
        {
           var viewModel = BindingContext as FicVmCatEdificiosNuevo;
          if (viewModel != null) viewModel.OnDisappearing();
        }
        protected async void FicMetNavigateBack(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage();
        }

    }
}