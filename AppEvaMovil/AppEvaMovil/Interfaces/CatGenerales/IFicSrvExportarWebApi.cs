using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppEvaMovil.Interfaces.CatGenerales
{
    public interface IFicSrvExportarWebApi
    {
        Task<string> FicPostExportInventarios();
    }//INTERFACE
}//NAMESPACE
