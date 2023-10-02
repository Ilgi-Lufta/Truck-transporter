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
            var shofers = _context.Naftas
                     .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate < searchSecondTime : true)
                .ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            var naftaShitur = _context.Naftas.Where(b => b.BlereShiturSelect == "Blere")
                  .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

                    .ToList();
            var naftaBlere = _context.Naftas.Where(b => b.BlereShiturSelect == "Shitur")
                  .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)

                    .ToList();
            ViewBag.RefPrice = _context.Naftas
                 //.Where(e => e.Litra > 0 && e.Leke > 0)
                 .GroupBy(e => e.BlereShiturSelect == "Blere")
                 .Select(e =>
                 (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
                         )
                 .FirstOrDefault();



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

            return View();
        }

        [HttpPost]
        public IActionResult CreateNafta(Nafta marrngaadd)
        {

          
            if(marrngaadd.BlereShiturSelect== "Shitur")
            {
                var cmimRef = _context.Naftas
                //.Where(e => e.Litra > 0 && e.Leke > 0)
                .GroupBy(e => e.BlereShiturSelect == "Blere")
                .Select(e =>
                (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
                        )
                .FirstOrDefault();

                Nafta nafta = new Nafta
                {
                    BlereShiturSelect = "Blere",
                    Litra = (0-marrngaadd.Litra),
                    Cmimi = cmimRef,
                    Leke= 0-(cmimRef* marrngaadd.Litra)
                };

                _context.Add(nafta);
                _context.SaveChanges();
            }
          


            if (ModelState.IsValid)
            {
                marrngaadd.Leke = marrngaadd.Litra * marrngaadd.Cmimi;
                _context.Add(marrngaadd);
                _context.SaveChanges();
                return RedirectToAction("AllNafta");
            }
            return View("AddNafta");
        }
        public IActionResult EditNafta(int id)
        {
            ViewBag.id = id;
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
            Nafta Editing = _context.Naftas.FirstOrDefault(p => p.NaftaId == id);

            return View(Editing);
        }

        [HttpPost]
        public IActionResult EditedNafta(int id, Nafta marrngaadd)
        {

            if (ModelState.IsValid)
            {

                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                Nafta editing = _context.Naftas.FirstOrDefault(p => p.NaftaId == id);
                editing.Cmimi = marrngaadd.Cmimi;
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
            Nafta removingShofer = _context.Naftas.FirstOrDefault(p => p.NaftaId == id);
            _context.Naftas.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllNafta");

        }
        public IActionResult NaftaRaport()
        {

            var shofers = _context.Rrugas.GroupBy(r => r.Model == false)
               .Select(b => new
               {
                   shpenzuar = b.Sum(m => m.NaftaPerTuShiturLitra * m.NaftaBlereCmim),
                   nafta = b.Sum(m => m.NaftaPerTuShiturLitra),
               })
               .ToList();
            var cmimi = shofers.Select(b => b.shpenzuar / b.nafta);

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            return View();
        }

    }
}
