using BioLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BioLab.Controllers
{
    public class GjendjaController : Controller
    {
        private readonly ILogger<GjendjaController> _logger;
        private MyContext _context;

        public GjendjaController(ILogger<GjendjaController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AllGjendja(DateTime searchFirstTime, DateTime searchSecondTime)
        {
            //var shofers = _context.Naftas
            //         .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
            //        .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
            //    .ToList();

            //if (shofers != null)
            //{
            //    ViewBag.Shofers = shofers;
            //}
            //var naftaShitur = _context.Naftas.Where(b => b.BlereShiturSelect == "Blere")
            //      .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
            //        .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

            //        .ToList();
            //var naftaBlere = _context.Naftas.Where(b => b.BlereShiturSelect == "Shitur")
            //      .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
            //        .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

            //        .ToList();
            //ViewBag.RefPrice = _context.Naftas
            //     //.Where(e => e.Litra > 0 && e.Leke > 0)
            //     .GroupBy(e => e.BlereShiturSelect == "Blere")
            //     .Select(e =>
            //     (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
            //             )
            //     .FirstOrDefault();

            ViewBag.Shofers = _context.ZbritShtoGjendjas.Include(e=>e.Currency).OrderBy(e=>e.CreatedDate).ToList();

            var Currencys = _context.Currencys.ToList();
            List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();

            foreach (var item in Currencys)
            {
                var Currency = _context.Currencys.FirstOrDefault(e => e.CurrencyId == item.CurrencyId);
                RrugaFitime rrugaFitim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    Currency = Currency,
                    ShpenzimXhiro = false,
                    Pagesa = 0
                };
                rrugaFitime.Add(rrugaFitim);
            }
            //rruga
            //var rrugas =  _context.Rrugas
            //    //.Include(e => e.Shofer)
            //    //.Include(e => e.PikaShkarkimi)
            //    //.Include(e=>e.Nafta).ThenInclude(e => e.Currency)
            //    //.Include(e=>e.PagesaDoganas).ThenInclude(e=>e.Currency)
            //    .Include(e => e.ShoferRrugas).ThenInclude(e => e.Shofer)
            //    .Include(e => e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi)
            //    //.Include(e=>e.RrugaShpenzimeEkstras).ThenInclude(e=>e.Currency)
            //    //.Include(e=>e.RrugaFitimeEkstras).ThenInclude(e=>e.Currency)
            //    .Include(e => e.RrugaFitimes).ThenInclude(e => e.Currency)
            //    .Where(e => e.Model == false)
            //                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
            //        .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)
            //    .ToList();

            //foreach (var rruga in rrugas)
            //{
            //    if (rruga.RrugaFitimes.Count > 0)
            //    {
            //        foreach (var fitim in rruga.RrugaFitimes)
            //        {
            //            var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
            //            rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
            //        }
            //    }
            //}

            //gjendja
            var gjendjas = _context.ZbritShtoGjendjas.Include(e=>e.Currency).ToList();

            foreach (var gjendja in gjendjas)
            {
                if (gjendja.ZbritShtoSelect == "Zbrit")
                {
                        var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == gjendja.CurrencyId);
                        rrugaFitim.Pagesa = rrugaFitim.Pagesa - gjendja.Pagesa;
                }
                else
                {
                    var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == gjendja.CurrencyId);
                    rrugaFitim.Pagesa = rrugaFitim.Pagesa + gjendja.Pagesa;
                }
            }

            ////nafta
            //var naftaStocksShitur = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Shitur").ToList();
            //foreach (var naftaStock in naftaStocksShitur)
            //{
            //    var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == naftaStock.CurrencyId);
            //    rrugaFitim.Pagesa = rrugaFitim.Pagesa + naftaStock.Pagesa;
            //}
            //var naftaStocksBlere = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere" && e.Pagesa>0).ToList();
            //foreach (var naftaStock in naftaStocksBlere)
            //{
            //    var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == naftaStock.CurrencyId);
            //    rrugaFitim.Pagesa = rrugaFitim.Pagesa - naftaStock.Pagesa;
            //}

            ViewBag.Totali = rrugaFitime;


            return View();
        }

        public IActionResult AddGjendja()
        {
            List<SelectListItem> blereShitur = new List<SelectListItem>();


            blereShitur.Add(new SelectListItem
            {
                Text = "Zbrit",
                Value = "Zbrit",
                Selected = true,

            });

            blereShitur.Add(new SelectListItem
            {
                Text = "Shto",
                Value = "Shto",

            }
           );

            ViewBag.blereShitur = blereShitur;

            var AllCurrency2 = _context.Currencys.ToList();
            if (AllCurrency2 != null)
            {
               // IDictionary<int, string> numberNames = new Dictionary<int, string>();
                List<SelectListItem> numberNames = new List<SelectListItem>();
                foreach (var currency in AllCurrency2)
                {
                    numberNames.Add(new SelectListItem
                    {
                        Text = currency.CurrencyUnit,
                        Value = currency.CurrencyId.ToString()

                    });
                }
                ViewBag.numberNames = numberNames;
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateGjendja(ZbritShtoGjendja marrngaadd)
        {


            //if(marrngaadd.BlereShiturSelect== "Shitur")
            //{
            //    var cmimRef = _context.Naftas
            //    //.Where(e => e.Litra > 0 && e.Leke > 0)
            //    .GroupBy(e => e.BlereShiturSelect == "Blere")
            //    .Select(e =>
            //    (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
            //            )
            //    .FirstOrDefault();

            //    Nafta nafta = new Nafta
            //    {
            //        BlereShiturSelect = "Blere",
            //        Litra = (0-marrngaadd.Litra),
            //        Cmimi = cmimRef,
            //        Leke= 0-(cmimRef* marrngaadd.Litra)
            //    };

            //    _context.Add(nafta);
            //    _context.SaveChanges();
            //}



            if (ModelState.IsValid)
            {
                _context.ZbritShtoGjendjas.Add(marrngaadd);
                _context.SaveChanges();
                return RedirectToAction("AllGjendja");
            }
            return View("AddGjendja");
        }
        public IActionResult EditGjendja(int id)
        {
            List<SelectListItem> blereShitur = new List<SelectListItem>();

            ZbritShtoGjendja Editing = _context.ZbritShtoGjendjas.FirstOrDefault(p => p.ZbritShtoGjendjaId == id);
            
            blereShitur.Add(new SelectListItem
            {
                Text = "Zbrit",
                Value = "Zbrit",
                Selected = Editing.ZbritShtoSelect == "Zbrit" ? true:false

            });

            blereShitur.Add(new SelectListItem
            {
                Text = "Shto",
                Value = "Shto",
                Selected = Editing.ZbritShtoSelect == "Shto" ? true : false
            }
           );
            ViewBag.blereShitur = blereShitur;
            ViewBag.id = id;
            var AllCurrency2 = _context.Currencys.ToList();
            if (AllCurrency2 != null)
            {
                // IDictionary<int, string> numberNames = new Dictionary<int, string>();
                List<SelectListItem> numberNames = new List<SelectListItem>();
                foreach (var currency in AllCurrency2)
                {
                    numberNames.Add(new SelectListItem
                    {
                        Text = currency.CurrencyUnit,
                        Value = currency.CurrencyId.ToString()

                    });
                }
                ViewBag.numberNames = numberNames;
            }



            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedGjendja(int id, ZbritShtoGjendja marrngaadd)
        {

            if (ModelState.IsValid)
            {

                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                ZbritShtoGjendja editing = _context.ZbritShtoGjendjas.FirstOrDefault(p => p.ZbritShtoGjendjaId == id);
                // editing.Cmimi = marrngaadd.Cmimi;
                editing.Shenime = marrngaadd.Shenime;
                editing.ZbritShtoSelect = marrngaadd.ZbritShtoSelect;
                editing.CurrencyId = marrngaadd.CurrencyId;
                editing.Pagesa = marrngaadd.Pagesa;

                _context.SaveChanges();
                return RedirectToAction("AllGjendja");
            }
            return RedirectToAction("EditGjendja", new { id = id });
        }
        public IActionResult FshiGjendja(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            ZbritShtoGjendja removingShofer = _context.ZbritShtoGjendjas.FirstOrDefault(p => p.ZbritShtoGjendjaId == id);
            _context.ZbritShtoGjendjas.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllGjendja");

        }
        public IActionResult GjendjaRaport(DateTime searchFirstTime, DateTime searchSecondTime)
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

           // rruga
            var rrugas = _context.Rrugas
                .Include(e => e.ShoferRrugas).ThenInclude(e => e.Shofer)
                .Include(e => e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi)
                .Include(e => e.RrugaFitimes).ThenInclude(e => e.Currency)
                .Where(e => e.Model == false)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();

            foreach (var rruga in rrugas)
            {
                if (rruga.RrugaFitimes.Count > 0)
                {
                    foreach (var fitim in rruga.RrugaFitimes)
                    {
                        if(fitim.Pagesa==0) continue;
                        var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                        rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                        rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + fitim.PagesaReale;

                        Llogari llogari = new Llogari()
                        {
                            Pagesa = fitim.Pagesa,
                            Tipi = TIPI.RRUGE,
                            CreatedDate = rruga.CreatedDate,
                            Pershkrim = "Fitime Nga Rruga " + rruga.Emri,
                            Currency= fitim.Currency.CurrencyUnit
                        };
                        llogaris.Add( llogari );
                    }
                }
            }

            //gjendja
            var gjendjas = _context.ZbritShtoGjendjas.Include(e => e.Currency)
                 .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();

            foreach (var gjendja in gjendjas)
            {
                    if (gjendja.Pagesa == 0) continue;
                if (gjendja.ZbritShtoSelect == "Zbrit")
                {
                    var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == gjendja.CurrencyId);
                    rrugaFitim.Pagesa = rrugaFitim.Pagesa - gjendja.Pagesa;
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale - gjendja.Pagesa;

                    Llogari llogari = new Llogari()
                    {
                        Pagesa = (0-gjendja.Pagesa),
                        Tipi = TIPI.NDRYSHIMGJENDJE,
                        CreatedDate = gjendja.CreatedDate,
                        Pershkrim = "Zbritje nga gjendja",
                        Currency = gjendja.Currency.CurrencyUnit
                    };
                    llogaris.Add(llogari);
                }
                else
                {

                    var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == gjendja.CurrencyId);
                    rrugaFitim.Pagesa = rrugaFitim.Pagesa + gjendja.Pagesa;
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + gjendja.Pagesa;
                    Llogari llogari = new Llogari()
                    {
                        Pagesa = gjendja.Pagesa,
                        Tipi = TIPI.NDRYSHIMGJENDJE,
                        CreatedDate = gjendja.CreatedDate,
                        Pershkrim = "shtim tek gjendja",
                        Currency = gjendja.Currency.CurrencyUnit

                    };
                    llogaris.Add(llogari);
                }
            }

            //nafta
            var naftaStocksShitur = _context.NaftaStocks.Include(e=>e.Currency)
                .Where(e => e.BlereShiturSelect == "Shitur" && e.RrugaId==null)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();
            foreach (var naftaStock in naftaStocksShitur)
            {
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == naftaStock.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa + naftaStock.Pagesa;
                if (naftaStock.PagesaKryer)
                {
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + naftaStock.Pagesa;
                }
                Llogari llogari = new Llogari()
                {
                    Pagesa = naftaStock.Pagesa,
                    Tipi = TIPI.NAFTA,
                    CreatedDate = naftaStock.CreatedDate,
                    Pershkrim = "Shtije nafte",
                    Currency = naftaStock.Currency.CurrencyUnit

                };
                llogaris.Add(llogari);

            }

            var naftaStocksBlere = _context.NaftaStocks.Include(e => e.Currency)
                .Where(e => e.BlereShiturSelect == "Blere" && e.Pagesa > 0 && e.RrugaId == null)
                .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();

            foreach (var naftaStock in naftaStocksBlere)
            {
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == naftaStock.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa - naftaStock.Pagesa;
                if (naftaStock.PagesaKryer)
                {
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale - naftaStock.Pagesa;
                }
                Llogari llogari = new Llogari()
                {
                    Pagesa = (0- naftaStock.Pagesa),
                    Tipi = TIPI.NAFTA,
                    CreatedDate = naftaStock.CreatedDate,
                    Pershkrim = "Blerje nafte",
                    Currency = naftaStock.Currency.CurrencyUnit

                };
                llogaris.Add(llogari);
            }
            ViewBag.Totali = rrugaFitime;
            ViewBag.Llogari = llogaris.OrderBy(e => e.CreatedDate);
            return View();
        }
        public class Llogari
        {
            public string  Pershkrim { get; set; }
            public decimal  Pagesa { get; set; }
            public DateTime CreatedDate { get; set; }
            public TIPI Tipi { get; set; }  
            public string Currency { get; set; }  
        }
        public enum TIPI
        {
            RRUGE,
            NAFTA,
            NDRYSHIMGJENDJE
        }



    }
}
