using System;
using System.Collections.Generic;
using System.Text;
using AppEvaMovil.Models.Asistencia;
using System.Threading.Tasks;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using AppEvaMovil.Data;
namespace AppEvaMovil.Interfaces.CatGenerales
{
    public interface IFicSrvCatEdificiosUpdate
    {
        Task FicMetUpdateEdificio(eva_cat_edificios ficPa_eva_Cat_Edificios);
    }
}
