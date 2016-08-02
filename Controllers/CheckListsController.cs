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
using CheckListManager.Models;

namespace CheckListManager.Controllers
{
    public class CheckListsController : ApiController
    {
        private CheckListManagerContext db = new CheckListManagerContext();

        // GET: api/CheckLists
        public IQueryable<CheckList> GetCheckLists()
        {
            return db.CheckLists;
        }

        // GET: api/CheckLists/5
        [ResponseType(typeof(CheckList))]
        public IHttpActionResult GetCheckList(int id)
        {
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return NotFound();
            }

            return Ok(checkList);
        }

        // GET: api/users/1/CheckLists
        [ResponseType(typeof(CheckList))]
        [Route("api/users/{id}/checklists")]
        public IHttpActionResult GetCheckListsByUser(int id)
        {
            List<CheckList> checkLists = db.CheckLists.Where(i => i.UserId == id).ToList();
            if (checkLists == null)
            {
                return NotFound();
            }

            return Ok(checkLists);
        }

        // PUT: api/CheckLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckList(int id, CheckList checkList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkList.CheckListId)
            {
                return BadRequest();
            }

            db.Entry(checkList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListExists(id))
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

        // POST: api/CheckLists
        [ResponseType(typeof(CheckList))]
        public IHttpActionResult PostCheckList(CheckList checkList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckLists.Add(checkList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checkList.CheckListId }, checkList);
        }

        // DELETE: api/CheckLists/5
        [ResponseType(typeof(CheckList))]
        public IHttpActionResult DeleteCheckList(int id)
        {
            CheckList checkList = db.CheckLists.Find(id);
            if (checkList == null)
            {
                return NotFound();
            }

            List<CheckListItem> checkListItems = db.CheckListItems.Where(i => i.CheckListId == id).ToList();
            foreach (var cli in checkListItems)
            {
                db.CheckListItems.Remove(cli);
            }

            db.CheckLists.Remove(checkList);
            db.SaveChanges();

            return Ok(checkList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckListExists(int id)
        {
            return db.CheckLists.Count(e => e.CheckListId == id) > 0;
        }
    }
}