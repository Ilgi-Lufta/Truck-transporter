using BioLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                editing.UpdatedDate = DateTime.Now;
              //  editing.Pagesa = marrngaadd.Pagesa;
               
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
        public IActionResult RaportShofer(string searchString, DateTime searchFirstTime, DateTime searchSecondTime)
        {
            var Currencys = _context.Currencys.ToList();
            List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();
            List<Llogari> llogaris = new List<Llogari>();

            foreach (var item in Currencys)
            {
                var Currency = _context.Currencys.FirstOrDefault(e => e.CurrencyId == item.CurrencyId);
                RrugaFitime rrugaFitim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    Currency = Currency,
                    //   ShpenzimXhiro = false,
                    Pagesa = 0
                };
                rrugaFitime.Add(rrugaFitim);
            }

            var pikaRrugas = _context.ShoferRrugas.Include(e => e.Shofer).Include(e => e.Rruga).Where(e=>e.Rruga.Model==false).Include(e => e.PagesaShoferits).ThenInclude(e => e.Currency)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .Where(m =>  searchString != null ? m.Shofer.Emri.Contains(searchString) : true)
            .ToList();

            foreach (var rruga in pikaRrugas)
            {
                if (rruga.PagesaShoferits.Count > 0)
                {
                    foreach (var fitim in rruga.PagesaShoferits)
                    {
                        if (fitim.Pagesa == 0) continue;
                        var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                        rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                        if (fitim.PagesaKryer)
                        {
                            rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + fitim.Pagesa;
                        }
                        rrugaFitim.Emri = rruga.Shofer?.Emri;

                        Llogari llogari = new Llogari()
                        {
                            Pagesa = fitim.Pagesa,
                            Tipi = TIPI.RRUGE,
                            CreatedDate = rruga.CreatedDate,
                            Pershkrim = "Shoferi " + rruga.Shofer?.Emri + ", Rruga " + rruga.Rruga?.Emri,
                            Currency = fitim.Currency.CurrencyUnit,
                             Shenime = rruga.Rruga.shenime,
                            PagesaKryer = fitim.PagesaKryer,
                            RrugaNaftaID = rruga.RrugaId
                        };
                        llogaris.Add(llogari);
                    }
                }
            }
            var gjendja = _context.ZbritShtoGjendjas.Include(e => e.Shofer).Include(e => e.Currency)
                .Where(e => e.ShoferId != null)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .Where(m =>  searchString != null ? m.Shofer.Emri.Contains(searchString) : true)
            .ToList();

            foreach (var fitim in gjendja)
            {
                if (fitim.Pagesa == 0) continue;
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + fitim.Pagesa;
                rrugaFitim.Emri = fitim.Shofer?.Emri;
                Llogari llogari = new Llogari()
                {
                    Pagesa = fitim.Pagesa,
                    Tipi = TIPI.NDRYSHIMGJENDJE,
                    CreatedDate = fitim.CreatedDate,
                    Pershkrim = "ndryshim gjendje " + fitim.Shofer?.Emri,
                    Currency = fitim.Currency.CurrencyUnit,
                     Shenime = fitim.Shenime,
                    PagesaKryer = true,
                    RrugaNaftaID = fitim.ZbritShtoGjendjaId


                };
                llogaris.Add(llogari);
            }


            ViewBag.Totali = rrugaFitime;
            ViewBag.Llogari = llogaris.OrderBy(e => e.CreatedDate);

            return View();
        }
        public class Llogari
        {
            public string Pershkrim { get; set; }
            public decimal Pagesa { get; set; }
            public bool PagesaKryer { get; set; }
            public DateTime CreatedDate { get; set; }
            public TIPI Tipi { get; set; }
            public string Currency { get; set; }

            public string Shenime { get; set; }
            public int RrugaNaftaID { get; set; }

        }
        public enum TIPI
        {
            RRUGE,
            NAFTA,
            NDRYSHIMGJENDJE
        }
    }
}
