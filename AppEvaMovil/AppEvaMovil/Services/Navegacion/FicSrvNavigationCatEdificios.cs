using AppEvaMovil.Interfaces.Navegacion;
using AppEvaMovil.ViewModels.CatGenerales;
//using AppEvaMovil.ViewModels.Inventarios;
using AppEvaMovil.Views.CatGenerales;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppEvaMovil.Services.Navegacion
{
    public class FicSrvNavigationCatEdificios : IFicSrvNavigationCatEdificios
    {
        private IDictionary<Type, Type> FicViewModelRouting = new Dictionary<Type, Type>()
        { 
            //AQUI SE HACE UNA UNION ENTRE LA VM Y VI DE CADA VIEW DE LA APP
            { typeof(FicVmCatEdificiosNuevo),typeof(FicViCatEdificiosNuevo) },
            { typeof(FicVmCatEdificiosList),typeof(FicViCatEdificiosList) },
            { typeof(FicVmCatEdificiosUpdate),typeof(FicViCatEdificiosUpdate) },

            //{ typeof(FicVmInventarioConteoList),typeof(FicViInventarioConteoList) },
            //{ typeof(FicVmInventarioConteosItem),typeof(FicViInventarioConteosItem) },
            //{ typeof(FicVmInventarioAcumuladoList),typeof(FicViInventarioAcumuladoList)},
            {typeof(FicVmImportarWebApi), typeof(FicViImportarWebApi)},
            {typeof(FicVmExportarWebApi), typeof(FicViExportarWebApi)}
        };

        #region METODOS DE IMPLEMENTACION DE LA INTERFACE -> IFicSrvNavigationInventario
        public void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null)
        {
            Type FicPageType = FicViewModelRouting[typeof(FicTDestinationViewModel)];
            var FicPage = Activator.CreateInstance(FicPageType, FicNavigationContext) as Page;

            if (FicPage != null)
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                mdp.Detail.Navigation.PushAsync(FicPage);
            }
        }

        public void FicMetNavigateTo(Type FicDestinationType, object FicNavigationContext = null)
        {
            Type FicPageType = FicViewModelRouting[FicDestinationType];
            var FicPage = Activator.CreateInstance(FicPageType, FicNavigationContext) as Page;

            if (FicPage != null)
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                mdp.Detail.Navigation.PushAsync(FicPage);
            }
        }

        public void FicMetNavigateBack()
        {
            Application.Current.MainPage = new NavigationPage();
        }
        #endregion

    }//CLASS
}//NAMESPACE
