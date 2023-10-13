using BioLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BioLab.Controllers
{
    public class ShoferController : Controller
    {
        private readonly ILogger<ShoferController> _logger;
        private MyContext _context;

        public ShoferController(ILogger<ShoferController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult AllShofer(string searchString)
        {
            var shofers = _context.Shofers.ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = _context.Shofers.ToList();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Shofers = shofers.Where(s => s.Emri!.Contains(searchString));
            }
            return View();
        }
        public IActionResult AddShofer()
        {
            var AllCurrency = _context.Currencys.ToList();
            if (AllCurrency != null)
            {
                List<SelectListItem> currencys = new List<SelectListItem>();
                foreach (var currency in AllCurrency)
                {
                    currencys.Add(new SelectListItem { Text = currency.CurrencyUnit, Value = currency.CurrencyId.ToString() });
                }
                ViewBag.currencys = currencys;
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateShofer(Shofer marrngaadd)
        {
           
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Shofers.Any(u => u.Emri == marrngaadd.Emri) )
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return View("AddShofer");
                }
                //vendosim lidhjen one to many per analizat e adminit te loguar
                // dhe e ruajm analizesn ne db
                
                _context.Add(marrngaadd);
                _context.SaveChanges();
                return RedirectToAction("AllShofer");
            }
            return View("AddShofer");
        }

        public IActionResult EditShofer(int id)
        {
            ViewBag.id = id;
            Shofer Editing = _context.Shofers.FirstOrDefault(p => p.ShoferId == id);

            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedShofer(int id, Shofer marrngaadd)
        {
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Shofers.Where(sh=>sh.ShoferId!=id).Any(u => u.Emri == marrngaadd.Emri))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "kziston nje user me kete emer!");

                    return RedirectToAction("EditShofer", new { id = id });
                }
                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                Shofer editing = _context.Shofers.FirstOrDefault(p => p.ShoferId == id);
                editing.Emri = marrngaadd.Emri;
                editing.Pagesa = marrngaadd.Pagesa;
               
                _context.SaveChanges();
                return RedirectToAction("AllShofer");
            }
            return RedirectToAction("EditShofer", new { id = id });
        }

        public IActionResult FshiShofer(int id)
        {
        
            //fshijme analizen e marre nga db me analizId si parametri id
            Shofer removingShofer = _context.Shofers.FirstOrDefault(p => p.ShoferId == id);
            _context.Shofers.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllShofer");

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
