using BioLab.Models;
using BioLab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult AllPika(string searchString ,DateTime searchFirstTime, DateTime searchSecondTime)
        {
            var shofers = _context.PikaShkarkimis.Include(e=>e.PagesaPikaShkarkimits).ThenInclude(e=>e.Currency)
                    .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = _context.PikaShkarkimis.ToList();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Shofers = shofers.Where(s => s.Emri!.Contains(searchString))
                                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true);
            }
          

            return View();
        }
        public IActionResult AddPika()
        {
            var AllCurrency = _context.Currencys.ToList();
            if (AllCurrency != null)
            {
                IDictionary<int, string> numberNames = new Dictionary<int, string>();
                foreach (var currency in AllCurrency)
                {
                    numberNames.Add(currency.CurrencyId, currency.CurrencyUnit);
                }
                ViewBag.numberNames = numberNames;
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreatePika(PikaShkarkimi marrngaadd)
        {
            marrngaadd.PagesaPikaShkarkimitsVM = marrngaadd.PagesaPikaShkarkimitsVM.Where(e=>e.CurrencyId!=0).ToList();

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

                marrngaadd.PagesaPikaShkarkimits = new List<PagesaPikaShkarkimit>();
                foreach (var PagesaPikaShkarkimitsVM in marrngaadd.PagesaPikaShkarkimitsVM)
                {
                    PagesaPikaShkarkimit PagesaPikaShkarkimit = new PagesaPikaShkarkimit()
                    {
                        CurrencyId = PagesaPikaShkarkimitsVM.CurrencyId,
                        Pagesa = PagesaPikaShkarkimitsVM.Pagesa,
                        PagesaKryer = PagesaPikaShkarkimitsVM.PagesaKryer,
                        PikaShkarkimiId = marrngaadd.PikaShkarkimiId
                    };
                   // marrngaadd.PagesaPikaShkarkimits.Add(PagesaPikaShkarkimit);
                _context.Add(PagesaPikaShkarkimit);
                _context.SaveChanges();
                }


                return RedirectToAction("AllPika");
            }
            return View("AddPika");
        }

        public IActionResult EditPika(int id)
        {
            ViewBag.id = id;
            PikaShkarkimi Editing = _context.PikaShkarkimis.Include(p=>p.PagesaPikaShkarkimits).FirstOrDefault(p => p.PikaShkarkimiId == id);
            var AllCurrency = _context.Currencys.ToList();
            if (AllCurrency != null)
            {
                IDictionary<int, string> numberNames = new Dictionary<int, string>();
                foreach (var currency in AllCurrency)
                {
                    numberNames.Add(currency.CurrencyId, currency.CurrencyUnit);
                }
                ViewBag.numberNames = numberNames;
            }
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
                PikaShkarkimi editing = _context.PikaShkarkimis.Include(p=>p.PagesaPikaShkarkimits).FirstOrDefault(p => p.PikaShkarkimiId == id);
                List<int> pagpikashkarkId = new List<int>();

                foreach (var item in editing.PagesaPikaShkarkimits)
                {
                   var pagpikashakrkimi = _context.PagesaPikaShkarkimits.FirstOrDefault(e => e.PagesaPikaShkarkimitId == item.PagesaPikaShkarkimitId).PagesaPikaShkarkimitId;
                    pagpikashkarkId.Add(pagpikashakrkimi);
                }
                foreach (var item in pagpikashkarkId)
                {
                   var pagpikashakrkimi = _context.PagesaPikaShkarkimits.FirstOrDefault(e => e.PagesaPikaShkarkimitId == item);

                _context.PagesaPikaShkarkimits.Remove(pagpikashakrkimi);
                }
                    _context.SaveChanges();
                editing.Emri = marrngaadd.Emri;
                //editing.Pagesa = marrngaadd.Pagesa;

                foreach (var PagesaPikaShkarkimitsVM in marrngaadd.PagesaPikaShkarkimits)
                {
                    PagesaPikaShkarkimit PagesaPikaShkarkimit = new PagesaPikaShkarkimit()
                    {
                        CurrencyId = PagesaPikaShkarkimitsVM.CurrencyId,
                        Pagesa = PagesaPikaShkarkimitsVM.Pagesa,
                        PagesaKryer = PagesaPikaShkarkimitsVM.PagesaKryer,
                        PikaShkarkimiId = id
                    };
                    // marrngaadd.PagesaPikaShkarkimits.Add(PagesaPikaShkarkimit);
                    _context.Add(PagesaPikaShkarkimit);
                    _context.SaveChanges();
                }


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
