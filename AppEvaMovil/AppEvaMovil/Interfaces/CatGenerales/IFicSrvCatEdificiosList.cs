using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.Models.Asistencia;
using System.Threading.Tasks;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using AppEvaMovil.Data;
namespace AppEvaMovil.Interfaces.CatGenerales
{
    public interface IFicSrvCatEdificiosList
    {
        Task<IEnumerable<eva_cat_edificios>> FicMetGetListCatEdificios();//READ
    }
}
