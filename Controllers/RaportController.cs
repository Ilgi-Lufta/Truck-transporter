using BioLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static BioLab.Controllers.NaftaController;

namespace BioLab.Controllers
{
    public class RaportController : Controller
    {
        private readonly ILogger<GjendjaController> _logger;
        private MyContext _context;

        public RaportController(ILogger<GjendjaController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Llogaria(DateTime searchFirstTime, DateTime searchSecondTime)
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
                .ToList();
            foreach (var nafta2 in naftaStocksShitur)
            {
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == nafta2.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa + nafta2.Pagesa;
                if (nafta2.PagesaKryer)
                {
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale + nafta2.Pagesa;
                }
                Llogari llogari = new Llogari()
                {
                    Pagesa = nafta2.Pagesa,
                    Tipi = TIPI.NAFTA,
                    CreatedDate = nafta2.CreatedDate,
                    Pershkrim = "Shtije nafte",
                    Currency = nafta2.Currency.CurrencyUnit

                };
                llogaris.Add(llogari);

            }

            var naftaStocksBlere = _context.NaftaStocks.Include(e => e.Currency)
                .Where(e => e.BlereShiturSelect == "Blere" && e.Pagesa > 0 && e.RrugaId == null)
                .ToList();

            foreach (var nafta3 in naftaStocksBlere)
            {
                var rrugaFitim = rrugaFitime.FirstOrDefault(e => e.CurrencyId == nafta3.CurrencyId);
                rrugaFitim.Pagesa = rrugaFitim.Pagesa - nafta3.Pagesa;
                if (nafta3.PagesaKryer)
                {
                    rrugaFitim.PagesaReale = rrugaFitim.PagesaReale - nafta3.Pagesa;
                }
                Llogari llogari = new Llogari()
                {
                    Pagesa = (0- nafta3.Pagesa),
                    Tipi = TIPI.NAFTA,
                    CreatedDate = nafta3.CreatedDate,
                    Pershkrim = "Blerje nafte",
                    Currency = nafta3.Currency.CurrencyUnit

                };
                llogaris.Add(llogari);
            }
            ViewBag.Totali = rrugaFitime;
            ViewBag.Llogari = llogaris.OrderBy(e => e.CreatedDate);



            var shofers = _context.NaftaStocks.Include(e => e.Currency).Include(e => e.Rruga)
                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                        .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                        .OrderBy(e => e.CreatedDate)
                        .ToList();

            foreach (var shofer in shofers)
            {
                shofer.Cmimi = Math.Round(shofer.Pagesa / shofer.Litra, 2);
            }

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }


            List<NaftaStock> naftaStock = new List<NaftaStock>();

            foreach (var item in Currencys)
            {
                var Currency = _context.Currencys.FirstOrDefault(e => e.CurrencyId == item.CurrencyId);
                NaftaStock rrugaFitim = new NaftaStock()
                {
                    CurrencyId = item.CurrencyId,
                    Currency = Currency,
                    Pagesa = 0,
                    Litra = 0,
                    BlereShiturSelect = "Blere"
                };
                NaftaStock rrugaFitim2 = new NaftaStock()
                {
                    CurrencyId = item.CurrencyId,
                    Currency = Currency,
                    Pagesa = 0,
                    Litra = 0,
                    BlereShiturSelect = "Shitur"
                };
                naftaStock.Add(rrugaFitim);
                naftaStock.Add(rrugaFitim2);
            }


            var naftaBlere2 = _context.NaftaStocks.Include(e => e.Currency)
                .Where(e => e.BlereShiturSelect == "Blere")
                .GroupBy(e => e.CurrencyId)
                .Select(m =>
                new
                {
                    Monedha = m.Max(no => no.Currency.CurrencyUnit),
                    Pagesa = m.Sum(p => p.Pagesa),
                    Litra = m.Sum(p => p.Litra),
                    CmimRef = Math.Round((m.Sum(b => b.Pagesa) / m.Sum(b => b.Litra)), 2)
                }
                )
                .ToList();

            var Blere = _context.NaftaStocks.Include(c => c.Currency).Where(e => e.BlereShiturSelect == "Blere" && e.Litra > 0)
             .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
          .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
            .GroupBy(e => e.CurrencyId)
            .Select(m => new naftaPeriudh
            {
                Monedha = m.Max(e => e.Currency.CurrencyUnit),
                Litra = m.Sum(e => e.Litra),
                Pagesa = m.Sum(e => e.Pagesa)
            }).ToList();
            var Shitur = _context.NaftaStocks.Include(c => c.Currency).Where(e => e.BlereShiturSelect == "Shitur" && e.Litra > 0)
                 .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
              .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
               .GroupBy(e => e.CurrencyId)
               .Select(m => new naftaPeriudh
               {
                   Monedha = m.Max(e => e.Currency.CurrencyUnit),
                   Litra = m.Sum(e => e.Litra),
                   Pagesa = m.Sum(e => e.Pagesa)
               }).ToList();

            foreach (var item in Currencys)
            {


                var naftablere = naftaStock.FirstOrDefault(e => e.BlereShiturSelect == "Blere" && e.CurrencyId == item.CurrencyId);

                var nBlere = Blere.FirstOrDefault(e => e.Monedha == item.CurrencyUnit);
                if (nBlere != null)
                {
                    naftablere.Litra = nBlere.Litra;
                    naftablere.Pagesa = nBlere.Pagesa;
                }

                var naftaShitur = naftaStock.FirstOrDefault(e => e.BlereShiturSelect == "Shitur" && e.CurrencyId == item.CurrencyId);

                var nShitur = Shitur.FirstOrDefault(e => e.Monedha == item.CurrencyUnit);
                if (nShitur != null)
                {
                    naftaShitur.Litra = nShitur.Litra;
                    naftablere.Pagesa = nShitur.Pagesa;
                }
            }

            ViewBag.naftaBlere2 = naftaBlere2;

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
