using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppEvaMovil.Interfaces.CatGenerales
{
    public interface IFicSrvImportarWebApi
    {
        Task<string> FicGetImportInventarios(int id=0);
        Task<string> FicGetImportCatalogos();
    }//INTERFACE
}//NAMESPACE
