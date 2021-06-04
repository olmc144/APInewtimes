using System;
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
    public class CiudadesController : ApiController
    {
        private news_timeEntities3 db = new news_timeEntities3();

        // GET: api/Ciudades
        public IQueryable<Ciudades> GetCiudades()
        {
            return db.Ciudades.Take(100);
        }

        // GET: api/Ciudades/5
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult GetCiudades(int id)
        {
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return Ok(ciudades);
        }

        // PUT: api/Ciudades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiudades(int id, Ciudades ciudades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ciudades.CiudadID)
            {
                return BadRequest();
            }

            db.Entry(ciudades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadesExists(id))
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

        // POST: api/Ciudades
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult PostCiudades(Ciudades ciudades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ciudades.Add(ciudades);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CiudadesExists(ciudades.CiudadID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ciudades.CiudadID }, ciudades);
        }

        // DELETE: api/Ciudades/5
        [ResponseType(typeof(Ciudades))]
        public IHttpActionResult DeleteCiudades(int id)
        {
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return NotFound();
            }

            db.Ciudades.Remove(ciudades);
            db.SaveChanges();

            return Ok(ciudades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CiudadesExists(int id)
        {
            return db.Ciudades.Count(e => e.CiudadID == id) > 0;
        }
    }
}