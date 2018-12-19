using AppEvaMovil.Data;
using AppEvaMovil.Interfaces.SQLite;
using AppEvaMovil.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppEvaMovil.Interfaces.CatGenerales;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Services.CatGenerales
{
    public class FicSrvExportarWebApi : IFicSrvExportarWebApi
    {
        private readonly FicDBContext FicLoBDContext;
        private readonly HttpClient FiClient;
        

        public FicSrvExportarWebApi()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            FiClient = new HttpClient();
            FiClient.MaxResponseContentBufferSize = 256000;

        }//CONSTRUCTOR

        private async Task<string> FicPostListInventarios(zt_inventatios_acumulados_conteos item)
        {
            const string url = "http://localhost:53285/api/New";

            var js = JsonConvert.SerializeObject(item.eva_cat_edificios);
            var uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await FiClient.PostAsync(
                new Uri(string.Format(url, string.Empty)), 
                new StringContent(js, Encoding.UTF8, "application/json")
            );
           
            return await response.Content.ReadAsStringAsync();
        }//POST: A INVENTARIOS

        public async Task<string> FicPostExportInventarios()
        {

            return await FicPostListInventarios(new zt_inventatios_acumulados_conteos()
            {

                eva_cat_edificios = await (from a in FicLoBDContext.eva_cat_edificios select a).AsNoTracking().ToListAsync()
              //  zt_inventarios_acumulados = await (from a in FicLoBDContext.zt_inventarios_acumulados select a).AsNoTracking().ToListAsync(),
               // zt_inventarios_conteos = await (from a in FicLoBDContext.zt_inventarios_conteos select a).AsNoTracking().ToListAsync()
            });
        }//METODO DE EXPORT INVENTARIOS

    }//CLASS 
}//NAMESPACE
