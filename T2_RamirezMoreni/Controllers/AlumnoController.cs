using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2_RamirezMoreni.Models;

namespace T2_RamirezMoreni.Controllers
{
    public class AlumnoController : Controller
    {

        private static string listaw = @"[]";



        private List<Alumno> Deserializar()
        {
            List<Alumno> lista =
                JsonConvert.DeserializeObject<List<Alumno>>(listaw);

            return lista;
        }



        private void Serializar(List<Alumno> lista)
        {
            listaw = JsonConvert.SerializeObject(
                lista,
                Formatting.Indented
            );


            string ruta = Server.MapPath("~/alumnos.json");

            System.IO.File.WriteAllText(ruta, listaw);
        }

        //index
        public ActionResult Index()
        {
            List<Alumno> lista = Deserializar();
            return View(lista);
        }



        public ActionResult Agregar()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            List<Alumno> lista = Deserializar();

            
            bool existe = lista.Any(
                x => x.dni == alumno.dni
            );

            if (existe)
            {
                ViewBag.Mensaje =
                    "El DNI ya se encuentra registrado.";

                return View(alumno);
            }
            lista.Add(alumno);


            Serializar(lista);

            ViewBag.Mensaje =
           "Alumno registrado correctamente.";

            return RedirectToAction("Index");

        }

        public ActionResult Eliminar(string dni) {

            List<Alumno> lista = Deserializar();

            Alumno alumno = lista.FirstOrDefault(
                x => x.dni == dni
            );

            if (alumno != null)
            {
                lista.Remove(alumno);

                Serializar(lista);
            }

            return RedirectToAction("Index");
        }



        public ActionResult Detalles(string dni)
        {
            List<Alumno> lista = Deserializar();

            Alumno alumno = lista.FirstOrDefault(
                x => x.dni == dni
            );

            if (alumno == null)
            {
                return HttpNotFound();
            }

            return View(alumno);
        }


        
        [HttpGet]
        public ActionResult Actualizar(string dni)
        {
            List<Alumno> lista = Deserializar();

            Alumno alumno = lista.FirstOrDefault(
                x => x.dni == dni
            );

            if (alumno == null)
            {
                return HttpNotFound();
            }

            ViewBag.DniOriginal = dni;

            return View(alumno);
        }


        
        [HttpPost]
        public ActionResult Actualizar(
            string dniOriginal,
            Alumno alumno)
        {
            List<Alumno> lista = Deserializar();


            Alumno alumnoExistente =
                lista.FirstOrDefault(
                    x => x.dni == dniOriginal
                );

            if (alumnoExistente == null)
            {
                return HttpNotFound();
            }



            bool dniRepetido = lista.Any(
                x => x.dni == alumno.dni
                && x.dni != dniOriginal
            );

            if (dniRepetido)
            {
                ViewBag.Mensaje =
                    "El DNI ingresado ya pertenece a otro alumno.";

                ViewBag.DniOriginal = dniOriginal;

                return View(alumno);
            }


                
                
                alumnoExistente.nombres = alumno.nombres;
                alumnoExistente.apellidos = alumno.apellidos;
                alumnoExistente.dni = alumno.dni;
                alumnoExistente.carrera = alumno.carrera;
                alumnoExistente.ciclo = alumno.ciclo;


               
                Serializar(lista);

                return RedirectToAction("Index");
            }

        }
}   
