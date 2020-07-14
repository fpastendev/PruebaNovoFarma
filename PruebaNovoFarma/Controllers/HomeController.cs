using System.Web.Mvc;
using Entidades;
using BLL;
using System.Collections.Generic;
using System;

namespace PruebaNovoFarma.Controllers
{
    public class HomeController : Controller
    {
        Negocio bll = new Negocio();

        public ActionResult Index()
        {
            var lista = new List<Entidades.Persona>();
            lista = bll.ListaPersonas(1);
            ViewBag.lista = lista;
            return View();
        }

        [HttpGet]
        public ActionResult EditaPersona(string id)
        {
            return View(bll.GetPersona(id));
        }

        [HttpPost]
        public ActionResult CreaPersona(Persona p)
        {
            p.Id = Guid.NewGuid().ToString();
            p.Activo = true;
            bll.CreaPersona(p);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditaPersona(Persona p)
        {
            var res = false;
            res = bll.EditaPersona(p);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EliminaPersona(string id)
        {
            bll.DesactivaPersona(id);
            return RedirectToAction("Index", "Home");
        }

    }
}
