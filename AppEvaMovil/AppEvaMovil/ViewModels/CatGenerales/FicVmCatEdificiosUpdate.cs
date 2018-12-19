using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.ViewModels.Base;
using AppEvaMovil.Models.Asistencia;
using System.Windows.Input;
using AppEvaMovil.Interfaces.Navegacion;
using AppEvaMovil.Interfaces.CatGenerales;
using AppEvaMovil.Services.CatGenerales;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using System.Collections.ObjectModel;

namespace AppEvaMovil.ViewModels.CatGenerales
{
    public class FicVmCatEdificiosUpdate : FicViewModelBase
    {
        private IFicSrvNavigationCatEdificios FicLoSrvNavigation;
        private IFicSrvCatEdificiosUpdate FicLoSrvApp;
        public eva_cat_edificios _Edificios;
        public eva_cat_edificios Edificio
        {
            get { return _Edificios; }
            set
            {
                _Edificios = value;
                RaisePropertyChanged();
            }
        }

        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }
        private void BackNavgExecute()
        {
            FicLoSrvNavigation.FicMetNavigateBack();
        }

        private ICommand UpdateEdificio;
        public ICommand FicMetUpdateCommand
        {
            get { return UpdateEdificio = UpdateEdificio ?? new FicVmDelegateCommand(UpdateEdificioExecute); }
        }
        private void UpdateEdificioExecute()
        {
            Edificio.FechaUltMod = DateTime.Now;
            if (Edificio.Activo == "True")
            {
                Edificio.Activo = "S";
            };
            if (Edificio.Activo == null)
            {
                Edificio.Activo = "N";
            };
            if (Edificio.Borrado == "True")
            {
                Edificio.Borrado = "S";
            };
            if (Edificio.Borrado == null)
            {
                Edificio.Borrado = "N";
            };
            FicLoSrvApp.FicMetUpdateEdificio(Edificio);
            FicLoSrvNavigation.FicMetNavigateTo<FicVmCatEdificiosList>(null);
        }

        public FicVmCatEdificiosUpdate(IFicSrvNavigationCatEdificios FicPaSrvNavigation, IFicSrvCatEdificiosUpdate FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
        }



        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            Edificio = new eva_cat_edificios();
        }
        public void llenado(eva_cat_edificios obj)
        {
            Edificio = obj;
        }


    }
}
