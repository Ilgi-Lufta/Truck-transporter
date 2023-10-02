using BioLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace BioLab.Controllers
{
    public class PikaShkarkimitController : Controller
    {
        private readonly ILogger<PikaShkarkimitController> _logger;
        private MyContext _context;

        public PikaShkarkimitController(ILogger<PikaShkarkimitController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult AddPika()
        {
            return View();
        }
        public IActionResult AllPika(string searchString ,DateTime searchFirstTime, DateTime searchSecondTime)
        {
            var shofers = _context.PikaShkarkimis
                    .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)
                .ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = _context.PikaShkarkimis.ToList();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Shofers = shofers.Where(s => s.Emri!.Contains(searchString))
                                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true);
            }
          

            return View();
        }

        [HttpPost]
        public IActionResult CreatePika(PikaShkarkimi marrngaadd)
        {
           
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.PikaShkarkimis.Any(u => u.Emri == marrngaadd.Emri) )
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return View("AddPika");
                }
                //vendosim lidhjen one to many per analizat e adminit te loguar
                // dhe e ruajm analizesn ne db
                
                _context.Add(marrngaadd);
                _context.SaveChanges();
                return RedirectToAction("AllPika");
            }
            return View("AddPika");
        }

        public IActionResult EditPika(int id)
        {
            ViewBag.id = id;
            PikaShkarkimi Editing = _context.PikaShkarkimis.FirstOrDefault(p => p.PikaShkarkimiId == id);

            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedPika(int id,PikaShkarkimi marrngaadd)
        {
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.PikaShkarkimis.Where(sh=>sh.PikaShkarkimiId!=id).Any(u => u.Emri == marrngaadd.Emri))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return RedirectToAction("EditPika", new { id = id });
                }
                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                PikaShkarkimi editing = _context.PikaShkarkimis.FirstOrDefault(p => p.PikaShkarkimiId == id);
                editing.Emri = marrngaadd.Emri;
                editing.Pagesa = marrngaadd.Pagesa;
               
                _context.SaveChanges();
                return RedirectToAction("AllPika");
            }
            return RedirectToAction("EditPika", new { id = id });
        }

        public IActionResult FshiPika(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            PikaShkarkimi removingShofer = _context.PikaShkarkimis.FirstOrDefault(p => p.PikaShkarkimiId == id);
            _context.PikaShkarkimis.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllPika");

        }
        //public IActionResult Search(string searchString)
        //{
        //    var Shofer = _context.Shofers.ToList();
        //    ViewBag.Shofer = Shofer;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        ViewBag.Shofer = Shofer.Where(s => s.Emri!.Contains(searchString));
        //    }
        //    return View("AllShofer");
        //}
    }
}
