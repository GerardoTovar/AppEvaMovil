using AppEvaMovil.Data;
using AppEvaMovil.Helpers;
using AppEvaMovil.Interfaces.CatGenerales;
using AppEvaMovil.Interfaces.SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Services.CatGenerales
{
    public class FicSrvCatEdificiosNuevo : IFicSrvCatEdificiosNuevo
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoBDContext;

        public FicSrvCatEdificiosNuevo()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetNuevoListCatEdificios(eva_cat_edificios item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.eva_cat_edificios.Add(item);
                FicLoBDContext.SaveChanges();
            }
        }
    }
}
