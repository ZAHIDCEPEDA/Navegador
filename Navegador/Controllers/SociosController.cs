using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Navegador.Data;
using Navegador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Navegador.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SociosController (ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SociosController
        public ActionResult Index()
        {
            List<Socio> ltssocios = new List<Socio>();
            ltssocios = _context.Socios.ToList();
            return View(ltssocios);
        }

        // GET: SociosController/Details/5
        public ActionResult Details(string id)
        {
            Socio socio = _context.Socios.Where(z => z.Cedula == id).FirstOrDefault();
            return View(socio);
        }

        // GET: SociosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SociosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Socio socio)
        {
            try
            {
                socio.Estado = 1;
                _context.Add(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socio);
            }
        }

        // GET: SociosController/Edit/5
        public ActionResult Edit(string id)
        {
            Socio socio = _context.Socios.Where(z => z.Cedula == id).FirstOrDefault();
            return View(socio);
        }

        // POST: SociosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Socio socio)
        {
            if(id != socio.Cedula)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.SaveChanges();
                _context.Update(socio);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socio);
            }
        }
        public ActionResult Activar (string id)
        {
            Socio socio = _context.Socios.Where(z => z.Cedula == id).FirstOrDefault();
            socio.Estado = 1;
            _context.SaveChanges();
            _context.Update(socio);
            return RedirectToAction("Index");
        }
        public ActionResult Desactivar(string id)
        {
            Socio socio = _context.Socios.Where(z => z.Cedula == id).FirstOrDefault();
            socio.Estado = 0;
            _context.SaveChanges();
            _context.Update(socio);
            return RedirectToAction("Index");
        }
    }
}
