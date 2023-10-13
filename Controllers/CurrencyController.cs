using BioLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace BioLab.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;
        private MyContext _context;

        public CurrencyController(ILogger<CurrencyController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult AddCurrency()
        {
            return View();
        }
        public IActionResult AllCurrency()
        {
            var shofers = _context.Currencys.ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateCurrency(Currency marrngaadd)
        {
           
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Currencys.Any(u => u.CurrencyUnit == marrngaadd.CurrencyUnit) )
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return View("AddCurrency");
                }
                //vendosim lidhjen one to many per analizat e adminit te loguar
                // dhe e ruajm analizesn ne db
             
                _context.Add(marrngaadd);
                _context.SaveChanges();
                return RedirectToAction("AllCurrency");
            }
            return View("AddCurrency");
        }

        public IActionResult EditCurrency(int id)
        {
            ViewBag.id = id;
            Currency Editing = _context.Currencys.FirstOrDefault(p => p.CurrencyId == id);

            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedCurrency(int id, Currency marrngaadd)
        {
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Currencys.Where(sh=>sh.CurrencyId != id).Any(u => u.CurrencyUnit == marrngaadd.CurrencyUnit))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return RedirectToAction("EditCurrency", new { id = id });
                }
                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                Currency editing = _context.Currencys.FirstOrDefault(p => p.CurrencyId == id);
                editing.CurrencyUnit = marrngaadd.CurrencyUnit;

               
                _context.SaveChanges();
                return RedirectToAction("AllCurrency");
            }
            return RedirectToAction("EditCurrency", new { id = id });
        }

        public IActionResult FshiCurrency(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            Currency removingShofer = _context.Currencys.FirstOrDefault(p => p.CurrencyId == id);
            _context.Currencys.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllCurrency");

        }
    }
}
