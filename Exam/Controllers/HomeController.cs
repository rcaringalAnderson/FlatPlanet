using Exam.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private CounterDBContext db = new CounterDBContext();

        // GET: Home
        public ActionResult Index()
        {
            var counters = from s in db.Counters
                           select s;

            Counter result =  counters.Where(a => a.Id == counters.Max(c => c.Id)).First();

            Counter AddCounter;

            if (result.counter <= 9)
            {
                AddCounter = new Counter { Id = result.Id, counter = result.counter + 1, counterdate = result.counterdate };
            } else
            {
                ModelState.AddModelError("Error", "Exceeded 10 Back to 1");
                AddCounter = new Counter { Id = result.Id, counter = 1, counterdate = result.counterdate };
            }

            return View(AddCounter);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "counter")]Counter model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Counters.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(model);
        }
    }
}