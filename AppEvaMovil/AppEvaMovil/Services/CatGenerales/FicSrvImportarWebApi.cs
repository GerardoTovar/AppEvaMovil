using AppEvaMovil.Data;
using AppEvaMovil.Interfaces.CatGenerales;
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
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Services.CatGenerales
{
    public class FicSrvImportarWebApi : IFicSrvImportarWebApi
    {
        private readonly FicDBContext FicLoBDContext;
        private readonly HttpClient FiClient;

        public FicSrvImportarWebApi()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            FiClient = new HttpClient();
            FiClient.MaxResponseContentBufferSize = 256000;
        }//CONSTRUCTOR

        private async Task<List<eva_cat_edificios>> FicGetListInventarioActualiza(int id=0)
        {
            try
            {
              
                string url = "";
                if (id != 0) url = "http://localhost:53285/api/FicGetListCatEdificios/" + id;
                else url = "http://localhost:53285/api/FicGetListCatEdificios";


                var task = await FiClient.GetAsync(url);
                var jsonString = await task.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_cat_edificios>>(jsonString);

            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
                return null;
            }
        }//GET: A INVENTARIOS


        private async Task<eva_cat_edificios> FicExistzt_inventarios(int id)
        {
            return await (from inv in FicLoBDContext.eva_cat_edificios where inv.IdEdificio == id select inv).AsNoTracking().SingleOrDefaultAsync();
        }//buscar en local


        public async Task<string> FicGetImportInventarios(int id=0)
        {
            string FicMensaje = "";
            try
            {
                FicMensaje = "IMPORTACION: \n";
                var FicGetReultREST = new List<eva_cat_edificios > ();

               if (id != 0) FicGetReultREST = await FicGetListInventarioActualiza(id);
                else FicGetReultREST = await FicGetListInventarioActualiza();

                if (FicGetReultREST != null && FicGetReultREST != null)
                {
                    FicMensaje += "IMPORTANDO: zt_inventarios \n";
                    foreach (eva_cat_edificios inv in FicGetReultREST)
                    {
                        var respuesta = await FicExistzt_inventarios(inv.IdEdificio);
                        if (respuesta != null)
                        {
                            try
                            {
                                respuesta.IdEdificio = inv.IdEdificio;
                                respuesta.Alias = inv.Alias;
                                respuesta.DesEdificio = inv.DesEdificio;
                                respuesta.Prioridad = inv.Prioridad;
                                respuesta.Clave = inv.Clave;
                                respuesta.FechaReg = inv.FechaReg;
                                respuesta.FechaUltMod = inv.FechaUltMod;
                                respuesta.UsuarioReg = inv.UsuarioReg;
                                respuesta.UsuarioMod = inv.UsuarioMod;
                                respuesta.Activo = inv.Activo;
                                respuesta.Borrado = inv.Borrado;

                                FicLoBDContext.Update(respuesta);
                                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-UPDATE-> IdInventario: " + inv.IdEdificio + " \n" : "-NO NECESITO ACTUALIZAR->  IdInventario: " + inv.IdEdificio + " \n";
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                        else
                        {
                            try
                            {
                                FicLoBDContext.Add(inv);
                                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-INSERT-> IdInventario: " + inv.IdEdificio + " \n" : "-ERROR EN INSERT-> IdInventario: " + inv.IdEdificio + " \n";
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                    }
                }
                else FicMensaje += "-> SIN DATOS. \n";
            }
            catch (Exception e)
            {
                FicMensaje += "ALERTA: " + e.Message.ToString() + "\n";
            }
            return FicMensaje;
        }//FicGetImportInventarios()

        public async Task<string> FicGetImportCatalogos()
        {
            string FicMensaje="";
            try
            {
                FicMensaje = "IMPORTACION: \n";
                //var FicGetReultREST = await FicGetListCatalogosActualiza();

                //if (FicGetReultREST != null && FicGetReultREST.zt_cat_productos != null)
                //{
                //    FicMensaje += "IMPORTANDO: zt_cat_productos \n";
                //    foreach (zt_cat_productos inv in FicGetReultREST.zt_cat_productos)
                //    {
                //        var respuesta = await FicExistzt_cat_productos(inv.IdSKU);
                //        if (respuesta != null)
                //        {
                //            try
                //            {
                //                respuesta.IdSKU = inv.IdSKU;
                //                respuesta.CodigoBarras = inv.CodigoBarras;
                //                respuesta.DesSKU = inv.DesSKU;
                //                respuesta.FechaReg = inv.FechaReg;
                //                respuesta.UsuarioReg = inv.UsuarioReg;
                //                respuesta.FechaUltMod = inv.FechaUltMod;
                //                respuesta.UsuarioMod = inv.UsuarioMod;
                //                respuesta.Activo = inv.Activo;
                //                respuesta.Borrado = inv.Borrado;
                //                FicMensaje +=  await FicLoBDContext.SaveChangesAsync() > 0 ? "-UPDATE-> IdSKU: " + inv.IdSKU + " \n" : "-NO NECESITO ACTUALIZAR-> IdSKU: " + inv.IdSKU + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //        else
                //        {
                //            try
                //            {
                //                FicLoBDContext.Add(inv);
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-INSERT-> IdSKU: " + inv.IdSKU + " \n" : "-ERROR EN INSERTAR-> IdSKU: " + inv.IdSKU + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //    }
                //}
                //else FicMensaje += "-> SIN DATOS. \n";

                //if (FicGetReultREST != null && FicGetReultREST.zt_cat_unidad_medidas != null)
                //{
                //    FicMensaje += "IMPORTANDO: zt_cat_unidad_medidas \n";
                //    foreach (zt_cat_unidad_medidas inv in FicGetReultREST.zt_cat_unidad_medidas)
                //    {
                //        var respuesta = await FicExistzt_cat_unidad_medidas(inv.IdUnidadMedida);
                //        if (respuesta != null)
                //        {
                //            try
                //            {
                //                respuesta.IdUnidadMedida = inv.IdUnidadMedida;
                //                respuesta.DesUMedida = inv.DesUMedida;
                //                respuesta.FechaReg = inv.FechaReg;
                //                respuesta.UsuarioReg = inv.UsuarioReg;
                //                respuesta.FechaUltMod = inv.FechaUltMod;
                //                respuesta.UsuarioMod = inv.UsuarioMod;
                //                respuesta.Activo = inv.Activo;
                //                respuesta.Borrado = inv.Borrado;
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-UPDATE-> IdUnidadMedida: " + inv.IdUnidadMedida + " \n" : "-NO NECESITO ACTUALIZAR-> IdUnidadMedida: " + inv.IdUnidadMedida + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //        else
                //        {
                //            try
                //            {
                //                FicLoBDContext.Add(inv);
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-INSERT-> IdUnidadMedida: " + inv.IdUnidadMedida + " \n" : "-ERROR EN INSERTAR-> IdUnidadMedida: " + inv.IdUnidadMedida + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //    }
                //}
                //else FicMensaje += "-> SIN DATOS. \n";

                //if (FicGetReultREST != null && FicGetReultREST.zt_cat_productos_medidas != null)
                //{
                //    FicMensaje += "IMPORTANDO: zt_cat_productos_medidas \n";
                //    foreach (zt_cat_productos_medidas inv in FicGetReultREST.zt_cat_productos_medidas)
                //    {
                //        var respuesta = await FicExistzt_cat_productos_medidas(inv.IdSKU, inv.IdUnidadMedida);
                //        if (respuesta != null)
                //        {
                //            try
                //            {
                //                respuesta.IdSKU = inv.IdSKU;
                //                respuesta.IdUnidadMedida = inv.IdUnidadMedida;
                //                respuesta.CantidadPZA = inv.CantidadPZA;
                //                respuesta.FechaReg = inv.FechaReg;
                //                respuesta.UsuarioReg = inv.UsuarioReg;
                //                respuesta.FechaUltMod = inv.FechaUltMod;
                //                respuesta.UsuarioMod = inv.UsuarioMod;
                //                respuesta.Activo = inv.Activo;
                //                respuesta.Borrado = inv.Borrado;
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync()>0 ? "-UPDATE-> IdUnidadMedida: " + inv.IdUnidadMedida + " ,IdSKU: " + inv.IdSKU + " \n" : "-NO NECESITO ACTUALIZAR-> IdUnidadMedida: " + inv.IdUnidadMedida + " ,IdSKU: " + inv.IdSKU + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //        else
                //        {
                //            try
                //            {
                //                FicLoBDContext.Add(inv);
                //                //await FicLoBDContext.SaveChangesAsync();
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-INSERT-> IdUnidadMedida: " + inv.IdUnidadMedida + " ,IdSKU: " + inv.IdSKU + " \n" : "-ERROR EN INSERTAR-> IdUnidadMedida: " + inv.IdUnidadMedida + " ,IdSKU: " + inv.IdSKU + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //    }
                //}
                //else FicMensaje += "-> SIN DATOS. \n";

                //if (FicGetReultREST != null && FicGetReultREST.zt_cat_cedis != null)
                //{
                //    FicMensaje += "IMPORTANDO: zt_cat_cedis \n";
                //    foreach (zt_cat_cedis inv in FicGetReultREST.zt_cat_cedis)
                //    {
                //        var respuesta = await FicExistzt_cat_cedis(inv.IdCEDI);
                //        if (respuesta != null)
                //        {
                //            try
                //            {
                //                respuesta.IdCEDI = inv.IdCEDI;
                //                respuesta.DesCEDI = inv.DesCEDI;
                //                respuesta.FechaReg = inv.FechaReg;
                //                respuesta.UsuarioReg = inv.UsuarioReg;
                //                respuesta.FechaUltMod = inv.FechaUltMod;
                //                respuesta.UsuarioMod = inv.UsuarioMod;
                //                respuesta.Activo = inv.Activo;
                //                respuesta.Borrado = inv.Borrado;
                                
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() >0 ? "-UPDATE-> IdCEDI: " + inv.IdCEDI + " \n": "-NO NECESITO ACTUALIZAR-> IdCEDI: " + inv.IdCEDI + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //        else
                //        {
                //            try
                //            {
                //                FicLoBDContext.Add(inv);
                //                //ñawait FicLoBDContext.SaveChangesAsync();
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() >0? "-INSERT-> IdCEDI: " + inv.IdCEDI + " \n": "-ERROR EN INSERTAR-> IdCEDI: " + inv.IdCEDI + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //    }
                //}
                //else FicMensaje += "-> SIN DATOS. \n";

                //if (FicGetReultREST != null && FicGetReultREST.zt_cat_almacenes != null)
                //{
                //    FicMensaje += "IMPORTANDO: zt_cat_almacenes \n";
                //    foreach (zt_cat_almacenes inv in FicGetReultREST.zt_cat_almacenes)
                //    {
                //        var respuesta = await FicExistzt_cat_almacenes(inv.IdAlmacen);
                //        if (respuesta != null)
                //        {
                //            try
                //            {
                //                respuesta.IdAlmacen = inv.IdAlmacen;
                //                respuesta.IdCEDI = inv.IdCEDI;
                //                respuesta.DesAlmacen = inv.DesAlmacen;
                //                respuesta.FechaReg = inv.FechaReg;
                //                respuesta.UsuarioReg = inv.UsuarioReg;
                //                respuesta.FechaUltMod = inv.FechaUltMod;
                //                respuesta.UsuarioMod = inv.UsuarioMod;
                //                respuesta.Activo = inv.Activo;
                //                respuesta.Borrado = inv.Borrado;
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() >0 ?  "-UPDATE-> IdAlmacen: " + inv.IdAlmacen + " \n": "-NO NECESITO ACTUALIZAR-> IdAlmacen: " + inv.IdAlmacen + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //        else
                //        {
                //            try
                //            {
                //                FicLoBDContext.Add(inv);
                //                FicMensaje += await FicLoBDContext.SaveChangesAsync() > 0 ? "-INSERT-> IdAlmacen: " + inv.IdAlmacen + " \n": "-ERROR EN INSERTAR-> IdAlmacen: " + inv.IdAlmacen + " \n";
                //            }
                //            catch (Exception e)
                //            {
                //                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                //            }
                //        }
                //    }
                //}
                //else FicMensaje += "-> SIN DATOS. \n";
            }
            catch (Exception e)
            {
                FicMensaje += "ALERTA: " + e.Message.ToString() + "\n";
            }
            return FicMensaje;
        }//FicGetImportCatalogos()

    }//CLASS
}//NAMESPACE
