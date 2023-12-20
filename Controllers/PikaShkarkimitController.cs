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
                        PikaShkarkimiId = id,
                        CreatedDate = editing.CreatedDate,
                        UpdatedDate = DateTime.Now
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
        public IActionResult RaportPika(string searchString, DateTime searchFirstTime, DateTime searchSecondTime)
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

            var pikaRrugas = _context.PikaRrugas.Include(e => e.PikaShkarkimi).Include(e => e.Rruga).Include(e => e.PikaRrugaPagesa).ThenInclude(e => e.Currency)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .Where(m =>  searchString != null ? m.PikaShkarkimi.Emri.Contains(searchString) : true)
            .ToList();

            foreach (var rruga in pikaRrugas)
            {
                if (rruga.PikaRrugaPagesa.Count > 0)
                {
                    foreach (var fitim in rruga.PikaRrugaPagesa)
                    {
                        if (fitim.Pagesa == 0) continue;
                        var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                        rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                        if (fitim.PagesaKryer)
                        {
                            rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + fitim.Pagesa;
                        }
                        rrugaFitim.Emri = rruga.PikaShkarkimi?.Emri;

                        Llogari llogari = new Llogari()
                        {
                            Pagesa = fitim.Pagesa,
                            Tipi = TIPI.RRUGE,
                            CreatedDate = rruga.CreatedDate,
                            Pershkrim = "Fitime " + rruga.PikaShkarkimi?.Emri + " Nga Rruga " + rruga.Rruga.Emri ,
                            Currency = fitim.Currency.CurrencyUnit,
                            Shenime = rruga.Rruga.shenime,
                            PagesaKryer = fitim.PagesaKryer

                        };
                        llogaris.Add(llogari);
                    }
                }
            }
            var gjendja = _context.ZbritShtoGjendjas.Include(e => e.PikaShkarkimi).Include(e => e.Currency)
                .Where(e=>e.PikaShkarkimiId!= null)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .Where(m =>  searchString != null ? m.PikaShkarkimi.Emri.Contains(searchString) : true)
            .ToList();

            foreach (var fitim in gjendja)
            {
                if (fitim.Pagesa == 0) continue;
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + fitim.Pagesa;
                rrugaFitim.Emri = fitim.PikaShkarkimi?.Emri;

                Llogari llogari = new Llogari()
                {
                    Pagesa = fitim.Pagesa,
                    Tipi = TIPI.RRUGE,
                    CreatedDate = fitim.CreatedDate,
                    Pershkrim = "ndryshim gjendje " + fitim.PikaShkarkimi?.Emri,
                    Currency = fitim.Currency.CurrencyUnit,
                    Shenime = fitim.Shenime,
                    PagesaKryer = true


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
        }
        public enum TIPI
        {
            RRUGE,
            NAFTA,
            NDRYSHIMGJENDJE
        }
    }
}
