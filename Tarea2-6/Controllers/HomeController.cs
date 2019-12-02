using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea2_6.Models;

namespace Tarea2_6.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            RegistroPelicula rp = new RegistroPelicula();
            return View(rp.RecuperarTodos());
        }
        public ActionResult Grabar()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {
            RegistroPelicula rp = new RegistroPelicula();
            Pelicula pelicula = new Pelicula
            {
                Codigo = int.Parse(collection["Codigo"]),
                Titulo = collection["Titulo"],
                Director = collection["Director"],
                Actorp = collection["Actorp"],
                No_Actores = int.Parse(collection["No_Actores"]),
                Duracion = double.Parse(collection["Duracion"]),
                Estreno = int.Parse(collection["Estreno"]),
            };
            rp.Grabar(pelicula);
            return RedirectToAction("Index");
        }
        public ActionResult Borrar(int cod)
        {
            RegistroPelicula registro = new RegistroPelicula();
            registro.Borrar(cod);
            return RedirectToAction("Index");
        }
        public ActionResult Modificacion(int cod)
        {
            RegistroPelicula registro = new RegistroPelicula();
            Pelicula pelicula = registro.Recuperar(cod);
            return View(pelicula);
        }
        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            RegistroPelicula registro = new RegistroPelicula();
            Pelicula pelicula = new Pelicula
            {
                Codigo = int.Parse(collection["Codigo"]),
                Titulo = collection["Titulo"].ToString(),
                Director = collection["Director"].ToString(),
                Actorp = collection["Actorp"].ToString(),
                No_Actores = int.Parse(collection["No_Actores"].ToString()),
                Duracion = double.Parse(collection["Duracion"].ToString()),
                Estreno = int.Parse(collection["Estreno"].ToString())
            };
            registro.Modificar(pelicula);
            return RedirectToAction("Index");
        }

    }
}