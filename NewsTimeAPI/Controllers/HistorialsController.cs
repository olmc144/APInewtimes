﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewsTimeAPI.Models;

namespace NewsTimeAPI.Controllers
{
    public class HistorialsController : ApiController
    {
        private news_timeEntities3 db = new news_timeEntities3();

        // GET: api/Historials
        public IHttpActionResult GetHistorial()
        {
            var historialciudad = (from h in db.Historial
                                  join c in db.Ciudades on h.hstIDciudad equals c.CiudadID
                                  select c ).ToList();

            return Ok(historialciudad);
        }

        // GET: api/Historials/5
        [ResponseType(typeof(Historial))]
        public IHttpActionResult GetHistorial(int id)
        {
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }

            return Ok(historial);
        }

        // PUT: api/Historials/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHistorial(int id, Historial historial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historial.IDHistorial)
            {
                return BadRequest();
            }

            db.Entry(historial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Historials
        [ResponseType(typeof(Historial))]
        public IHttpActionResult PostHistorial(Historial historial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Historial.Add(historial);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HistorialExists(historial.IDHistorial))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = historial.IDHistorial }, historial);
        }

        // DELETE: api/Historials/5
        [ResponseType(typeof(Historial))]
        public IHttpActionResult DeleteHistorial(int id)
        {
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }

            db.Historial.Remove(historial);
            db.SaveChanges();

            return Ok(historial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HistorialExists(int id)
        {
            return db.Historial.Count(e => e.IDHistorial == id) > 0;
        }
    }
}