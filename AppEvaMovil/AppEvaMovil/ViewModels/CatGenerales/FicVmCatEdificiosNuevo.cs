using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.Services.CatGenerales;
using AppEvaMovil.Interfaces.Navegacion;

using System.Text;
using AppEvaMovil.ViewModels.Base;
using System.Windows.Input;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using AppEvaMovil.Interfaces.CatGenerales;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppEvaMovil.ViewModels.CatGenerales
{
    public class FicVmCatEdificiosNuevo : FicViewModelBase
    {
        private IFicSrvNavigationCatEdificios FicLoSrvNavigation;
        private IFicSrvCatEdificiosNuevo FicLoSrvApp;

        private eva_cat_edificios _Edificios;
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

        private ICommand AddEdificio;
        public ICommand FicMetAddCommand
        {
            get { return AddEdificio = AddEdificio ?? new FicVmDelegateCommand(AddEdificioExecute); }
        }
        private void AddEdificioExecute()
        {
            Edificio.UsuarioMod = Edificio.UsuarioReg;
            Edificio.FechaUltMod = DateTime.Now;
            Edificio.FechaReg = DateTime.Now;
            if (Edificio.Activo == "True") {
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
            FicLoSrvApp.FicMetNuevoListCatEdificios(Edificio);
            FicLoSrvNavigation.FicMetNavigateTo<FicVmCatEdificiosList>(null);
        }

        public FicVmCatEdificiosNuevo(IFicSrvNavigationCatEdificios FicPaSrvNavigation, IFicSrvCatEdificiosNuevo FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            _Edificios = new eva_cat_edificios();
        }

    }
}
