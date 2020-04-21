﻿using Proyecto_ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ASP.Controllers
{
    public class LibrosController : Controller
    {


        // GET: Libros

        public ActionResult Index()
        {
            try
            {
                using (LibrosContext db = new LibrosContext())
                {
                    List<CAT_LIBROS> lista = db.CAT_LIBROS.ToList();
                    return View(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Busqueda

        public ActionResult Buscar(int Cadena = 1)
        {
            try
            {
                using (LibrosContext db = new LibrosContext())
                {
                    List<CAT_LIBROS> Blibro = db.CAT_LIBROS.Where(B => B.isbn == Cadena).ToList();

                    return View(Blibro);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Agregar Libros

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(CAT_LIBROS Libro)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new LibrosContext())
                {
                    db.CAT_LIBROS.Add(Libro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Fallo al ingresar libro -" + ex.Message);
                return View();
            }
        }



        // Editar Libros

        public ActionResult Editar(int id)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View();
                using (var db = new LibrosContext())
                {

                    CAT_LIBROS Blibro = db.CAT_LIBROS.Find(id);

                    return View(Blibro);


                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CAT_LIBROS Libro)
        {
            try
            {
                using (var db = new LibrosContext())
                {

                    CAT_LIBROS Blibro = db.CAT_LIBROS.Find(Libro.isbn);
                    Blibro.isbn = Libro.isbn;
                    Blibro.nombre = Libro.nombre;
                    Blibro.autor = Libro.autor;
                    Blibro.editorial = Libro.editorial;
                    Blibro.edicion = Libro.edicion;
                    Blibro.escuela = Libro.escuela;
                    Blibro.unidades = Libro.unidades;
                    Blibro.tematica = Libro.tematica;
                    Blibro.asignatura = Libro.asignatura;
                    db.SaveChanges();
                    return RedirectToAction("Index");



                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        // Detalles Libros
        public ActionResult Detalles(int id)
        {
            try
            {

                using (var db = new LibrosContext())
                {

                    CAT_LIBROS Blibro = db.CAT_LIBROS.Find(id);

                    return View(Blibro);
                }
            }

            catch (Exception)
            {

                throw;
            }
        }


        // Eliminar libros
        public ActionResult Eliminar(int id)
        {
            try
            {

                using (var db = new LibrosContext())
                {

                    CAT_LIBROS Blibro = db.CAT_LIBROS.Find(id);
                    db.CAT_LIBROS.Remove(Blibro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch (Exception)
            {

                throw;
            }
        }



        public ActionResult Librosdañados()
        {
            Response.Redirect("http://localhost:53651/LibrosDa%C3%B1ados/IndexDa%C3%B1ados");
            return View();
        }


        public ActionResult Back()
        {
            Response.Redirect("http://localhost:53651/Home/Index/22");
            return View();
        }

        //Programación para agregar libros dañados

        public ActionResult IndexDañados()
        {
            try
            {
                using (LibrosContext db = new LibrosContext())
                {
                    List<CAT_LIBROS_DAÑADOS> lista = db.CAT_LIBROS_DAÑADOS.ToList();
                    return View(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AgregarDañados(int id)
        {
            using (var db = new LibrosContext())
            {

                CAT_LIBROS Blibro = db.CAT_LIBROS.Find(id);
                return View(Blibro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarDañados(CAT_LIBROS_DAÑADOS Libro)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new LibrosContext())
                {
                    db.CAT_LIBROS_DAÑADOS.Add(Libro);

                    db.SaveChanges();
                    return RedirectToAction("IndexDañados");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Fallo al reportar libro dañado - " + ex.Message);
                return View();
            }
        }

        public ActionResult DetallesDañados(int id)
        {
            try
            {

                using (var db = new LibrosContext())
                {

                    CAT_LIBROS_DAÑADOS Blibro = db.CAT_LIBROS_DAÑADOS.Find(id);

                    return View(Blibro);
                }
            }

            catch (Exception)
            {

                throw;
            }
        }


        // Eliminar Libros Dañados

        public ActionResult EliminarDañados(int id)
        {
            try
            {

                using (var db = new LibrosContext())
                {

                    CAT_LIBROS_DAÑADOS Blibro = db.CAT_LIBROS_DAÑADOS.Find(id);
                    db.CAT_LIBROS_DAÑADOS.Remove(Blibro);
                    db.SaveChanges();
                    return RedirectToAction("IndexDañados");
                }
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}