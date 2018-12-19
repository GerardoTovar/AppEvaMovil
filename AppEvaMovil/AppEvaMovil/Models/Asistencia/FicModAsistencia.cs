using System;
using System.Collections.Generic;
using System.Text;

//
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEvaMovil.Models.Asistencia
{
    public class FicModAsistencia {
        public class cat_estatus
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public Int16 IdEstatus { get; set; }

            //[ForeignKey("cat_tipo_estatus"), Required]
            public cat_tipo_estatus cat_tipo_estatus { get; set; }
            public Int16 IdTipoEstatus { get; set; }


            [StringLength(50, ErrorMessage = "La cllave no puede ser mas de 50 caracteres")]
            public String Clave { get; set; }
            [StringLength(30, ErrorMessage = "La Descripcion no puede ser mas de 30 caracteres")]
            public String DesEstatus { get; set; }
            [StringLength(1, ErrorMessage = "Activo no puede ser más de 1 caracter")]
            public String Activo { get; set; }
            public DateTime FechaReg { get; set; }
            public DateTime FechaUltMod { get; set; }
            [StringLength(20, ErrorMessage = "El registro no puede ser mas de 20 caracteres")]
            public String UsuarioReg { get; set; }
            [StringLength(20, ErrorMessage = "El modo no puede ser mas de 20 caracteres")]
            public String UsuarioMod { get; set; }
            [StringLength(1, ErrorMessage = "Borrado no puede ser más de 1 caracter")]
            public bool Borrado { get; set; }
        }
         public class zt_inventatios_acumulados_conteos
        {

            public List<eva_cat_edificios> eva_cat_edificios { get; set; }

        }

        public class eva_cat_edificios
        {

            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public Int16 IdEdificio { get; set; }
            [StringLength(20, ErrorMessage = "Alias no debe ser mayor a 20 caracteres")]
            public String Alias { get; set; }
            [StringLength(50, ErrorMessage = "La Descripcion no debe ser mayor a 50 caracteres")]
            public String DesEdificio { get; set; }
            public Int16 Prioridad { get; set; }
            [StringLength(20, ErrorMessage = "Clave no debe ser mayor a 20 caracteres")]
            public String Clave { get; set; }
            public DateTime FechaReg { get; set; }
            public DateTime FechaUltMod { get; set; }
            [StringLength(30, ErrorMessage = "Registro no debe ser mayor a 30 caracteres")]
            public String UsuarioReg { get; set; }
            [StringLength(30, ErrorMessage = "Modo no debe ser mayor a 30 caracteres")]
            public String UsuarioMod { get; set; }
            [StringLength(1, ErrorMessage = "Activo no puede ser más de 1 caracter")]
            public String Activo { get; set; }
            [StringLength(1, ErrorMessage = "Borrado no puede ser más de 1 caracter")]
            public String Borrado { get; set; }
        }

        public class cat_tipo_estatus
        {
            // [Key, Required]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public Int16 IdTipoEstatus { get; set; }
            [StringLength(50, ErrorMessage = "Descripcion no debe ser mas de 50 caracteres")]
            public String DesTipoEstatus { get; set; }
            [StringLength(1, ErrorMessage = "Activo no puede ser más de 1 caracter")]
            public String Activo { get; set; }
            public DateTime FechaReg { get; set; }
            [StringLength(20, ErrorMessage = "Registro no debe ser mayor a 20 catacteres")]
            public String UsuarioReg { get; set; }
            public DateTime FechaUltMod { get; set; }
            [StringLength(20, ErrorMessage = "Modo no debe ser mayor a 20 caracteres")]
            public String UsuarioMod { get; set; }
            [StringLength(1, ErrorMessage = "Activo no puede ser más de 1 caracter")]
            public bool Borrado { get; set; }
        }

        public class eva_cat_espacios
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public Int16 IdEspacio { get; set; }
            public eva_cat_edificios eva_cat_edificios { get; set; }
            public Int16 IdEdificio { get; set; }

            [StringLength(20, ErrorMessage = "Modo no debe ser mayor a 20 caracteres")]
            public String Clave { get; set; }
            [StringLength(50, ErrorMessage = "Descripcion no debe ser mayor a 50 caracteres")]
            public String DesEspacio { get; set; }
            public Int16 Prioridad { get; set; }
            [StringLength(10, ErrorMessage = "Alias no puede ser más de 10 caracteres")]
            public String Alias { get; set; }
            public Int16 RangoTiempoReserva { get; set; }
            public Int16 Capacidad;

            public cat_tipo_estatus cat_tipo_estatus { get; set; }
            public Int16 IdTipoEstatus { get; set; }

            public cat_estatus cat_estatus { get; set; }
            public Int16 IdEstatus { get; set; }

            [StringLength(20, ErrorMessage = "Referencia no puede ser más de 20 caracter")]
            public String RefeUbicacion { get; set; }
            [StringLength(1, ErrorMessage = "PermiteCruce no puede ser más de 1 caracter")]
            public String PermiteCruce { get; set; }
            [StringLength(50, ErrorMessage = "obserbacion no puede ser más de 50 caracter")]
            public String Observacion { get; set; }
            public DateTime FechaReg { get; set; }
            public DateTime FechaUltMod { get; set; }
            [StringLength(20, ErrorMessage = "usuario reg no puede ser más de 20 caracter")]
            public String UsuarioReg { get; set; }
            [StringLength(20, ErrorMessage = "modo no puede ser más de 20 caracter")]
            public String UsuarioMod { get; set; }
            [StringLength(1, ErrorMessage = "Activo no puede ser más de 1 caracter")]
            public bool Activo { get; set; }
            [StringLength(1, ErrorMessage = "Borrado no puede ser más de 1 caracter")]
            public bool Borrado { get; set; }
        }
    }
    

    
}
