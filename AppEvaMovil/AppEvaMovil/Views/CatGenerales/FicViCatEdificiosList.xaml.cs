using AppEvaMovil.ViewModels.CatGenerales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppEvaMovil.Helpers;
using AppEvaMovil.Views.CatGenerales;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppEvaMovil.Data;
using AppEvaMovil.Interfaces.SQLite;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Views.CatGenerales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatEdificiosList : ContentPage
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoDBContext;
        public FicViCatEdificiosList()
        {
            InitializeComponent();
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            BindingContext = App.FicVmLocator.FicVmCatEdificiosList;
        }//CONSTRUCTOR

        public FicViCatEdificiosList(object FicNavigationContext)
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatEdificiosList;
        }//CONSTRUCTOR

        //Command="{Binding FicMetDeleteCommand}"
        protected async void FicMetDeleteCommand(object sender, EventArgs e)
        {
            var context = BindingContext as FicVmCatEdificiosList;
            if (context._SfDataGrid_SelectItem_Edificio == null)
            {
                await DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN EDIFICIO", "OK");
                return;
            }
            bool conf = await DisplayAlert("Cuidado", "¿Desea eliminar este elemento?", "Sí", "No");
            if (conf)
            {
                await FicMetRemoveEdificio(context._SfDataGrid_SelectItem_Edificio);
                context.FicSfDataGrid_ItemSource_CatEdificios.Remove(context._SfDataGrid_SelectItem_Edificio);
            }

           dataGrid.View.Refresh();
        }

        public async Task FicMetRemoveEdificio(eva_cat_edificios item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                FicLoDBContext.SaveChanges();
            }
        }

        protected async override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmCatEdificiosList;
            if (FicViewModel != null)
            {
                FicViewModel.OnAppearing();
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE
    }//CLASSE

}//NAMESPACE