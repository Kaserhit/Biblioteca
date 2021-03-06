﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Proyecto_ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ASP.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud


            // mostrar vista de los tickets emitidos
        public ActionResult Index()
        {
            using (LibrosContext db = new LibrosContext())
            {
                List<CAT_SOLICITUD> lista = db.CAT_SOLICITUD.ToList();
                return View(lista);
            }
        }

        // Busqueda de los tickets emitidos atraves de la base de datos y se actualiza la vista
        public ActionResult Buscar(FormCollection item)
        {
            try
            {
                string libro = item["id"];
                LibrosContext db = new LibrosContext();
                int id_cedula = Convert.ToInt32(libro);

                var datos = db.CAT_SOLICITUD.Where(x => x.cedula == id_cedula).Select(x => x).ToList();

                return View("~/Views/Solicitud/Index.cshtml", datos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Eliminar  el ticket emitido y volver a la base de datos el libro
        public ActionResult Agregar(CAT_SOLICITUD Libro)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new LibrosContext())
                {
                    db.CAT_SOLICITUD.Add(Libro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                return View();
            }
        }
        // Eliminar  los tickets emitidos
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (var db = new LibrosContext())
                {

                    CAT_SOLICITUD Blibro = db.CAT_SOLICITUD.Find(id);
                    // Programacion correspondiente para crear un pdf
                    Document pdfDoc = new Document(PageSize.LETTER, 0, 0, 0, 0);

                    PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

                    pdfDoc.Open();




                    //imagen

                    string path = Server.MapPath("/images/ulacit.png");
                    Image logo = Image.GetInstance(path);
                    logo.SetAbsolutePosition(210f, 150f);
                    logo.ScaleAbsolute(204f, 53f);
                    pdfDoc.Add(logo);

                    // texto

                    pdfDoc.Add(new Paragraph("                                                                 ---------------Ticker Entrega--------------"));
                    pdfDoc.Add(new Paragraph("                                    "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                                 ...............Datos del Entegador..............."));
                    pdfDoc.Add(new Paragraph("                                    "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                          Nombre:                              " + (Blibro.nombre)));
                    pdfDoc.Add(new Paragraph("                                                          Apellidos:                           " + (Blibro.apellido1 + " " + Blibro.apellido2)));
                    pdfDoc.Add(new Paragraph("                                                          Cedula:                              " + (Blibro.cedula)));

                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                                  ...............Datos del Libro..............."));
                    pdfDoc.Add(new Paragraph("                                    "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                          Nombre de Libro:                          " + (Blibro.nombrelibro)));
                    pdfDoc.Add(new Paragraph("                                                          ISBN del Libro:                             " + (Blibro.iscb)));

                    pdfDoc.Add(new Paragraph("                                    "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                                  .................Datos del Prestamo................."));
                    pdfDoc.Add(new Paragraph("                                    "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                        Fecha de Entrega:                   " + Blibro.fechaentrega.ToString()));
                    pdfDoc.Add(new Paragraph("                                                        Fecha de Solicitud:                  " + (Blibro.fechasoli.ToString())));                   pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                     "));
                    pdfDoc.Add(new Paragraph("                                                                                  Biblioteca Ulacit         "));
                    pdfDoc.Add(new Paragraph("                                                                            ___________________         "));

                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";

                    Response.AddHeader("content-disposition", "attatchment; filename=Reporte.pdf");
                    System.Web.HttpContext.Current.Response.Write(pdfDoc);
                    Response.Flush();
                    Response.End();
                    db.CAT_SOLICITUD.Remove(Blibro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                   

                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Back()
        {
            // Redirecionamiento al Menu
            Response.Redirect("http://localhost:53651/Home/Index/22");
            return View();
        }

    }
}