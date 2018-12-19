using AppEvaMovil.Interfaces.CatGenerales;
using AppEvaMovil.Interfaces.Navegacion;
using AppEvaMovil.Models.Asistencia;
using AppEvaMovil.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.ViewModels.CatGenerales
{
    public class FicVmCatEdificiosList : INotifyPropertyChanged
    {
        public ObservableCollection<eva_cat_edificios> _FicSfDataGrid_ItemSource_CatEdificios;
        public eva_cat_edificios _FicSfDataGrid_SelectItem_CatEdificios;
        private ICommand _FicMetAddEdificioICommand, _FicMetAcumuladosICommand;
        private IFicSrvNavigationCatEdificios IFicSrvNavigationCatEdificios;
        private IFicSrvCatEdificiosList IFicSrvCatEdificiosList;


        public FicVmCatEdificiosList(IFicSrvNavigationCatEdificios IFicSrvNavigationCatEdificios, IFicSrvCatEdificiosList IFicSrvCatEdificiosList)
        {
            this.IFicSrvNavigationCatEdificios = IFicSrvNavigationCatEdificios; this.IFicSrvCatEdificiosList = IFicSrvCatEdificiosList;
            _FicSfDataGrid_ItemSource_CatEdificios = new ObservableCollection<eva_cat_edificios>();

        }//CONSTRUCTOR

        public ObservableCollection<eva_cat_edificios> FicSfDataGrid_ItemSource_CatEdificios
        {  
            get
            {
                return _FicSfDataGrid_ItemSource_CatEdificios;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL GRID DE LA VIEW
        public eva_cat_edificios _SfDataGrid_SelectItem_Edificio;

        public eva_cat_edificios FicSfDataGrid_SelectItem_CatEdificios
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL GRID DE LA VIEW

        public eva_cat_edificios SfDataGrid_SelectItem_Edificio
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }


        private ICommand UpdateEdificio;
        public ICommand FicMetUpdateCommand
        {

            get { return UpdateEdificio = UpdateEdificio ?? new FicVmDelegateCommand(UpdateEdificioExecute); }
        }
        private void UpdateEdificioExecute()
        {
            if (_SfDataGrid_SelectItem_Edificio == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN EDIFICIO", "OK");
                return;
            }
            else
            {
                IFicSrvNavigationCatEdificios.FicMetNavigateTo<FicVmCatEdificiosUpdate>(_SfDataGrid_SelectItem_Edificio);
            }
           
        }

        private ICommand NewEdificio;
        public ICommand FicMetNewCommand
        {
            get { return NewEdificio = NewEdificio ?? new FicVmDelegateCommand(NewEdificioExecute); }
        }
        private void NewEdificioExecute()
        {

            IFicSrvNavigationCatEdificios.FicMetNavigateTo<FicVmCatEdificiosNuevo>(null);
        }


        public async void OnAppearing()
        {
            try
                {
                var source_local_inv = await IFicSrvCatEdificiosList.FicMetGetListCatEdificios();
                if (source_local_inv != null)
                {
                    foreach (eva_cat_edificios inv in source_local_inv)
                    {
                        _FicSfDataGrid_ItemSource_CatEdificios.Add(inv);
                     }
                }//LLENAR EL GRID
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//SOBRE CARGA AL METODO OnAppearing() DE LA VIEW 
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
            public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        #endregion
    }//CLASS 
}//NAMESPACE

