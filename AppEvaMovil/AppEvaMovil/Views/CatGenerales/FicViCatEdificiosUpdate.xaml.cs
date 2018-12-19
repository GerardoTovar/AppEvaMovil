using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppEvaMovil.Models.Asistencia;
using AppEvaMovil.ViewModels.CatGenerales;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Views.CatGenerales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatEdificiosUpdate : ContentPage
    {
        private eva_cat_edificios FicLoParameter;
        public FicViCatEdificiosUpdate()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatEdificiosUpdate;
        }
        public FicViCatEdificiosUpdate(eva_cat_edificios FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            //Prior.Value = (FicParameter as eva_cat_edificios).Prioridad;
            BindingContext = App.FicVmLocator.FicVmCatEdificiosUpdate;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            (BindingContext as FicVmCatEdificiosUpdate).Edificio.Prioridad = Int16.Parse(e.Value.ToString());
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmCatEdificiosUpdate;
            if (viewModel != null)
            {
                viewModel.OnAppearing(FicLoParameter);
                viewModel.llenado(FicLoParameter);
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmCatEdificiosUpdate;
            if (viewModel != null)
            {
                viewModel.OnDisappearing();
            }
        }

    }
}