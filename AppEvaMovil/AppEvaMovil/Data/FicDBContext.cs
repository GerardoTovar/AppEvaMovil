using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

//
using Microsoft.EntityFrameworkCore;
using static AppEvaMovil.Models.Asistencia.FicModAsistencia;

namespace AppEvaMovil.Data
{
    public class FicDBContext : DbContext
    {
        private readonly string FicDataBasePath;

        public DbSet<eva_cat_edificios> eva_cat_edificios { get; set; }
        public DbSet<eva_cat_espacios> eva_cat_espacios { get; set; }
        public DbSet<cat_estatus> cat_estatus { get; set; }
        

        public FicDBContext(string FicPaDataBasePath)
        {
            FicDataBasePath = FicPaDataBasePath;
            FicMetCrearDB();
        } //CONSTRUCTOR

        public async void FicMetCrearDB()
        {
            try
            {
                //Se crea la base de datos en base el al esquema 
                //await Database.EnsureDeletedAsync();
                //Nuevo metodo básico de Entity Framework que garantiza que exite la base de datos para el contexto
                //Si no existe, no se toma ninguna acción
                await Database.EnsureCreatedAsync();
            }
            catch (Exception e) {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//ESTE METODO CREA LA BASE DE DATOS

        protected async override void OnConfiguring(DbContextOptionsBuilder FicPaOptionsBuilder) {
            try
            {
                FicPaOptionsBuilder.UseSqlite($"Filename={FicDataBasePath}");
                FicPaOptionsBuilder.EnableSensitiveDataLogging();
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//CONFIGURACION DE LA CONEXION

        

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                /*//CREANDO LLAVES PRIMARIAS
                modelBuilder.Entity<eva_cat_edificios>()
                    .HasKey(c => new { c.IdEdificio });
                modelBuilder.Entity<eva_cat_espacios>();

                modelBuilder.Entity<eva_cat_espacios>()
                    .HasKey(c => new { c.IdEspacio });
                modelBuilder.Entity<eva_cat_espacios>();

                modelBuilder.Entity<cat_estatus>()
                    .HasKey(c => new { c.IdEstatus });
                modelBuilder.Entity<eva_cat_espacios>();


                //CREANDO LLAVES FORANEAS
                modelBuilder.Entity<eva_cat_espacios>()
                    .HasOne(c => c.eva_cat_edificios)
                    .WithMany().HasForeignKey(s => new { s.IdEdificio });

                modelBuilder.Entity<eva_cat_espacios>()
                   .HasOne(c => c.cat_estatus)
                   .WithMany().HasForeignKey(s => new { s.IdEstatus });*/


                //EVA_CAT_EDIFICIOS
                modelBuilder.Entity<eva_cat_edificios>().HasKey(pk => new { pk.IdEdificio });

                //EVA_CAT_ESPACIOS
                modelBuilder.Entity<eva_cat_espacios>().HasKey(pk => new { pk.IdEspacio });
                modelBuilder.Entity<eva_cat_espacios>().HasOne(fk => fk.eva_cat_edificios).WithMany().HasForeignKey(fk => new { fk.IdEspacio });
                modelBuilder.Entity<eva_cat_espacios>().HasOne(fk => fk.cat_estatus).WithMany().HasForeignKey(fk => new { fk.IdTipoEstatus, fk.IdEstatus });

                //CAT_ESTATUS
                modelBuilder.Entity<cat_estatus>().HasKey(pk => new { pk.IdEstatus, pk.IdTipoEstatus });
                modelBuilder.Entity<cat_estatus>().HasOne(fk => fk.cat_tipo_estatus).WithMany().HasForeignKey(fk => new { fk.IdTipoEstatus });


                //CAT_TIPO_ESTATUS
                modelBuilder.Entity<cat_tipo_estatus>().HasKey(pk => new { pk.IdTipoEstatus });

            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }

        }//AL CREAR EL MODELO
    }
}
