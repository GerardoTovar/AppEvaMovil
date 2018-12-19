using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.Models.Asistencia;
using System.Threading.Tasks;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using AppEvaMovil.Data;
namespace AppEvaMovil.Interfaces.CatGenerales
{
    public interface IFicSrvCatEdificiosNuevo
    {
        Task FicMetNuevoListCatEdificios(eva_cat_edificios item);// CREATE
    }
}
