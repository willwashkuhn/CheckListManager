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
    public class CheckListItemsController : ApiController
    {
        private CheckListManagerContext db = new CheckListManagerContext();

        // GET: api/CheckListItems
        public IQueryable<CheckListItem> GetCheckListItems()
        {
            return db.CheckListItems;
        }

        // GET: api/CheckListItems/5
        [ResponseType(typeof(CheckListItem))]
        public IHttpActionResult GetCheckListItem(int id)
        {
            CheckListItem checkListItem = db.CheckListItems.Find(id);
            if (checkListItem == null)
            {
                return NotFound();
            }

            return Ok(checkListItem);
        }

        // GET: api/checklists/1/checklistitems
        [ResponseType(typeof(CheckList))]
        [Route("api/checklists/{id}/checklistitems")]
        public IHttpActionResult GetCheckListItemsByCheckList(int id)
        {
            List<CheckListItem> checkListItems = db.CheckListItems.Where(i => i.CheckListId == id).ToList();
            if (checkListItems == null)
            {
                return NotFound();
            }

            return Ok(checkListItems);
        }

        // PUT: api/CheckListItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckListItem(int id, int ord, CheckListItem checkListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(checkListItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListItemExists(id, ord))
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

        // POST: api/CheckListItems
        [ResponseType(typeof(CheckListItem))]
        public IHttpActionResult PostCheckListItem(CheckListItem checkListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckListItems.Add(checkListItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checkListItem.CheckListId, checkListItem.Order }, checkListItem);
        }

        // DELETE: api/CheckListItems/5
        [ResponseType(typeof(CheckListItem))]
        public IHttpActionResult DeleteCheckListItem(int id)
        {
            CheckListItem checkListItem = db.CheckListItems.Find(id);
            if (checkListItem == null)
            {
                return NotFound();
            }

            db.CheckListItems.Remove(checkListItem);
            db.SaveChanges();

            return Ok(checkListItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckListItemExists(int id, int ord)
        {
            return db.CheckListItems.Count(e => e.CheckListId == id && e.Order == ord ) > 0;
        }
    }
}