using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using intcom.web.Models;

namespace intcom.web.Controllers
{
    public class AcessoController : Controller
    {
        private CONTOSOEntities db = new CONTOSOEntities();

        // GET: Acesso
        public ActionResult Index()
        {
            return View(db.ACESSO.ToList());
        }

        // GET: Acesso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSO.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        // GET: Acesso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acesso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LOGIN,USERNAME,SENHA,ATIVO,PERFIL,NOME,SOBRENOME")] ACESSO aCESSO)
        {
            if (ModelState.IsValid)
            {
                db.ACESSO.Add(aCESSO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCESSO);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Acesso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSO.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        // POST: Acesso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LOGIN,USERNAME,SENHA,ATIVO,PERFIL,NOME,SOBRENOME")] ACESSO aCESSO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCESSO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCESSO);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Acesso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSO.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Acesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACESSO aCESSO = db.ACESSO.Find(id);
            db.ACESSO.Remove(aCESSO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
