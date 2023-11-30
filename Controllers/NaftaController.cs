using BioLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BioLab.Controllers
{
    public class NaftaController : Controller
    {
        private readonly ILogger<NaftaController> _logger;
        private MyContext _context;

        public NaftaController(ILogger<NaftaController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AllNafta(DateTime searchFirstTime, DateTime searchSecondTime)
        {
            var shofers = _context.NaftaStocks.Include(e=>e.Currency)
                     .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();
            foreach (var shofer in shofers)
            {
                shofer.Cmimi = shofer.Pagesa / shofer.Litra;
            }

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            var naftaShitur = _context.NaftaStocks.Where(b => b.BlereShiturSelect == "Blere")
                  .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

                    .ToList();
            var naftaBlere = _context.NaftaStocks.Where(b => b.BlereShiturSelect == "Shitur")
                  .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

                    .ToList();
            //ViewBag.RefPrice = _context.Naftas
            //     //.Where(e => e.Litra > 0 && e.Leke > 0)
            //     .GroupBy(e => e.BlereShiturSelect == "Blere")
            //     .Select(e =>
            //     (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
            //             )
            //     .FirstOrDefault();



            return View();
        }

        public IActionResult AddNafta()
        {
            List<SelectListItem> blereShitur = new List<SelectListItem>();


            blereShitur.Add(new SelectListItem
            {
                Text = "Shitur",
                Value = "Shitur",
                Selected = true,

            });

            blereShitur.Add(new SelectListItem
            {
                Text = "Blere",
                Value = "Blere",

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
        public IActionResult CreateNafta(NaftaStock marrngaadd)
        {
            var litrablere = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
              //.Where(e => e.Litra > 0 && e.Leke > 0)
              .GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
              .Select(e => e.Sum(b => b.Litra)).FirstOrDefault();
            //var litraShitur = _context.Naftas.Where(e => e.BlereShiturSelect == "Shitur")
            ////.Where(e => e.Litra > 0 && e.Leke > 0)
            //.GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
            //.Select(e => e.Sum(b => b.Litra)).FirstOrDefault();
            // var litra = litrablere - litraBlereNegativ;

            if (ModelState.IsValid)
            {
                

                if (marrngaadd.BlereShiturSelect == "Shitur")
                {
                    if (litrablere < marrngaadd.Litra)
                    {
                        return View("AddNafta");
                    }
                    BlereShitur blereShitur = new BlereShitur();

                    var cmimRef = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
                    //.Where(e => e.Litra > 0 && e.Leke > 0)
                    .GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
                    .Select(e =>
                    (e.Sum(b => b.Pagesa) / e.Sum(b => b.Litra))
                            )
                    .FirstOrDefault();

                    _context.Add(blereShitur);
                    _context.SaveChanges();

                    NaftaStock nafta = new NaftaStock
                    {
                        BlereShiturSelect = "Blere",
                        Litra = (0 - marrngaadd.Litra),
                        Pagesa = 0 - (cmimRef * marrngaadd.Litra),
                        CurrencyId = marrngaadd.CurrencyId,
                        BlereShiturId = blereShitur.BlereShiturId
                    };

                    _context.Add(nafta);
                    _context.SaveChanges();
                    //  return RedirectToAction("AllNafta");
                    marrngaadd.BlereShiturId = blereShitur.BlereShiturId;
                }
                _context.NaftaStocks.Add(marrngaadd);
                _context.SaveChanges();
                //else
                //{

                //}

                return RedirectToAction("AllNafta");
            }
            return View("AddNafta");
        }
        public IActionResult EditNafta(int id)
        {
            ViewBag.id = id;
            List<SelectListItem> blereShitur = new List<SelectListItem>();
            NaftaStock Editing = _context.NaftaStocks.FirstOrDefault(p => p.NaftaStockId == id);

            blereShitur.Add(new SelectListItem
            {
                Text = "Shitur",
                Value = "Shitur",
                Selected = Editing.BlereShiturSelect == "Shitur" ? true : false

            });

            blereShitur.Add(new SelectListItem
            {
                Text = "Blere",
                Value = "Blere",
                Selected = Editing.BlereShiturSelect == "Blere" ? true : false
            });

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

           

            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedNafta(int id, NaftaStock marrngaadd)
        {
            var EditingBlereNegativ = _context.NaftaStocks.FirstOrDefault(p => p.BlereShiturId == marrngaadd.BlereShiturId && p.BlereShiturSelect == "Blere");
            var litrablere = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
                              //.Where(e => e.Litra > 0 && e.Leke > 0)
                              ////// to be discused
                              .GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
                              .Select(e => e.Sum(b => b.Litra)).FirstOrDefault();
            //var litraShitur = _context.Naftas.Where(e => e.BlereShiturSelect == "Shitur")
            ////.Where(e => e.Litra > 0 && e.Leke > 0)
            //.GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
            //.Select(e => e.Sum(b => b.Litra)).FirstOrDefault();
            //var litra = litrablere - litraShitur;
            var litraBlereNegativ = EditingBlereNegativ.Litra;

            var litra = litrablere - litraBlereNegativ;
            if (ModelState.IsValid)
            {
                if (marrngaadd.BlereShiturSelect == "Shitur")
                {
                    if (litra < marrngaadd.Litra)
                    {
                        return RedirectToAction("EditNafta", new { id = id });
                    }
                    //////to be discused

                    //get cmim  ref
                    var cmimRef = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
                       //.Where(e => e.Litra > 0 && e.Leke > 0)
                       .GroupBy(e => e.CurrencyId == marrngaadd.CurrencyId)
                       .Select(e =>
                       (e.Sum(b => b.Pagesa) / e.Sum(b => b.Litra))
                               )
                       .FirstOrDefault();
                    //get editing negativ nafta end edit




                    EditingBlereNegativ.BlereShiturSelect = "Blere";
                    EditingBlereNegativ.Litra = (0 - marrngaadd.Litra);
                    EditingBlereNegativ.Pagesa = 0 - (cmimRef * marrngaadd.Litra);
                        EditingBlereNegativ.CurrencyId = marrngaadd.CurrencyId;
                    
                }
                //edit marr nga add

                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                NaftaStock editing = _context.NaftaStocks.FirstOrDefault(p => p.NaftaStockId == id);
                    editing.Pagesa = marrngaadd.Pagesa;
                    editing.CurrencyId = marrngaadd.CurrencyId;
                    editing.Litra = marrngaadd.Litra;
                    editing.BlereShiturSelect = marrngaadd.BlereShiturSelect;

                    _context.SaveChanges();
                    return RedirectToAction("AllNafta");
                
            }
            return RedirectToAction("EditNafta", new { id = id });
        }
        public IActionResult FshiNafta(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            NaftaStock nafta = _context.NaftaStocks.FirstOrDefault(p => p.NaftaStockId == id);
            if (nafta.BlereShiturId != null || nafta.BlereShiturId != 0)
            {
                var BlereNegativ = _context.Naftas.FirstOrDefault(p => p.BlereShiturId == nafta.BlereShiturId && p.BlereShiturSelect == "Blere");
                _context.Naftas.Remove(BlereNegativ);
            }

            _context.NaftaStocks.Remove(nafta);
            _context.SaveChanges();
            return RedirectToAction("AllNafta");

        }
        //public IActionResult NaftaRaport()
        //{

        //    var shofers = _context.Rrugas.GroupBy(r => r.Model == false)
        //       .Select(b => new
        //       {
        //           shpenzuar = b.Sum(m => m.NaftaPerTuShiturLitra * m.NaftaBlereCmim),
        //           nafta = b.Sum(m => m.NaftaPerTuShiturLitra),
        //       })
        //       .ToList();
        //    var cmimi = shofers.Select(b => b.shpenzuar / b.nafta);

        //    if (shofers != null)
        //    {
        //        ViewBag.Shofers = shofers;
        //    }
        //    return View();
        //}

    }
}
