using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;

namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {

            List<ListTablaViewModel> lst;
            using (pruebaEntities1 db = new pruebaEntities1())
            {
                lst = (from d in db.tabla
                       select new ListTablaViewModel
                       {
                           Id = d.id,
                           Nombre = d.nombre,
                           Fecha_Nacimiento = d.fecha_nacimiento,
                           Correo = d.correo
                       }).ToList();
            }

            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (pruebaEntities1 db = new pruebaEntities1())
                    {
                        var tabla = new tabla()
                        {
                            nombre = model.Nombre,
                            correo = model.Correo,
                            fecha_nacimiento = model.Fecha_Nacimiento
                        };

                        db.tabla.Add(tabla);
                        db.SaveChanges();
                    }

                   return Redirect("~/Tabla/");
                }

                return View(model);
            }
            catch (Exception exc)
            {

                throw new Exception(exc.Message);
            }

        }


        public ActionResult Editar(int Id)
        {
            TablaViewModel model = new TablaViewModel();
            using (pruebaEntities1 db = new pruebaEntities1())
            {
                var registro = db.tabla.Find(Id);
                model.Correo = registro.correo;
                model.Nombre = registro.nombre;
                model.Fecha_Nacimiento = registro.fecha_nacimiento;
                model.Id= registro.id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (pruebaEntities1 db = new pruebaEntities1())
                    {
                        var tabla = db.tabla.Find(model.Id);
                        tabla.nombre = model.Nombre;
                        tabla.correo = model.Correo;
                        tabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Tabla/");
                }

                return View(model);
            }
            catch (Exception exc)
            {

                throw new Exception(exc.Message);
            }

        }


        public ActionResult Eliminar(int Id)
        {
            using (pruebaEntities1 db = new pruebaEntities1())
            {
                var registro = db.tabla.Find(Id);
                db.tabla.Remove(registro);
                db.SaveChanges();
            }
            return Redirect("~/Tabla/");
        }



    }
}