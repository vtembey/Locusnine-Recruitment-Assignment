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
using UserAPI.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;

namespace UserAPI.Controllers
{
    public class UserController : ApiController
    {
        private LocusNineEntities db = new LocusNineEntities();

        // GET: api/User
        public IQueryable<USERTBL> GetUsers()
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
        public IHttpActionResult PutUSER( USERTBL uSERTBL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != uSERTBL.USER_PK)
            //{
            //    return BadRequest();
            //}

            db.Entry(uSERTBL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERTBLExists(uSERTBL.USER_PK))
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

        //[System.Web.Http.HttpPatch]
        //public IHttpActionResult PatchEmail(USERTBL uSERTBL)
        //{

        //}

        // POST: api/User
        [ResponseType(typeof(USERTBL))]
        public IHttpActionResult PostUSER(USERTBL uSERTBL)
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
        public IHttpActionResult DeleteUSER(int id)
        {
            USERTBL uSERTBL = db.USERTBLs.Find(id);
            if (uSERTBL == null)
            {
                return NotFound();
                //return false;
            }

            db.USERTBLs.Remove(uSERTBL);
            db.SaveChanges();

            return Ok(uSERTBL);
            //return true;
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