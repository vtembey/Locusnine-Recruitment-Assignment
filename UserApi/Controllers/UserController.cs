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
		
		ResponseType(typeof(void))]
        public IHttpActionResult PatchEmail(USERTBL uSERTBL)
        {
            string sqlUpdate;           

            int intNameLength = uSERTBL.FULL_NAME.SubstringUpToFirst(' ').Length;

            var nameCount = (from row in db.USERTBLs
                         where row.FULL_NAME.Substring(0, intNameLength) == uSERTBL.FULL_NAME.Substring(0, intNameLength)
                         select row).Count();
            
            if (Convert.ToInt32(nameCount) == 1)
            {
                int user_pk = (from row in db.USERTBLs
                               select (row.USER_PK)).Max();

                sqlUpdate = "UPDATE LocusNine.dbo.USERTBL " +
                                    "SET USERTBL.EMAIL_ID = " + "\'" + uSERTBL.FULL_NAME.Substring(0, intNameLength).ToLower() + "@locusnine.com" + "\'" +
                                    " WHERE USERTBL.USER_PK=@user_pk";
                db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@user_pk", user_pk));
            }
            else
            {
                sqlUpdate = "UPDATE LocusNine.dbo.USERTBL " +
                                    "SET USERTBL.EMAIL_ID = " + "\'" +(uSERTBL.FULL_NAME.Substring(0, intNameLength).ToLower()+ "+" + (nameCount-1)+"@locusnine.com") + "\'" +
                                    " WHERE USERTBL.USER_PK=@user_pk";
                db.Database.ExecuteSqlCommand(sqlUpdate, new SqlParameter("@user_pk", uSERTBL.USER_PK));
            }                     

            return StatusCode(HttpStatusCode.NoContent);
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