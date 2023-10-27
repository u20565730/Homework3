using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using u20565730_HWK03.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace u20565730_HWK03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult> Index(int? page, int? page2, int? page3)
        {
            int pagesize = 10;
            int pagenumber = (page ?? 1);
            int pagenumber2 = (page2 ?? 1);
            int pagenumber3 = (page3 ?? 1);
            var x = await db.books.Include(b => b.author).Include(b => b.type).ToListAsync();
            var y = await db.students.ToListAsync();
            var viewModel = new CombinedBooks
            {
                Book = x.ToPagedList(pagenumber, pagesize),
                Student = y.ToPagedList(pagenumber2, pagesize),
                Borrow = await db.borrows.Include(b => b.book.author).Include(b => b.book.type).Include(b => b.student).ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Maintain(int? page, int? page2, int? page3)
        {
            int pagesize = 10;
            int pagenumber = (page ?? 1);
            int pagenumber2 = (page2 ?? 1);
            int pagenumber3 = (page3 ?? 1);
            var x = await db.authors.ToListAsync();
            var y = await db.borrows.Include(b => b.book).Include(b => b.student).ToListAsync();
            var z = await db.types.ToListAsync();

            var viewModel2 = new CombinedBorrows
            {

                Author = x.ToPagedList(pagenumber, pagesize),
                Borrow = y.ToPagedList(pagenumber2, pagesize),
                Type = z.ToPagedList(pagenumber3, pagesize)
            };

            return View(viewModel2);
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your application description page.";


            return View();
        }

        [HttpGet]
        public JsonResult ChartData()
        {
            var report = db.borrows.GroupBy(r => r.student.gender).Select(group => new
            {
                TypeName = group.Key,
                Count = group.Count()
            }).ToList(); // Ensure that the data is materialized as a list

            // Create an object with two arrays for labels and values
            var chartData = new
            {
                TypeName = report.Select(r => r.TypeName).ToArray(),
                Count = report.Select(r => r.Count).ToArray()
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }


    }
}