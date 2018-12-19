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
    public class FicSrvCatEdificiosList : IFicSrvCatEdificiosList
    {
        private readonly FicDBContext FicLoBDContext;

        public FicSrvCatEdificiosList() {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task<IEnumerable<eva_cat_edificios>> FicMetGetListCatEdificios()
        {
            return await (from inv in FicLoBDContext.eva_cat_edificios select inv).AsNoTracking().ToListAsync();
        }

    }
}
