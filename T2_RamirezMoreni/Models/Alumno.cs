using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace T2_RamirezMoreni.Models
{
    public class Alumno
    {
   
        public string nombres { get; set; }
        public string apellidos { get; set; }

        public string dni { get; set; }
        public string carrera { get; set; }
        public int ciclo { get; set; }


        public Alumno()
        {

        }

        public Alumno(string nombres, string apellidos, string dni, string carrera, int ciclo)
        {

            this.nombres = nombres;
            this.apellidos = apellidos;
            this.dni = dni;
            this.carrera = carrera;
            this.ciclo = ciclo;
        }
        
    }
}




    
