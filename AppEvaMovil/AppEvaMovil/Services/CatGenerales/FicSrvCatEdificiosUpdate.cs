using AppEvaMovil.Interfaces.CatGenerales;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using AppEvaMovil.Data;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;
using AppEvaMovil.Interfaces.SQLite;
using AppEvaMovil.Helpers;

namespace AppEvaMovil.Services.CatGenerales
{
    public class FicSrvCatEdificiosUpdate : IFicSrvCatEdificiosUpdate
    {
        private  FicDBContext FicLoBDContext;
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        public FicSrvCatEdificiosUpdate()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetUpdateEdificio(eva_cat_edificios item)
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                FicLoBDContext.SaveChanges();
            }
        }

    }
}
