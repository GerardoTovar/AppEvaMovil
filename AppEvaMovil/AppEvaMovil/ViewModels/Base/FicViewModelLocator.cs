using AppEvaMovil.Interfaces.CatGenerales;
using AppEvaMovil.Interfaces.Navegacion;
using AppEvaMovil.Services.CatGenerales;
using AppEvaMovil.Services.Navegacion;

using AppEvaMovil.Views.CatGenerales;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.ViewModels.CatGenerales;




namespace AppEvaMovil.ViewModels.Base
{
    public class FicViewModelLocator
    {
        private static IContainer FicIContainer;

        public FicViewModelLocator()
        {
            //FIC: ContainerBuilder es una clase de la libreria de Autofac para poder ejecutar la interfaz en las diferentes plataformas 
            var FicContainerBuilder = new ContainerBuilder();

            //-------------------------------- VIEW MODELS ------------------------------------------------------
            //FIC: se procede a registrar las ViewModels para que se puedan mandar llamar en cualquier plataforma
            //---------------------------------------------------------------------------------------------------

            FicContainerBuilder.RegisterType<FicVmCatEdificiosList>();
            FicContainerBuilder.RegisterType<FicVmCatEdificiosNuevo>();
            FicContainerBuilder.RegisterType<FicVmCatEdificiosUpdate>();
            FicContainerBuilder.RegisterType<FicVmExportarWebApi>();
            FicContainerBuilder.RegisterType<FicVmImportarWebApi>();
            //FicContainerBuilder.RegisterType<FicVmInventarioConteoList>();
            //FicContainerBuilder.RegisterType<FicVmInventarioConteosItem>();
            //FicContainerBuilder.RegisterType<FicVmInventarioAcumuladoList>();
            ////------------------------- INTERFACE SERVICES OF THE VIEW MODELS -----------------------------------
            ////FIC: se procede a registrar la interface con la que se comunican las ViewModels con los Servicios 
            ////para poder ejecutar las tareas (metodos o funciones, etc) del servicio en cuestion.
            ////---------------------------------------------------------------------------------------------------///

            FicContainerBuilder.RegisterType<FicSrvNavigationCatEdificios>().As<IFicSrvNavigationCatEdificios>().SingleInstance();

            FicContainerBuilder.RegisterType<FicSrvCatEdificiosNuevo>().As<IFicSrvCatEdificiosNuevo>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatEdificiosList>().As<IFicSrvCatEdificiosList>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatEdificiosUpdate>().As<IFicSrvCatEdificiosUpdate>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvExportarWebApi>().As<IFicSrvExportarWebApi>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvImportarWebApi>().As<IFicSrvImportarWebApi>().SingleInstance();
            //FicContainerBuilder.RegisterType<FicSrvInventariosConteosItem>().As<IFicSrvInventariosConteosItem>().SingleInstance();
            //FicContainerBuilder.RegisterType<FicSrvInventariosConteoList>().As<IFicSrvInventariosConteoList>().SingleInstance();
            //FicContainerBuilder.RegisterType<FicSrvInventarioAcumuladoList>().As<IFicSrvInventarioAcumuladoList>().SingleInstance();

            //FIC: se asigna o se libera el contenedor
            //-------------------------------------------
            if (FicIContainer != null) FicIContainer.Dispose();

            FicIContainer = FicContainerBuilder.Build();
        }//CONSTRUCTOR

        //-------------------- CONTROL DE INVENTARIOS ------------------------
        //FIC: se manda llamar desde el backend de la View de List
        public FicVmCatEdificiosList FicVmCatEdificiosList
        {
            get { return FicIContainer.Resolve<FicVmCatEdificiosList>(); }

        }
        public FicVmCatEdificiosNuevo FicVmCatEdificiosNuevo
        {
            get { return FicIContainer.Resolve<FicVmCatEdificiosNuevo>(); }
        }
        public FicVmCatEdificiosUpdate FicVmCatEdificiosUpdate
        {
            get { return FicIContainer.Resolve<FicVmCatEdificiosUpdate>(); }
        }
        public FicVmImportarWebApi FicVmImportarWebApi
        {
            get { return FicIContainer.Resolve<FicVmImportarWebApi>(); }
        }

        public FicVmExportarWebApi FicVmExportarWebApi
        {
            get { return FicIContainer.Resolve<FicVmExportarWebApi>(); }
        }
        //public FicVmInventarioConteoList FicVmInventarioConteoList
        //{                                
        //    get { return FicIContainer.Resolve<FicVmInventarioConteoList>(); }
        //}

        //public FicVmInventarioConteosItem FicVmInventarioConteosItem
        //{
        //    get { return FicIContainer.Resolve<FicVmInventarioConteosItem>(); }
        //}

        //public FicVmInventarioAcumuladoList FicVmInventarioAcumuladoList
        //{
        //    get { return FicIContainer.Resolve<FicVmInventarioAcumuladoList>(); }
        //}

    }//CLASS
}//NAMESPACE
