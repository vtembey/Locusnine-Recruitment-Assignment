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
using UserApi.Models;

namespace UserApi.Controllers
{
    public class UserController : ApiController
    {
        private LocusNineEntities db = new LocusNineEntities();

        // GET: api/User
        public IQueryable<USERTBL> GetUSERTBLs()
        {
            return db.USERTBLs;
        }

        // GET: api/User/5
        [ResponseType(typeof(USERTBL))]
        public IHttpActionResult GetUSERTBL(int id)
        {
            USERTBL uSERTBL = db.USERTBLs.Find(id);
            if (uSERTBL == null)
            {
                return NotFound();
            }

            return Ok(uSERTBL);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUSERTBL(int id, USERTBL uSERTBL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSERTBL.USER_PK)
            {
                return BadRequest();
            }

            db.Entry(uSERTBL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERTBLExists(id))
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

        // POST: api/User
        [ResponseType(typeof(USERTBL))]
        public IHttpActionResult PostUSERTBL(USERTBL uSERTBL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USERTBLs.Add(uSERTBL);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSERTBL.USER_PK }, uSERTBL);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(USERTBL))]
        public IHttpActionResult DeleteUSERTBL(int id)
        {
            USERTBL uSERTBL = db.USERTBLs.Find(id);
            if (uSERTBL == null)
            {
                return NotFound();
            }

            db.USERTBLs.Remove(uSERTBL);
            db.SaveChanges();

            return Ok(uSERTBL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERTBLExists(int id)
        {
            return db.USERTBLs.Count(e => e.USER_PK == id) > 0;
        }
    }
}