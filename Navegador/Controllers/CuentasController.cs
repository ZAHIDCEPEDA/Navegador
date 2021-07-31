using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Navegador.Data;
using Navegador.Models;
using Navegador.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Navegador.Controllers
{
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }
        private void Combox()
        {
            ViewData["CodigoSocio"] = new SelectList(_context.Socios.Select(z => new TablasDatos
            {
                CedulaSocio = z.Cedula,
                NombreSocio = $"{z.Nombre}{z.Apellido}",
                Estado = z.Estado
            }).Where(z => z.Estado == 1).ToList(), "CedulaSocio", "NombreSocio");
        }
        // GET: CuentasController
        public ActionResult Index()
        {
            List<TablasDatos> ltscuentas = new List<TablasDatos>();
            ltscuentas = _context.Cuenta.Select(z => new TablasDatos
            {
                NumeroCuenta=z.Numero,
                Saldo=z.SaldoTotal,
                NombreSocio = $"{z.CodigoSocioNavigation.Nombre}{z.CodigoSocioNavigation.Apellido}",
                 Estado = z.Estado
            }).ToList();
            return View(ltscuentas);
        }

        // GET: CuentasController/Details/5
        public ActionResult Details(string id)
        {

            Cuenta cuenta = _context.Cuenta.Where(z => z.Numero == id).FirstOrDefault();
            return View(cuenta);
        }

        // GET: CuentasController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }

        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            try
            {
                cuenta.Estado = 1;
                _context.Add(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentasController/Edit/5
        public ActionResult Edit(string id)
            {
            Cuenta cuenta= _context.Cuenta.Where(z => z.Numero == id).FirstOrDefault();
            Combox();
            return View(cuenta);
        }

        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Cuenta cuenta)
        {
            if (id != cuenta.Numero)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(cuenta);
            }
        }

        // GET: CuentasController/Delete/5
        public ActionResult Activar(string id)
        {
            Cuenta cuenta = _context.Cuenta.Where(z => z.Numero == id).FirstOrDefault();
            cuenta.Estado = 1;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Desactivar(string id)
        {
            Cuenta cuenta = _context.Cuenta.Where(z => z.Numero == id).FirstOrDefault();
            cuenta.Estado = 0;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
