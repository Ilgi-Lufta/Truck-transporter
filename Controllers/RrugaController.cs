using BioLab.Models;
using BioLab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BioLab.Controllers
{
    public class RrugaController : Controller
    {
        private readonly ILogger<RrugaController> _logger;
        private MyContext _context;

        public RrugaController(ILogger<RrugaController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult AllRruga(string searchString)
        {
            var shofers = _context.Rrugas
                .Include(e=>e.PagesaDoganas).ThenInclude(e=>e.Currency)
                .Include(e => e.ShoferRrugas).ThenInclude(e => e.Shofer)
                 .Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits).ThenInclude(e => e.Currency)
                //.Include(e=>e.Shofer)
                //.Include(e => e.PikaShkarkimi)
                .Where(e=>e.Model==true)

                .ToList();

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Shofers = shofers.Where(s => s.Emri!.Contains(searchString))

                    ;
            }

            //var Currencys = _context.Currencys.ToList();
            //List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();

            //foreach (var item in Currencys)
            //{
            //    var pagesa = rrugas.Sum(e => e.RrugaFitimes.FirstOrDefault().Pagesa);
            //    RrugaFitime rrugaFitim = new RrugaFitime()
            //    {
            //        CurrencyId = item.CurrencyId,
            //        ShpenzimXhiro = false,
            //        Pagesa = pagesa,
            //    };
            //    rrugaFitime.Add(rrugaFitim);
            //}

            //ViewBag.Totali = rrugaFitime;
            return View();
        }

        public IActionResult AddRruga()
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

            var AllCurrency2 = _context.Currencys.ToList();
            if (AllCurrency2 != null)
            {
                IDictionary<int, string> numberNames = new Dictionary<int, string>();
                foreach (var currency in AllCurrency)
                {
                    numberNames.Add(currency.CurrencyId, currency.CurrencyUnit);
                }
                ViewBag.numberNames = numberNames;
            }
            var AllShofer2 = _context.Shofers.ToList();
            if (AllShofer2 != null)
            {
                IDictionary<int, string> ShoferIdNames = new Dictionary<int, string>();
                foreach (var Shofer2 in AllShofer2)
                {
                    ShoferIdNames.Add(Shofer2.ShoferId, Shofer2.Emri);
                }
                ViewBag.ShoferIdNames = ShoferIdNames;
            }



            var AllShofer =_context.Shofers.ToList();
            if (AllShofer != null)
            {
                 List<SelectListItem> shofers = new List<SelectListItem>();
                foreach (var shofer in AllShofer)
                {
                    shofers.Add(new SelectListItem { Text = shofer.Emri, Value = shofer.ShoferId.ToString() });
                }
            ViewBag.shofers = shofers;
            }
            var AllPika = _context.PikaShkarkimis.ToList();
            if (AllPika != null)
            {
                List<SelectListItem> pikas = new List<SelectListItem>();
                foreach (var pika in AllPika)
                {

                    pikas.Add(new SelectListItem { Text = pika.Emri, Value = pika.PikaShkarkimiId.ToString() });

                }
                ViewBag.Pikat=pikas;
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateRruga(Rruga marrngaadd)
        {
            
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Rrugas.Where(e => e.Model == true).Any(u => u.Emri == marrngaadd.Emri))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje rruge me kete emer!");

                    return View("AddRruga");
                }
                //vendosim lidhjen one to many per analizat e adminit te loguar
                // dhe e ruajm analizesn ne db
               // marrngaadd.ShoferId = _context.Shofers.FirstOrDefault().ShoferId;
                //marrngaadd.PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault().PikaShkarkimiId;
                marrngaadd.Model = true;
               // marrngaadd.PagesaShoferit

                _context.Add(marrngaadd);
                _context.SaveChanges();

                //Shofer RRUGA
                foreach (var ShoferitRrugaVM in marrngaadd.ShoferitRrugaVM)
                {

                    ShoferRruga shoferRruga = new ShoferRruga()
                    {
                        ShoferId = ShoferitRrugaVM.ShoferId,
                        RrugaId = marrngaadd.RrugaId,
                    };

                    _context.Add(shoferRruga);
                    _context.SaveChanges();
                    foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                    {
                        PagesaShoferit pagesaShoferit = new PagesaShoferit()
                        {
                            CurrencyId = PagesaShoferitVM.CurrencyId,
                            ShoferRrugaId = shoferRruga.ShoferRrugaId,
                            Pagesa = PagesaShoferitVM.Pagesa,
                            PagesaKryer = PagesaShoferitVM.PagesaKryer,
                            ShpenzimXhiro = true,
                        };
                        _context.Add(pagesaShoferit);
                        _context.SaveChanges();
                    }
                }

                //Pagesa dogana RRUGA
                foreach (var PagesaDoganaVM in marrngaadd.PagesaDoganaVM)
                {
                    PagesaDogana PagesaShoferit = new PagesaDogana()
                    {
                        CurrencyId = PagesaDoganaVM.CurrencyId,
                        Pagesa = PagesaDoganaVM.Pagesa,
                        RrugaId = marrngaadd.RrugaId,
                        PagesaKryer = PagesaDoganaVM.PagesaKryer,
                        ShpenzimXhiro = true
                    };
                    _context.Add(PagesaShoferit);
                    _context.SaveChanges();
                }
                return RedirectToAction("AllRruga");
        }



        public IActionResult FshiRruga(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            Rruga removingShofer = _context.Rrugas.FirstOrDefault(p => p.RrugaId == id);
            _context.Rrugas.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllRruga");

        }


        public IActionResult EditRruga(int id)
        {
            ViewBag.id = id;
            Rruga rruga = _context.Rrugas.Include(e => e.PagesaDoganas).Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits).FirstOrDefault(p => p.RrugaId == id);
            rruga.PagesaDoganaVM = new List<PagesaDoganaVM>();
            foreach (var PagesaDoganas in rruga.PagesaDoganas)
            {
                PagesaDoganaVM pagesaDoganaVM = new PagesaDoganaVM()
                {
                    CurrencyId = PagesaDoganas.CurrencyId,
                    Pagesa = PagesaDoganas.Pagesa,
                    PagesaKryer = PagesaDoganas.PagesaKryer
                };
                rruga.PagesaDoganaVM.Add(pagesaDoganaVM);
            }

            rruga.ShoferitRrugaVM = new List<ShoferitRrugaVM>();
            List<PagesaShoferitVM> pagesaShoferitVMs = new List<PagesaShoferitVM>();

            foreach (var ShoferRrugas in rruga.ShoferRrugas)
            {
                foreach (var pagesaShoferits in ShoferRrugas.PagesaShoferits)
                {
                    PagesaShoferitVM pagesaShoferitVM = new PagesaShoferitVM()
                    {
                        CurrencyId = pagesaShoferits.CurrencyId,
                        Pagesa = pagesaShoferits.Pagesa,
                        PagesaKryer = pagesaShoferits.PagesaKryer
                    };
                    pagesaShoferitVMs.Add(pagesaShoferitVM);
                }
                ShoferitRrugaVM shoferitRrugaVM = new ShoferitRrugaVM()
                {
                    ShoferId = ShoferRrugas.ShoferId,
                    pagesaShoferitVM = pagesaShoferitVMs
                };
                rruga.ShoferitRrugaVM.Add(shoferitRrugaVM);
                pagesaShoferitVMs = new List<PagesaShoferitVM>();
            }
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
            var AllShofer2 = _context.Shofers.ToList();
            if (AllShofer2 != null)
            {
                IDictionary<int, string> ShoferIdNames = new Dictionary<int, string>();
                foreach (var Shofer2 in AllShofer2)
                {
                    ShoferIdNames.Add(Shofer2.ShoferId, Shofer2.Emri);
                }
                ViewBag.ShoferIdNames = ShoferIdNames;
            }
            return View(rruga);
        }

        [HttpPost]
        public IActionResult EditedRruga(int id, Rruga marrngaadd)
        {
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Rrugas.Where(sh => sh.RrugaId != id).Any(u => u.Emri == marrngaadd.Emri))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return RedirectToAction("EditRruga", new { id = id });
                }
                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                Rruga editing = _context.Rrugas.Include(e => e.PagesaDoganas).Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits).FirstOrDefault(p => p.RrugaId == id);
                editing.Emri = marrngaadd.Emri;
               // editing.Dogana = marrngaadd.Dogana;
                editing.NaftaShpenzuarLitra = marrngaadd.NaftaShpenzuarLitra;
                List<int> pagpikashkarkId = new List<int>();
                foreach (var item in editing.PagesaDoganas)
                {
                    var pagpikashakrkimi = _context.PagesaDoganas.FirstOrDefault(e => e.PagesaDoganaId == item.PagesaDoganaId).PagesaDoganaId;
                    pagpikashkarkId.Add(pagpikashakrkimi);
                }
                foreach (var item in pagpikashkarkId)
                {
                    var pagpikashakrkimi = _context.PagesaDoganas.FirstOrDefault(e => e.PagesaDoganaId == item);

                    _context.PagesaDoganas.Remove(pagpikashakrkimi);
                }
                _context.SaveChanges();
                //Pagesa dogana RRUGA
                foreach (var PagesaDoganaVM in marrngaadd.PagesaDoganaVM)
                {
                    PagesaDogana PagesaShoferit = new PagesaDogana()
                    {
                        CurrencyId = PagesaDoganaVM.CurrencyId,
                        Pagesa = PagesaDoganaVM.Pagesa,
                        RrugaId = id,
                        PagesaKryer = PagesaDoganaVM.PagesaKryer,
                        ShpenzimXhiro = true
                    };
                    _context.Add(PagesaShoferit);
                    _context.SaveChanges();
                }


                List<int> shoferRrugaId = new List<int>();
                foreach (var item in editing.ShoferRrugas)
                {
                    var pagpikashakrkimi = _context.ShoferRrugas.FirstOrDefault(e => e.ShoferRrugaId == item.ShoferRrugaId).ShoferRrugaId;
                    shoferRrugaId.Add(pagpikashakrkimi);
                }
                foreach (var item in shoferRrugaId)
                {
                    var pagpikashakrkimi = _context.ShoferRrugas.FirstOrDefault(e => e.ShoferRrugaId == item);

                    _context.ShoferRrugas.Remove(pagpikashakrkimi);
                }


                _context.SaveChanges();



                //Shofer RRUGA
                foreach (var ShoferitRrugaVM in marrngaadd.ShoferitRrugaVM)
                {

                    ShoferRruga shoferRruga = new ShoferRruga()
                    {
                        ShoferId = ShoferitRrugaVM.ShoferId,
                        RrugaId = id,
                    };

                    _context.Add(shoferRruga);
                    _context.SaveChanges();
                    foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                    {
                        PagesaShoferit pagesaShoferit = new PagesaShoferit()
                        {
                            CurrencyId = PagesaShoferitVM.CurrencyId,
                            ShoferRrugaId = shoferRruga.ShoferRrugaId,
                            Pagesa = PagesaShoferitVM.Pagesa,
                            PagesaKryer = PagesaShoferitVM.PagesaKryer,
                            ShpenzimXhiro = true,
                        };
                        _context.Add(pagesaShoferit);
                        _context.SaveChanges();
                    }
                }





                _context.SaveChanges();
                return RedirectToAction("AllRruga");
            }
            return RedirectToAction("EditPika", new { id = id });
        }


        public IActionResult AllRrugaJoModel(string searchString, DateTime searchFirstTime, DateTime searchSecondTime)
        {
            var shofers = _context.Rrugas
                //.Include(e => e.Shofer)
                //.Include(e => e.PikaShkarkimi)
                //.Include(e=>e.Nafta).ThenInclude(e => e.Currency)
                //.Include(e=>e.PagesaDoganas).ThenInclude(e=>e.Currency)
                .Include(e=>e.ShoferRrugas).ThenInclude(e => e.Shofer)
                .Include(e=>e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi)
                //.Include(e=>e.RrugaShpenzimeEkstras).ThenInclude(e=>e.Currency)
                //.Include(e=>e.RrugaFitimeEkstras).ThenInclude(e=>e.Currency)
                .Include(e=>e.RrugaFitimes).ThenInclude(e=>e.Currency)
                .Where(e => e.Model == false)
                                    .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)
                .ToList();
            //foreach (var shofer in shofers)
            //{
            //    if(shofer.Nafta.Litra)
            //}
            List<Rruga> rrugas = new List<Rruga>();
            if (shofers != null)
            {
                rrugas = shofers;
                ViewBag.Shofers = rrugas;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                rrugas = shofers.Where(s => s.Emri!.Contains(searchString))
                                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true).ToList();
                ViewBag.Shofers = rrugas;

            }

            //var rrugafitime = 0;
            //var gjendja = 0;
            //var nafta = 0;

            var Currencys = _context.Currencys.ToList();
            List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();

            foreach (var item in Currencys)
            {
                  var Currency = _context.Currencys.FirstOrDefault(e=>e.CurrencyId == item.CurrencyId);
                RrugaFitime rrugaFitim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    Currency = Currency,
                    ShpenzimXhiro = false,
                    Pagesa = 0
                };
                rrugaFitime.Add(rrugaFitim);
            }

            foreach (var rruga in rrugas)
            {
                if (rruga.RrugaFitimes.Count > 0)
                {
                    foreach (var fitim in rruga.RrugaFitimes)
                    { 
                       var rrugaFitim= rrugaFitime.FirstOrDefault(e => e.CurrencyId == fitim.CurrencyId);
                        rrugaFitim.Pagesa = rrugaFitim.Pagesa + fitim.Pagesa;
                    }
                }
            }

            ViewBag.Totali = rrugaFitime;

            return View();
        }
        public IActionResult AddRrugaJoModel(int id)
        {
            var rruga = _context.Rrugas.Include(e=>e.PikaRrugas).ThenInclude(e=>e.PikaRrugaPagesa).Include(e => e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi).Include(e=>e.PagesaDoganas).Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits).FirstOrDefault(e => e.RrugaId == id);
            ViewBag.id = id;
            rruga.PagesaDoganaVM = new List<PagesaDoganaVM>();
            foreach (var PagesaDoganas in rruga.PagesaDoganas)
            {
                PagesaDoganaVM pagesaDoganaVM = new PagesaDoganaVM()
                {
                    CurrencyId= PagesaDoganas.CurrencyId,
                    Pagesa= PagesaDoganas.Pagesa,
                    PagesaKryer = PagesaDoganas.PagesaKryer
                };
                rruga.PagesaDoganaVM.Add(pagesaDoganaVM);
            }

            rruga.ShoferitRrugaVM = new List<ShoferitRrugaVM>();
            List<PagesaShoferitVM> pagesaShoferitVMs = new List<PagesaShoferitVM>();

            foreach (var ShoferRrugas in rruga.ShoferRrugas)
            {
                foreach (var pagesaShoferits in ShoferRrugas.PagesaShoferits)
                {
                    PagesaShoferitVM pagesaShoferitVM = new PagesaShoferitVM()
                    {
                        CurrencyId = pagesaShoferits.CurrencyId,
                        Pagesa = pagesaShoferits.Pagesa,
                        PagesaKryer = pagesaShoferits.PagesaKryer
                    };
                    pagesaShoferitVMs.Add(pagesaShoferitVM);
                }
                ShoferitRrugaVM shoferitRrugaVM = new ShoferitRrugaVM()
                {
                    ShoferId = ShoferRrugas.ShoferId,
                    pagesaShoferitVM = pagesaShoferitVMs
                };
                rruga.ShoferitRrugaVM.Add(shoferitRrugaVM);
                pagesaShoferitVMs = new List<PagesaShoferitVM>();
            }




            //var AllShofer = _context.Shofers.ToList();
            //if (AllShofer != null)
            //{
            //    List<SelectListItem> shofers = new List<SelectListItem>();
            //    foreach (var shofer in AllShofer)
            //    {

            //        shofers.Add(new SelectListItem
            //        {
            //            Text = shofer.Emri,
            //            Value = shofer.ShoferId.ToString(),
            //            //Selected = AllShofer.Count() == 1 ? true : false,
            //            Selected = rruga.Shofer.Emri == shofer.Emri? true : false
            //        });
            //    }
            //    ViewBag.shofers = shofers;
            //}
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

            var AllCurrency2 = _context.Currencys.ToList();
            if (AllCurrency2 != null)
            {
                IDictionary<int, string> numberNames = new Dictionary<int, string>();
                foreach (var currency in AllCurrency)
                {
                    numberNames.Add(currency.CurrencyId, currency.CurrencyUnit);
                }
                ViewBag.numberNames = numberNames;
            }
            var AllShofer2 = _context.Shofers.ToList();
            if (AllShofer2 != null)
            {
                IDictionary<int, string> ShoferIdNames = new Dictionary<int, string>();
                foreach (var Shofer2 in AllShofer2)
                {
                    ShoferIdNames.Add(Shofer2.ShoferId, Shofer2.Emri);
                }
                ViewBag.ShoferIdNames = ShoferIdNames;
            }



            var AllShofer = _context.Shofers.ToList();
            if (AllShofer != null)
            {
                List<SelectListItem> shofers = new List<SelectListItem>();
                foreach (var shofer in AllShofer)
                {
                    shofers.Add(new SelectListItem { Text = shofer.Emri, Value = shofer.ShoferId.ToString() });
                }
                ViewBag.shofers = shofers;
            }
            //var AllPika = _context.PikaShkarkimis.ToList();
            //if (AllPika != null)
            //{
            //    List<SelectListItem> pikas = new List<SelectListItem>();
            //    foreach (var pika in AllPika)
            //    {

            //        pikas.Add(new SelectListItem { Text = pika.Emri, Value = pika.PikaShkarkimiId.ToString() });

            //    }
            //    ViewBag.Pikat = pikas;
            //}

            ViewBag.PikaPagesa = _context.PikaShkarkimis.Include(e => e.PagesaPikaShkarkimits).ThenInclude(e => e.Currency).ToList();


            var AllPika = _context.PikaShkarkimis.ToList();
            if (AllPika != null)
            {
                List<SelectListItem> pikas = new List<SelectListItem>();
                foreach (var pika in AllPika)
                {

                    pikas.Add(new SelectListItem
                    {
                        Text = pika.Emri,
                        Value = pika.PikaShkarkimiId.ToString(),
                        Selected = AllPika.Count() == 1 ? true : false
                    });

                }
                if (pikas.Count > 1)
                {
                    pikas.Add(new SelectListItem
                    {
                        Text = "Zgjidh Piken e Shkarkimit",
                        Value = "-1",
                        Selected = true
                    });

                }
                ViewBag.Pikat = pikas;
            }
            ViewBag.pagesa = AllPika.Count() == 1 ? AllPika[0].Pagesa : 0;

            List<PikaRrugaPagesaVM> PikaRrugaPagesaVMs = new List<PikaRrugaPagesaVM>();
            PikaRrugaPagesaVM  PikaRrugaPagesaVM = new  PikaRrugaPagesaVM(){
                CurrencyId= 1 ,
                Pagesa=2,
                PagesaKryer=true
            };
            PikaRrugaPagesaVMs.Add(PikaRrugaPagesaVM);
            PikaRrugasVM PikaRrugasVM  = new PikaRrugasVM();
            PikaRrugasVM.PikaRrugaPagesaVMs = PikaRrugaPagesaVMs;
            PikaRrugasVM.PikaShkarkimiId = 4;
            rruga.PikaRrugasVM.Add(PikaRrugasVM);
            if (rruga.PikaRrugas.Count() == 0)
            {
                PikaShkarkimi pika = new PikaShkarkimi() { Emri = "Zgjidh" };
                List<PikaRruga> pikaRruga = new List<PikaRruga>();
                List<PikaRrugaPagesa> pikaRrugaPagesas = new List<PikaRrugaPagesa>();
                PikaRrugaPagesa pikaRrugaPagesa = new PikaRrugaPagesa()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                pikaRrugaPagesas.Add(pikaRrugaPagesa);
                PikaRruga PikaRruga = new PikaRruga()
                {
                    RrugaId = rruga.RrugaId,
                    PikaShkarkimi = pika,
                    PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault().PikaShkarkimiId,
                    PikaRrugaPagesa = pikaRrugaPagesas,
                    PikaShkarkimiName = "Zgjidh"
                };

                pikaRruga.Add(PikaRruga);
                rruga.PikaRrugas = pikaRruga;
            }
            else
            {
                foreach (var PikaRrugas in rruga.PikaRrugas)
                {
                    PikaRrugas.PikaShkarkimiName = PikaRrugas.PikaShkarkimi.Emri;
                }
                    
            }

            //shpenzime extra
            if (rruga.RrugaShpenzimeEkstras.Count == 0)
            {
                RrugaShpenzimeEkstra rrugaShpenzimeEkstra = new RrugaShpenzimeEkstra()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                rruga.RrugaShpenzimeEkstras.Add(rrugaShpenzimeEkstra);
            }
            else
            {
                foreach (var PagesaDoganas in rruga.RrugaShpenzimeEkstras)
                {
                    RrugaShpenzimeEkstra rrugaShpenzimeEkstra = new RrugaShpenzimeEkstra()
                    {
                        CurrencyId = PagesaDoganas.CurrencyId,
                        Pagesa = PagesaDoganas.Pagesa,
                        PagesaKryer = PagesaDoganas.PagesaKryer
                    };
                    rruga.RrugaShpenzimeEkstras.Add(rrugaShpenzimeEkstra);
                }
            }
            //fitime extra
            if (rruga.RrugaFitimeEkstras.Count == 0)
            {
                RrugaFitimeEkstra rrugaFitimeEkstra = new RrugaFitimeEkstra()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                rruga.RrugaFitimeEkstras.Add(rrugaFitimeEkstra);
            }
            else
            {
                foreach (var PagesaDoganas in rruga.RrugaFitimeEkstras)
                {
                    RrugaFitimeEkstra rrugaShpenzimeEkstra = new RrugaFitimeEkstra()
                    {
                        CurrencyId = PagesaDoganas.CurrencyId,
                        Pagesa = PagesaDoganas.Pagesa,
                        PagesaKryer = PagesaDoganas.PagesaKryer
                    };
                    rruga.RrugaFitimeEkstras.Add(rrugaShpenzimeEkstra);
                }
            }
            //Nafta
            if (rruga.Nafta.Count == 0)
            {
                Nafta nafta = new Nafta()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0,
                    Litra = 0,
                };
                rruga.Nafta.Add(nafta);
            }
            else
            {
                foreach (var PagesaDoganas in rruga.Nafta)
                {
                    Nafta nafta = new Nafta()
                    {
                        CurrencyId = PagesaDoganas.CurrencyId,
                        Pagesa = PagesaDoganas.Pagesa,
                        PagesaKryer = PagesaDoganas.PagesaKryer,
                        Litra = PagesaDoganas.Litra
                    };
                    rruga.Nafta.Add(nafta);
                }
            }




            //  ViewBag.PikaRrugasVM
            return View(rruga);
        }
        [HttpPost]
        public IActionResult CreateRrugaJoModel(Rruga marrngaadd,int id)
        {
    //to calculate all fitimet grouped by currency
   // flag for pagesa kryer 
  //TO TEST to calculate nafta per tu shitur  

/// to fix llogaritja e shpenzimit te naftes


           var Currencys= _context.Currencys.ToList();
            List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();
            List<RrugaFitime> rrugaFitimeShpenzims = new List<RrugaFitime>();
            List<RrugaFitime> rrugaFitimeXhiros = new List<RrugaFitime>();

            bool PagesaKryer = true;

            foreach (var item in Currencys)
            {
                RrugaFitime rrugaFitimeShpenzim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = true
                };
                rrugaFitimeShpenzims.Add(rrugaFitimeShpenzim);
                RrugaFitime rrugaFitimeXhiro = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = false
                };
                rrugaFitimeXhiros.Add(rrugaFitimeXhiro);
                RrugaFitime rrugaFitim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = false
                };
                rrugaFitime.Add(rrugaFitim);
            }

            /////

///// nafta part
            if (marrngaadd.PikaRrugas.FirstOrDefault().PikaShkarkimiName == "Zgjidh")
            { marrngaadd.PikaRrugas = null; }
            else
            {
                foreach (var pikaRruga in marrngaadd.PikaRrugas)
                {
                    int PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault(e => e.Emri == pikaRruga.PikaShkarkimiName).PikaShkarkimiId;
                    PikaRrugasVM pikaRrugasVM = new PikaRrugasVM()
                    {
                        PikaShkarkimiId = PikaShkarkimiId
                    };
                    foreach (var PikaRrugaPagesa in pikaRruga.PikaRrugaPagesa)
                    {
                        PikaRrugaPagesaVM PagesaVM = new PikaRrugaPagesaVM()
                        {
                            Pagesa = PikaRrugaPagesa.Pagesa,
                            CurrencyId = PikaRrugaPagesa.CurrencyId,
                            PagesaKryer = PikaRrugaPagesa.PagesaKryer,
                        };
                        pikaRrugasVM.PikaRrugaPagesaVMs.Add(PagesaVM);
                    }
                    marrngaadd.PikaRrugasVM.Add(pikaRrugasVM);
                }
                marrngaadd.PikaRrugas = null;
            }
            if (marrngaadd.shenime == null) marrngaadd.shenime = "test";

            //RrugaShpenzimeEkstras
            foreach (var RrugaShpenzimeEkstra in marrngaadd.RrugaShpenzimeEkstras)
            {
                if (RrugaShpenzimeEkstra.Pagesa == 0)
                {
                    RrugaShpenzimeEkstra.PagesaKryer = true;
                }

            }
            //RrugaFitimeEkstras
            foreach (var RrugaFitimeEkstra in marrngaadd.RrugaFitimeEkstras)
            {
                if (RrugaFitimeEkstra.Pagesa == 0)
                {
                    RrugaFitimeEkstra.PagesaKryer = true;
                }
            }
            //  Pika Nafta
            foreach (var Nafta in marrngaadd.Nafta)
            {

                if (Nafta.Litra == 0)
                {
                    Nafta.PagesaKryer = true;
                }
                Nafta.BlereShiturSelect = "Blere";
            }
            /// shtimi i rruges
            _context.Add(marrngaadd);
            _context.SaveChanges(); 

            //Pagesa dogana RRUGA
            foreach (var PagesaDoganaVM in marrngaadd.PagesaDoganaVM)
            {
                if(PagesaDoganaVM.Pagesa == 0)
                {
                    PagesaDoganaVM.PagesaKryer = true;
                }
                PagesaDogana PagesaShoferit = new PagesaDogana()
                {
                    CurrencyId = PagesaDoganaVM.CurrencyId,
                    Pagesa = PagesaDoganaVM.Pagesa,
                    RrugaId = marrngaadd.RrugaId,
                    PagesaKryer = PagesaDoganaVM.PagesaKryer,
                    ShpenzimXhiro = true
                };
                _context.Add(PagesaShoferit);
                _context.SaveChanges();
            }

            //Shofer RRUGA
            foreach (var ShoferitRrugaVM in marrngaadd.ShoferitRrugaVM)
            {
                ShoferRruga shoferRruga = new ShoferRruga()
                {
                    ShoferId = ShoferitRrugaVM.ShoferId,
                    RrugaId = marrngaadd.RrugaId,
                };

                _context.Add(shoferRruga);
                _context.SaveChanges();
                foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                {
                    if (PagesaShoferitVM.Pagesa == 0)
                    {
                        PagesaShoferitVM.PagesaKryer = true;
                    }
                    PagesaShoferit pagesaShoferit = new PagesaShoferit()
                    {
                        CurrencyId = PagesaShoferitVM.CurrencyId,
                        ShoferRrugaId = shoferRruga.ShoferRrugaId,
                        Pagesa = PagesaShoferitVM.Pagesa,
                        PagesaKryer = PagesaShoferitVM.PagesaKryer,
                        ShpenzimXhiro = true,
                    };
                    _context.Add(pagesaShoferit);
                    _context.SaveChanges();
                }
            }

          
            // Pika Rruga
            if (marrngaadd.PikaRrugasVM != null)
            {
                foreach (var PikaRrugaVM in marrngaadd.PikaRrugasVM)
                {
                    int PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault(e => e.PikaShkarkimiId == PikaRrugaVM.PikaShkarkimiId).PikaShkarkimiId;

                    PikaRruga pikaRruga = new PikaRruga()
                    {
                        RrugaId=marrngaadd.RrugaId,
                        PikaShkarkimiId = PikaShkarkimiId,
                    };

                    foreach (var PikaRrugaPagesaVM in PikaRrugaVM.PikaRrugaPagesaVMs)
                    {

                        if (PikaRrugaPagesaVM.Pagesa == 0)
                        {
                            PikaRrugaPagesaVM.PagesaKryer = true;
                        }
                        PikaRrugaPagesa pikaRrugaPagesa = new PikaRrugaPagesa()
                        {
                            CurrencyId = PikaRrugaPagesaVM.CurrencyId,
                            Pagesa = PikaRrugaPagesaVM.Pagesa,
                            PagesaKryer = PikaRrugaPagesaVM.PagesaKryer
                        };
                        pikaRruga.PikaRrugaPagesa.Add(pikaRrugaPagesa);
                    }
                    _context.Add(pikaRruga);
                    _context.SaveChanges();
                }
            }


            ///
            //// NAFTA PART
            //llogaritja e naftes se blere
            decimal naftaBlereLitra = 0;
            decimal NaftaShpenzuarLitra = marrngaadd.NaftaShpenzuarLitra;
            foreach (var Nafta in marrngaadd.Nafta)
            {
                Nafta.BlereShiturSelect = "Blere";
                naftaBlereLitra = Nafta.Litra + naftaBlereLitra;
            }

            marrngaadd.NaftaBlereLitra = naftaBlereLitra;
            // to add nafta si naftastock
            // to add nafta si shpenzim

            //to fix nafta blere no metter the currency
            int naftaCount = marrngaadd.Nafta.Count();
            List<NaftaStock> naftaStocks= new List<NaftaStock>();
            for (int i = 0; i < naftaCount; i++)
            {
                if (marrngaadd.Nafta[i].Litra == 0)
                    continue;
                //if (naftaBlereLitra <= NaftaShpenzuarLitra)
                //{ }
                    if (marrngaadd.Nafta[i].Litra <= NaftaShpenzuarLitra && i != naftaCount-1)
                {
                    //naftaBlereLitra = naftaBlereLitra - marrngaadd.Nafta[i].Litra;
                    NaftaShpenzuarLitra = NaftaShpenzuarLitra - marrngaadd.Nafta[i].Litra;
                    //shtohet pagesa e plote e naftes si shpenzim
                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == marrngaadd.Nafta[i].CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + marrngaadd.Nafta[i].Pagesa;
                    if (marrngaadd.Nafta[i].PagesaKryer == true)
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + marrngaadd.Nafta[i].Pagesa;
                    }
                }
                else
                {
                    decimal cmim = marrngaadd.Nafta[i].Pagesa / marrngaadd.Nafta[i].Litra;
                    decimal litraMbetur = marrngaadd.Nafta[i].Litra - NaftaShpenzuarLitra;
                    if (litraMbetur>=0)
                    {
                        decimal pagesashpenzim = cmim * NaftaShpenzuarLitra;
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == marrngaadd.Nafta[i].CurrencyId);
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + pagesashpenzim;
                        if (marrngaadd.Nafta[i].PagesaKryer == true)
                        {
                            rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + pagesashpenzim;
                        }

                    }
                    NaftaShpenzuarLitra = 0;
                    decimal pagesaNaftaMbetur= litraMbetur * cmim;
                    //decimal shpenzim = NaftaShpenzuarLitra * cmim;

                    NaftaStock naftaStock = new NaftaStock()
                    {
                        Litra = litraMbetur,
                        Pagesa = pagesaNaftaMbetur,
                        PagesaKryer = marrngaadd.Nafta[i].PagesaKryer,
                        CurrencyId = marrngaadd.Nafta[i].CurrencyId,
                        RrugaId = marrngaadd.RrugaId,
                      //  BlereShiturSelect = "Blere",
                    };
                    naftaStocks.Add(naftaStock);
                }
            }

           //menyra si llogaritet nafta e blere nga rruga

            // shtimi i naftes se blere nese litrat jane pozitive
           // shtimi i nje cifti blere shitur nese nafta eshte negative
           // nese cmimi referenc i naftes stock eshte negativ 
           // perdoret si (negativ) cmim per naften e blere cmimi i naftes se qe eshte bere negativ


            foreach(var naftastock in naftaStocks)
            {
                if (naftastock.Litra == 0)
                    continue;
                if (naftastock.Litra > 0)
                {
                    naftastock.BlereShiturSelect = "Blere";
                    ////shtohet pagesa per litrat e mbetura te naftes si shpenzim
                    ///
                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == naftastock.CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + naftastock.Pagesa;
                     if (naftastock.PagesaKryer)
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + naftastock.Pagesa;
                    }
                    _context.Add(naftastock);
                    _context.SaveChanges();
                    continue;
                }
                if (naftastock.Litra < 0)
                {
                    BlereShitur blereShitur = new BlereShitur();
                    _context.Add(blereShitur);
                    _context.SaveChanges();
                    

                    //marrja e listes se currencyve te naftes stock dhe cmimit ref pozitiv

                    List<CurrCmimRef> currCmimRefs = new List<CurrCmimRef>();
                    List<int> naftaStocksCurrencyId = _context.NaftaStocks.GroupBy(e=>e.CurrencyId).Select(e => e.Key).ToList();
                    foreach (var CurrencyId in naftaStocksCurrencyId)
                    {
                        var cmimRef = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
                       //.Where(e => e.Litra > 0 && e.Leke > 0)
                       .GroupBy(e => e.CurrencyId == CurrencyId)
                       .Select(e =>
                       (e.Sum(b => b.Pagesa) / e.Sum(b => b.Litra))
                               )
                       .FirstOrDefault();
                        if (cmimRef > 0)
                        {
                            CurrCmimRef currCmimRef = new CurrCmimRef()
                            {
                                CmimRef = cmimRef,
                                CurrencyId = CurrencyId
                            };
                            currCmimRefs.Add(currCmimRef);
                        }
                    }
                    //krijimi i naftes se blere (negative)
                    NaftaStock naftablereStock = new NaftaStock()
                    {
                        Litra = naftastock.Litra,
                        // Pagesa = pagesaNaftaMbetur,
                        PagesaKryer = naftastock.PagesaKryer,
                      //  CurrencyId = naftastock.CurrencyId,
                        RrugaId = marrngaadd.RrugaId,
                        BlereShiturSelect = "Blere",
                        BlereShiturId = blereShitur.BlereShiturId
                    };

                    if (currCmimRefs.Count > 0)
                    {
                        var currCmimRef = currCmimRefs.FirstOrDefault();
                       naftablereStock.Pagesa =  naftablereStock.Litra * currCmimRef.CmimRef;
                       naftablereStock.CurrencyId = currCmimRef.CurrencyId;
                        naftablereStock.Shenime = "cmim ref";
                        //shtohet pagesa per litrat (negative) me cmimin nga cmimi references te naftes si shpenzim
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == currCmimRef.CurrencyId);
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa +(0- naftablereStock.Pagesa);


                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + (0- naftablereStock.Pagesa);
                    }
                    else
                    {
                        decimal cmim = (0 - naftastock.Pagesa) / (0 - naftastock.Litra);
                        naftablereStock.Pagesa = naftastock.Litra * cmim;
                        naftablereStock.Shenime = "cmim nafta blere nga rruga";
                        //shtohet pagesa per litrat (negative) me cmim nga nafta blere nga rruga te naftes si shpenzim
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == naftastock.CurrencyId);
                        naftablereStock.CurrencyId = naftastock.CurrencyId;
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa - naftablereStock.Pagesa;

                        if (naftastock.PagesaKryer == true)
                        {
                            rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale - naftablereStock.Pagesa;
                        }

                    }

                    naftastock.BlereShiturSelect = "Shitur";
                    //shto shtije cmim 0 litra +
                    naftastock.Litra = 0 - naftastock.Litra;
                    naftastock.Pagesa = 0;
                    naftastock.BlereShiturId = blereShitur.BlereShiturId;

                    _context.Add(naftastock);
                    _context.Add(naftablereStock);
                    _context.SaveChanges();

                }

            }


            /////// Fitime part 

            // Shpenzim part      dogana 
            foreach (var PagesaDoganaVM in marrngaadd.PagesaDoganaVM)
            {
               var rrugaFitimeShpenzim =  rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaDoganaVM.CurrencyId);
                rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaDoganaVM.Pagesa;
                if (PagesaDoganaVM.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + PagesaDoganaVM.Pagesa;
                }

            }

            // Shpenzim part   Shofer RRUGA
            foreach (var ShoferitRrugaVM in marrngaadd.ShoferitRrugaVM)
            {
                foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                {
                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaShoferitVM.CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaShoferitVM.Pagesa;
                    if (PagesaShoferitVM.PagesaKryer == false)
                    {
                        PagesaKryer = false;
                    }
                    else
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + PagesaShoferitVM.Pagesa;
                    }
                }
            }

            // Fitim part  Pika Rruga
            if (marrngaadd.PikaRrugasVM != null)
            {
                foreach (var PikaRrugaVM in marrngaadd.PikaRrugasVM)
                {
                    foreach (var PikaRrugaPagesaVM in PikaRrugaVM.PikaRrugaPagesaVMs)
                    {
                        var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == PikaRrugaPagesaVM.CurrencyId);
                        rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + PikaRrugaPagesaVM.Pagesa;
                        if (PikaRrugaPagesaVM.PagesaKryer == false)
                        {
                            PagesaKryer = false;
                        }
                        else
                        {
                            rrugaFitimeXhiro.PagesaReale = rrugaFitimeXhiro.PagesaReale + PikaRrugaPagesaVM.Pagesa;
                        }
                       
                    }
                   
                }
            }
            //  Shpenzim part    RrugaShpenzimeEkstras
            foreach (var RrugaShpenzimeEkstra in marrngaadd.RrugaShpenzimeEkstras)
            {
                var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == RrugaShpenzimeEkstra.CurrencyId);
                rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + RrugaShpenzimeEkstra.Pagesa;
                if (RrugaShpenzimeEkstra.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + RrugaShpenzimeEkstra.Pagesa;

                }
            }
            //  Fitim part    RrugaFitimeEkstras
            foreach (var RrugaFitimeEkstra in marrngaadd.RrugaFitimeEkstras)
            {
                var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == RrugaFitimeEkstra.CurrencyId);
                rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + RrugaFitimeEkstra.Pagesa;
                if (RrugaFitimeEkstra.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeXhiro.PagesaReale = rrugaFitimeXhiro.PagesaReale + RrugaFitimeEkstra.Pagesa;

                }
            }

            foreach (var RrugaNafte in marrngaadd.Nafta)
            {
                if (RrugaNafte.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
            }
            // to calulate fitime 
            foreach (var rrugaFitimeXhiro in rrugaFitimeXhiros)
            {
                var rrugaFitimecurr = rrugaFitime.FirstOrDefault(e => e.CurrencyId == rrugaFitimeXhiro.CurrencyId);
                rrugaFitimecurr.Pagesa = rrugaFitimecurr.Pagesa+ rrugaFitimeXhiro.Pagesa ;
                rrugaFitimecurr.PagesaReale = rrugaFitimecurr.PagesaReale + rrugaFitimeXhiro.PagesaReale;
            }
            foreach (var rrugaFitimeShpenzim in rrugaFitimeShpenzims)
            {
                var rrugaFitimecurr = rrugaFitime.FirstOrDefault(e => e.CurrencyId == rrugaFitimeShpenzim.CurrencyId);
                rrugaFitimecurr.Pagesa = rrugaFitimecurr.Pagesa - rrugaFitimeShpenzim.Pagesa;
                rrugaFitimecurr.PagesaReale = rrugaFitimecurr.PagesaReale - rrugaFitimeShpenzim.PagesaReale;
            }

            marrngaadd.RrugaFitimes = rrugaFitime;
            marrngaadd.PagesaKryer = PagesaKryer;
            _context.SaveChanges();
           
            return RedirectToAction("AllRrugaJoModel");

       //     return RedirectToAction("AddRrugaJoModel", new { id = id });
            //}
        }
        [HttpPost]
        public decimal CreateRrugaJoModel2(int id, string val)
        {
           var test = int.Parse(val);
            var paga = _context.PikaShkarkimis.FirstOrDefault(e=>e.PikaShkarkimiId ==test).Pagesa;

            return paga;
        }
         [HttpPost]
        public PikaRruga CreateRrugaJoModel3(string id2, string rrugaId)
        {
            var test = int.Parse(id2);
            var rrugaIdd = int.Parse(rrugaId);
            var pikaShkarkimi = _context.PikaShkarkimis.Include(e => e.PagesaPikaShkarkimits).FirstOrDefault(e => e.PikaShkarkimiId == test);
           // var rruga = _context.Rrugas.FirstOrDefault(e => e.RrugaId == rrugaIdd);

            PikaRruga pika1 = new PikaRruga();
            pika1.RrugaId = rrugaIdd;
            pika1.PikaShkarkimiId = test;
            pika1.PikaShkarkimiName = pikaShkarkimi.Emri;
            //_context.Add(pika1);
            //_context.SaveChanges();

            foreach (var item in pikaShkarkimi.PagesaPikaShkarkimits)
            {
                PikaRrugaPagesa pikaRrugaPagesa = new PikaRrugaPagesa();
              //  pikaRrugaPagesa.PikaRrugaId = pika1.PikaRrugaId;
                pikaRrugaPagesa.CurrencyId = item.CurrencyId;
                pikaRrugaPagesa.Pagesa = item.Pagesa;
                pikaRrugaPagesa.PagesaKryer = item.PagesaKryer;
                pika1.PikaRrugaPagesa.Add(pikaRrugaPagesa);
                //_context.Add(pikaRrugaPagesa);
                //_context.SaveChanges();
            }

           
            return pika1;






        }

        [HttpPost]
        public List<SelectListItem> Get()
        {
            var AllCurrency = _context.Currencys.ToList();
            List<SelectListItem> currencys = new List<SelectListItem>();

            if (AllCurrency != null)
            {
                foreach (var currency in AllCurrency)
                {
                    currencys.Add(new SelectListItem
                    {
                        Text = currency.CurrencyUnit,
                        Value = currency.CurrencyId.ToString()
                    });
                }
              //  ViewBag.currencys = currencys;
            }
            return currencys;
        }


        public IActionResult EditRrugaJoModel(int id)
        {
            var rruga = _context.Rrugas
                .Include(e => e.PikaRrugas).ThenInclude(e => e.PikaRrugaPagesa).ThenInclude(e => e.Currency)
                .Include(e => e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi)
                .Include(e => e.PagesaDoganas)
                .Include(e => e.RrugaShpenzimeEkstras).ThenInclude(e => e.Currency)
                .Include(e => e.RrugaFitimeEkstras).ThenInclude(e => e.Currency)
                .Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits).ThenInclude(e => e.Currency)
                .Include(e => e.Nafta).ThenInclude(e => e.Currency)
                .FirstOrDefault(e => e.RrugaId == id);
            ViewBag.id = id;
            rruga.PagesaDoganaVM = new List<PagesaDoganaVM>();
            foreach (var PagesaDoganas in rruga.PagesaDoganas)
            {
                PagesaDoganaVM pagesaDoganaVM = new PagesaDoganaVM()
                {
                    CurrencyId = PagesaDoganas.CurrencyId,
                    Pagesa = PagesaDoganas.Pagesa,
                    PagesaKryer = PagesaDoganas.PagesaKryer
                };
                rruga.PagesaDoganaVM.Add(pagesaDoganaVM);
            }

            rruga.ShoferitRrugaVM = new List<ShoferitRrugaVM>();
            List<PagesaShoferitVM> pagesaShoferitVMs = new List<PagesaShoferitVM>();

            foreach (var ShoferRrugas in rruga.ShoferRrugas)
            {
                foreach (var pagesaShoferits in ShoferRrugas.PagesaShoferits)
                {
                    PagesaShoferitVM pagesaShoferitVM = new PagesaShoferitVM()
                    {
                        CurrencyId = pagesaShoferits.CurrencyId,
                        Pagesa = pagesaShoferits.Pagesa,
                        PagesaKryer = pagesaShoferits.PagesaKryer
                    };
                    pagesaShoferitVMs.Add(pagesaShoferitVM);
                }
                ShoferitRrugaVM shoferitRrugaVM = new ShoferitRrugaVM()
                {
                    ShoferId = ShoferRrugas.ShoferId,
                    pagesaShoferitVM = pagesaShoferitVMs
                };
                rruga.ShoferitRrugaVM.Add(shoferitRrugaVM);
                pagesaShoferitVMs = new List<PagesaShoferitVM>();
            }




            //var AllShofer = _context.Shofers.ToList();
            //if (AllShofer != null)
            //{
            //    List<SelectListItem> shofers = new List<SelectListItem>();
            //    foreach (var shofer in AllShofer)
            //    {

            //        shofers.Add(new SelectListItem
            //        {
            //            Text = shofer.Emri,
            //            Value = shofer.ShoferId.ToString(),
            //            //Selected = AllShofer.Count() == 1 ? true : false,
            //            Selected = rruga.Shofer.Emri == shofer.Emri? true : false
            //        });
            //    }
            //    ViewBag.shofers = shofers;
            //}
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

            var AllCurrency2 = _context.Currencys.ToList();
            if (AllCurrency2 != null)
            {
                IDictionary<int, string> numberNames = new Dictionary<int, string>();
                foreach (var currency in AllCurrency)
                {
                    numberNames.Add(currency.CurrencyId, currency.CurrencyUnit);
                }
                ViewBag.numberNames = numberNames;
            }
            var AllShofer2 = _context.Shofers.ToList();
            if (AllShofer2 != null)
            {
                IDictionary<int, string> ShoferIdNames = new Dictionary<int, string>();
                foreach (var Shofer2 in AllShofer2)
                {
                    ShoferIdNames.Add(Shofer2.ShoferId, Shofer2.Emri);
                }
                ViewBag.ShoferIdNames = ShoferIdNames;
            }



            var AllShofer = _context.Shofers.ToList();
            if (AllShofer != null)
            {
                List<SelectListItem> shofers = new List<SelectListItem>();
                foreach (var shofer in AllShofer)
                {
                    shofers.Add(new SelectListItem { Text = shofer.Emri, Value = shofer.ShoferId.ToString() });
                }
                ViewBag.shofers = shofers;
            }
         

            ViewBag.PikaPagesa = _context.PikaShkarkimis.Include(e => e.PagesaPikaShkarkimits).ThenInclude(e => e.Currency).ToList();


            var AllPika = _context.PikaShkarkimis.ToList();
            if (AllPika != null)
            {
                List<SelectListItem> pikas = new List<SelectListItem>();
                foreach (var pika in AllPika)
                {

                    pikas.Add(new SelectListItem
                    {
                        Text = pika.Emri,
                        Value = pika.PikaShkarkimiId.ToString(),
                        Selected = AllPika.Count() == 1 ? true : false
                    });

                }
                if (pikas.Count > 1)
                {
                    pikas.Add(new SelectListItem
                    {
                        Text = "Zgjidh Piken e Shkarkimit",
                        Value = "-1",
                        Selected = true
                    });

                }
                ViewBag.Pikat = pikas;
            }
            ViewBag.pagesa = AllPika.Count() == 1 ? AllPika[0].Pagesa : 0;

            List<PikaRrugaPagesaVM> PikaRrugaPagesaVMs = new List<PikaRrugaPagesaVM>();
            PikaRrugaPagesaVM PikaRrugaPagesaVM = new PikaRrugaPagesaVM()
            {
                CurrencyId = 1,
                Pagesa = 2,
                PagesaKryer = true
            };
            PikaRrugaPagesaVMs.Add(PikaRrugaPagesaVM);
            PikaRrugasVM PikaRrugasVM = new PikaRrugasVM();
            PikaRrugasVM.PikaRrugaPagesaVMs = PikaRrugaPagesaVMs;
        //    PikaRrugasVM.PikaShkarkimiId = 4;
            rruga.PikaRrugasVM.Add(PikaRrugasVM);
            if (rruga.PikaRrugas.Count() == 0)
            {
                PikaShkarkimi pika = new PikaShkarkimi() { Emri = "Zgjidh" };
                List<PikaRruga> pikaRruga = new List<PikaRruga>();
                List<PikaRrugaPagesa> pikaRrugaPagesas = new List<PikaRrugaPagesa>();
                PikaRrugaPagesa pikaRrugaPagesa = new PikaRrugaPagesa()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                pikaRrugaPagesas.Add(pikaRrugaPagesa);
                PikaRruga PikaRruga = new PikaRruga()
                {
                    RrugaId = rruga.RrugaId,
                    PikaShkarkimi = pika,
                    PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault().PikaShkarkimiId,
                    PikaRrugaPagesa = pikaRrugaPagesas,
                    PikaShkarkimiName = "Zgjidh"
                };

                pikaRruga.Add(PikaRruga);
                rruga.PikaRrugas = pikaRruga;
            }
            else
            {
                foreach (var PikaRrugas in rruga.PikaRrugas)
                {
                    PikaRrugas.PikaShkarkimiName = PikaRrugas.PikaShkarkimi.Emri;
                }

            }

            //shpenzime extra
            if (rruga.RrugaShpenzimeEkstras.Count == 0)
            {
                RrugaShpenzimeEkstra rrugaShpenzimeEkstra = new RrugaShpenzimeEkstra()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                rruga.RrugaShpenzimeEkstras.Add(rrugaShpenzimeEkstra);
            }
           
            //fitime extra
            if (rruga.RrugaFitimeEkstras.Count == 0)
            {
                RrugaFitimeEkstra rrugaFitimeEkstra = new RrugaFitimeEkstra()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0
                };
                rruga.RrugaFitimeEkstras.Add(rrugaFitimeEkstra);
            }
           
            //Nafta
            if (rruga.Nafta.Count == 0)
            {
                Nafta nafta = new Nafta()
                {
                    CurrencyId = _context.Currencys.FirstOrDefault().CurrencyId,
                    PagesaKryer = false,
                    Pagesa = 0,
                    Litra = 0,
                };
                rruga.Nafta.Add(nafta);
            }
          




            //  ViewBag.PikaRrugasVM
            return View(rruga);
        }
        //to copy from CreateRrugaJoModel after testing
        [HttpPost]
        public IActionResult EditedRrugaJoModel(int id, Rruga marrngaadd)
        {
            //to calculate all fitimet grouped by currency
            // flag for pagesa kryer 
            //TO TEST to calculate nafta per tu shitur  

            /// to fix llogaritja e shpenzimit te naftes
            /// 

            //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
            Rruga editing = _context.Rrugas
                .Include(e => e.PagesaDoganas)
                .Include(e => e.ShoferRrugas).ThenInclude(e => e.PagesaShoferits)
                .Include(e => e.PikaRrugas).ThenInclude(e => e.PikaShkarkimi)
                .Include(e => e.Nafta)
                .Include(e => e.RrugaShpenzimeEkstras)
                .Include(e => e.RrugaFitimeEkstras)
                .FirstOrDefault(p => p.RrugaId == id);
            editing.Emri = marrngaadd.Emri;
            editing.NaftaShpenzuarLitra = marrngaadd.NaftaShpenzuarLitra;

            // remove dogana
            List<int> pagpikashkarkId = new List<int>();
            foreach (var item in editing.PagesaDoganas)
            {
                var pagpikashakrkimi = _context.PagesaDoganas.FirstOrDefault(e => e.PagesaDoganaId == item.PagesaDoganaId).PagesaDoganaId;
                pagpikashkarkId.Add(pagpikashakrkimi);
            }
            foreach (var item in pagpikashkarkId)
            {
                var pagpikashakrkimi = _context.PagesaDoganas.FirstOrDefault(e => e.PagesaDoganaId == item);

                _context.PagesaDoganas.Remove(pagpikashakrkimi);
            }
            _context.SaveChanges();
            editing.PagesaDoganaVM = marrngaadd.PagesaDoganaVM;

            //Pagesa dogana RRUGA
            //List<PagesaDoganaVM> pagesaDoganas = new List<PagesaDoganaVM>();
            //foreach (var PagesaDoganaVM in marrngaadd.PagesaDoganaVM)
            //{
            //    PagesaDogana PagesaShoferit = new PagesaDogana()
            //    {
            //        CurrencyId = PagesaDoganaVM.CurrencyId,
            //        Pagesa = PagesaDoganaVM.Pagesa,
            //        RrugaId = id,
            //        PagesaKryer = PagesaDoganaVM.PagesaKryer,
            //        ShpenzimXhiro = true
            //    };
            //    pagesaDoganas.Add(PagesaShoferit);
            //}
            //editing.PagesaDoganas = pagesaDoganas;
            

            // remove shofer
            List<int> shoferRrugaId = new List<int>();
            foreach (var item in editing.ShoferRrugas)
            {
                var pagpikashakrkimi = _context.ShoferRrugas.FirstOrDefault(e => e.ShoferRrugaId == item.ShoferRrugaId).ShoferRrugaId;
                shoferRrugaId.Add(pagpikashakrkimi);
            }
            foreach (var item in shoferRrugaId)
            {
                var pagpikashakrkimi = _context.ShoferRrugas.FirstOrDefault(e => e.ShoferRrugaId == item);

                _context.ShoferRrugas.Remove(pagpikashakrkimi);
            }


            _context.SaveChanges();

            editing.ShoferitRrugaVM = marrngaadd.ShoferitRrugaVM;


            ////Shofer RRUGA
            //foreach (var ShoferitRrugaVM in marrngaadd.ShoferitRrugaVM)
            //{

            //    ShoferRruga shoferRruga = new ShoferRruga()
            //    {
            //        ShoferId = ShoferitRrugaVM.ShoferId,
            //        RrugaId = id,
            //    };

            //    _context.Add(shoferRruga);
            //    _context.SaveChanges();
            //    foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
            //    {
            //        PagesaShoferit pagesaShoferit = new PagesaShoferit()
            //        {
            //            CurrencyId = PagesaShoferitVM.CurrencyId,
            //            ShoferRrugaId = shoferRruga.ShoferRrugaId,
            //            Pagesa = PagesaShoferitVM.Pagesa,
            //            PagesaKryer = PagesaShoferitVM.PagesaKryer,
            //            ShpenzimXhiro = true,
            //        };
            //        _context.Add(pagesaShoferit);
            //        _context.SaveChanges();
            //    }
            //}

            ////
            ////
            ///
            ////// pika rruga
            List<int> PikaRrugaId = new List<int>();
            foreach (var item in editing.PikaRrugas)
            {
                var pagpikashakrkimi = _context.PikaRrugas.FirstOrDefault(e => e.PikaShkarkimiId == item.PikaShkarkimiId).PikaShkarkimiId;
                PikaRrugaId.Add(pagpikashakrkimi);
            }
            foreach (var item in PikaRrugaId)
            {
                var pagpikashakrkimi = _context.PikaRrugas.FirstOrDefault(e => e.PikaShkarkimiId == item);

                _context.PikaRrugas.Remove(pagpikashakrkimi);
            }

            _context.SaveChanges();

            //pika RRUGA
            if (editing.PikaRrugas.FirstOrDefault() != null)
            {
                if(editing.PikaRrugas.FirstOrDefault().PikaShkarkimi.Emri == "Zgjidh")
                editing.PikaRrugas = null;
                else
                {
                    foreach (var pikarruga in marrngaadd.PikaRrugas)
                    {
                        int PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault(e => e.Emri == pikarruga.PikaShkarkimiName).PikaShkarkimiId;

                        PikaRrugasVM shoferRruga = new PikaRrugasVM()
                        {
                            PikaShkarkimiId = PikaShkarkimiId,
                            // RrugaId = id,
                        };

                        //   _context.Add(shoferRruga);
                        //  _context.SaveChanges();
                        foreach (var Pagesapika in pikarruga.PikaRrugaPagesa)
                        {
                            PikaRrugaPagesaVM pagesaShoferit = new PikaRrugaPagesaVM()
                            {
                                CurrencyId = Pagesapika.CurrencyId,
                                //   PikaRrugaId = shoferRruga.PikaRrugaId,
                                Pagesa = Pagesapika.Pagesa,
                                PagesaKryer = Pagesapika.PagesaKryer,
                                //  ShpenzimXhiro = true,
                            };
                            shoferRruga.PikaRrugaPagesaVMs.Add(pagesaShoferit);
                        }
                        editing.PikaRrugasVM.Add(shoferRruga);
                    }
                    editing.PikaRrugas = null;
                }
            }
            
            /// shpenzime ekstra
            List<int> shpenzimeEkstraId = new List<int>();
            foreach (var item in editing.RrugaShpenzimeEkstras)
            {
                var pagpikashakrkimi = _context.RrugaShpenzimeEkstras.FirstOrDefault(e => e.RrugaShpenzimeEkstraId == item.RrugaShpenzimeEkstraId).RrugaShpenzimeEkstraId;
                PikaRrugaId.Add(pagpikashakrkimi);
            }
            foreach (var item in shpenzimeEkstraId)
            {
                var pagpikashakrkimi = _context.RrugaShpenzimeEkstras.FirstOrDefault(e => e.RrugaShpenzimeEkstraId == item);

                _context.RrugaShpenzimeEkstras.Remove(pagpikashakrkimi);
            }

            _context.SaveChanges();

            editing.RrugaShpenzimeEkstras = marrngaadd.RrugaShpenzimeEkstras;

            /// xhiro ekstra
            List<int> XhiroEkstraId = new List<int>();
            foreach (var item in editing.RrugaFitimeEkstras)
            {
                var pagpikashakrkimi = _context.RrugaFitimeEkstras.FirstOrDefault(e => e.RrugaFitimeEkstraId == item.RrugaFitimeEkstraId).RrugaFitimeEkstraId;
                XhiroEkstraId.Add(pagpikashakrkimi);
            }
            foreach (var item in XhiroEkstraId)
            {
                var pagpikashakrkimi = _context.RrugaFitimeEkstras.FirstOrDefault(e => e.RrugaFitimeEkstraId == item);

                _context.RrugaFitimeEkstras.Remove(pagpikashakrkimi);
            }
            editing.RrugaFitimes = null;
            _context.SaveChanges();

            editing.RrugaFitimeEkstras = marrngaadd.RrugaFitimeEkstras;


            /// nafata
            List<int> NaftaId = new List<int>();
            foreach (var item in editing.Nafta)
            {
                var pagpikashakrkimi = _context.Naftas.FirstOrDefault(e => e.NaftaId == item.NaftaId).NaftaId;
                NaftaId.Add(pagpikashakrkimi);
            }
            foreach (var item in NaftaId)
            {
                var pagpikashakrkimi = _context.Naftas.FirstOrDefault(e => e.NaftaId == item);

                _context.Naftas.Remove(pagpikashakrkimi);
            }

            _context.SaveChanges();

            editing.Nafta = marrngaadd.Nafta;

            ///naftasStocks
            ///
            //List<int> NaftaStockId = new List<int>();
            //foreach (var item in editing.Nafta)
            //{
            //    var pagpikashakrkimi = _context.NaftaStocks.FirstOrDefault(e => e.NaftaStockId == item.NaftaStockId).NaftaStockId;
            //    PikaRrugaId.Add(pagpikashakrkimi);
            //}
            //foreach (var item in NaftaStockId)
            //{
            //    var pagpikashakrkimi = _context.NaftaStocks.FirstOrDefault(e => e.NaftaId == item);

            //    _context.Naftas.Remove(pagpikashakrkimi);
            //}
            var naftaStocksList = _context.NaftaStocks.Where(e => e.RrugaId == editing.RrugaId).ToList();

            foreach (var item in naftaStocksList)
            {
                _context.NaftaStocks.Remove(item);
            }
            editing.PagesaKryer = true;


            var Currencys = _context.Currencys.ToList();
            List<RrugaFitime> rrugaFitime = new List<RrugaFitime>();
            List<RrugaFitime> rrugaFitimeShpenzims = new List<RrugaFitime>();
            List<RrugaFitime> rrugaFitimeXhiros = new List<RrugaFitime>();

            bool PagesaKryer = true;

            foreach (var item in Currencys)
            {
                RrugaFitime rrugaFitimeShpenzim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = true
                };
                rrugaFitimeShpenzims.Add(rrugaFitimeShpenzim);
                RrugaFitime rrugaFitimeXhiro = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = false
                };
                rrugaFitimeXhiros.Add(rrugaFitimeXhiro);
                RrugaFitime rrugaFitim = new RrugaFitime()
                {
                    CurrencyId = item.CurrencyId,
                    ShpenzimXhiro = false
                };
                rrugaFitime.Add(rrugaFitim);
            }

         

            if (marrngaadd.shenime == null) marrngaadd.shenime = "test";

            //RrugaShpenzimeEkstras
            foreach (var RrugaShpenzimeEkstra in editing.RrugaShpenzimeEkstras)
            {
                if (RrugaShpenzimeEkstra.Pagesa == 0)
                {
                    RrugaShpenzimeEkstra.PagesaKryer = true;
                }

            }
            //RrugaFitimeEkstras
            foreach (var RrugaFitimeEkstra in editing.RrugaFitimeEkstras)
            {
                if (RrugaFitimeEkstra.Pagesa == 0)
                {
                    RrugaFitimeEkstra.PagesaKryer = true;
                }
            }
            //  Pika Nafta
            foreach (var Nafta in editing.Nafta)
            {

                if (Nafta.Litra == 0)
                {
                    Nafta.PagesaKryer = true;
                }
                Nafta.BlereShiturSelect = "Blere";
            }


            ///// shtimi i rruges
            //_context.Add(marrngaadd);
            //_context.SaveChanges();

            //Pagesa dogana RRUGA
            foreach (var PagesaDoganaVM in editing.PagesaDoganaVM)
            {
                if (PagesaDoganaVM.Pagesa == 0)
                {
                    PagesaDoganaVM.PagesaKryer = true;
                }
                PagesaDogana PagesaShoferit = new PagesaDogana()
                {
                    CurrencyId = PagesaDoganaVM.CurrencyId,
                    Pagesa = PagesaDoganaVM.Pagesa,
                    RrugaId = editing.RrugaId,
                    PagesaKryer = PagesaDoganaVM.PagesaKryer,
                    ShpenzimXhiro = true
                };
                _context.Add(PagesaShoferit);
                _context.SaveChanges();
            }

            //Shofer RRUGA
            foreach (var ShoferitRrugaVM in editing.ShoferitRrugaVM)
            {
                ShoferRruga shoferRruga = new ShoferRruga()
                {
                    ShoferId = ShoferitRrugaVM.ShoferId,
                    RrugaId = editing.RrugaId,
                };

                _context.Add(shoferRruga);
                _context.SaveChanges();
                foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                {
                    if (PagesaShoferitVM.Pagesa == 0)
                    {
                        PagesaShoferitVM.PagesaKryer = true;
                    }
                    PagesaShoferit pagesaShoferit = new PagesaShoferit()
                    {
                        CurrencyId = PagesaShoferitVM.CurrencyId,
                        ShoferRrugaId = shoferRruga.ShoferRrugaId,
                        Pagesa = PagesaShoferitVM.Pagesa,
                        PagesaKryer = PagesaShoferitVM.PagesaKryer,
                        ShpenzimXhiro = true,
                    };
                    _context.Add(pagesaShoferit);
                    _context.SaveChanges();
                }
            }


            // Pika Rruga
            if (editing.PikaRrugasVM != null)
            {
                foreach (var PikaRrugaVM in editing.PikaRrugasVM)
                {
                    int PikaShkarkimiId = _context.PikaShkarkimis.FirstOrDefault(e => e.PikaShkarkimiId == PikaRrugaVM.PikaShkarkimiId).PikaShkarkimiId;

                    PikaRruga pikaRruga = new PikaRruga()
                    {
                        RrugaId = editing.RrugaId,
                        PikaShkarkimiId = PikaShkarkimiId,
                    };

                    foreach (var PikaRrugaPagesaVM in PikaRrugaVM.PikaRrugaPagesaVMs)
                    {

                        if (PikaRrugaPagesaVM.Pagesa == 0)
                        {
                            PikaRrugaPagesaVM.PagesaKryer = true;
                        }
                        PikaRrugaPagesa pikaRrugaPagesa = new PikaRrugaPagesa()
                        {
                            CurrencyId = PikaRrugaPagesaVM.CurrencyId,
                            Pagesa = PikaRrugaPagesaVM.Pagesa,
                            PagesaKryer = PikaRrugaPagesaVM.PagesaKryer
                        };
                        pikaRruga.PikaRrugaPagesa.Add(pikaRrugaPagesa);
                    }
                    _context.Add(pikaRruga);
                    _context.SaveChanges();
                }
            }


            ///
            //// NAFTA PART
            //llogaritja e naftes se blere
            decimal naftaBlereLitra = 0;
            decimal NaftaShpenzuarLitra = editing.NaftaShpenzuarLitra;
            foreach (var Nafta in editing.Nafta)
            {
                Nafta.BlereShiturSelect = "Blere";
                naftaBlereLitra = Nafta.Litra + naftaBlereLitra;
            }
            editing.NaftaBlereLitra = naftaBlereLitra;
            // to add nafta si naftastock
            // to add nafta si shpenzim

            //to fix nafta blere no metter the currency
            int naftaCount = editing.Nafta.Count();
            List<NaftaStock> naftaStocks = new List<NaftaStock>();
            for (int i = 0; i < naftaCount; i++)
            {
                //if (naftaBlereLitra <= NaftaShpenzuarLitra)
                //{ }
                if (editing.Nafta[i].Litra <= NaftaShpenzuarLitra && i != naftaCount - 1)
                {
                    //naftaBlereLitra = naftaBlereLitra - marrngaadd.Nafta[i].Litra;
                    NaftaShpenzuarLitra = NaftaShpenzuarLitra - editing.Nafta[i].Litra;
                    //shtohet pagesa e plote e naftes si shpenzim
                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == editing.Nafta[i].CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + editing.Nafta[i].Pagesa;
                    if (marrngaadd.Nafta[i].PagesaKryer == true)
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + editing.Nafta[i].Pagesa;
                    }
                }
                else
                {
                    decimal cmim = editing.Nafta[i].Pagesa / editing.Nafta[i].Litra;
                    decimal litraMbetur = editing.Nafta[i].Litra - NaftaShpenzuarLitra;
                    if (litraMbetur >= 0)
                    {
                        decimal pagesashpenzim = cmim * NaftaShpenzuarLitra;
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == editing.Nafta[i].CurrencyId);
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + pagesashpenzim;
                        if (marrngaadd.Nafta[i].PagesaKryer == true)
                        {
                            rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + pagesashpenzim;
                        }
                    }
                    NaftaShpenzuarLitra = 0;
                    decimal pagesaNaftaMbetur = litraMbetur * cmim;
                    //decimal shpenzim = NaftaShpenzuarLitra * cmim;

                    NaftaStock naftaStock = new NaftaStock()
                    {
                        Litra = litraMbetur,
                        Pagesa = pagesaNaftaMbetur,
                        PagesaKryer = marrngaadd.Nafta[i].PagesaKryer,
                        CurrencyId = editing.Nafta[i].CurrencyId,
                        RrugaId = editing.RrugaId,
                        //  BlereShiturSelect = "Blere",
                    };
                    naftaStocks.Add(naftaStock);
                }
            }

            //menyra si llogaritet nafta e blere nga rruga

            // shtimi i naftes se blere nese litrat jane pozitive
            // shtimi i nje cifti blere shitur nese nafta eshte negative
            // nese cmimi referenc i naftes stock eshte negativ 
            // perdoret si (negativ) cmim per naften e blere cmimi i naftes se qe eshte bere negativ


            foreach (var naftastock in naftaStocks)
            {
                if (naftastock.Litra == 0)
                    continue;
                if (naftastock.Litra > 0)
                {
                    naftastock.BlereShiturSelect = "Blere";
                    ////shtohet pagesa per litrat e mbetura te naftes si shpenzim
                    //var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == naftastock.CurrencyId);
                    //rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + naftastock.Pagesa;

                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == naftastock.CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + naftastock.Pagesa;
                    if (naftastock.PagesaKryer)
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + naftastock.Pagesa;
                    }

                    _context.Add(naftastock);
                    _context.SaveChanges();
                    continue;
                }
                if (naftastock.Litra < 0)
                {
                    BlereShitur blereShitur = new BlereShitur();
                    _context.Add(blereShitur);
                    _context.SaveChanges();


                    //marrja e listes se currencyve te naftes stock dhe cmimit ref pozitiv

                    List<CurrCmimRef> currCmimRefs = new List<CurrCmimRef>();
                    List<int> naftaStocksCurrencyId = _context.NaftaStocks.GroupBy(e => e.CurrencyId).Select(e => e.Key).ToList();
                    foreach (var CurrencyId in naftaStocksCurrencyId)
                    {
                        var cmimRef = _context.NaftaStocks.Where(e => e.BlereShiturSelect == "Blere")
                       //.Where(e => e.Litra > 0 && e.Leke > 0)
                       .GroupBy(e => e.CurrencyId == CurrencyId)
                       .Select(e =>
                       (e.Sum(b => b.Pagesa) / e.Sum(b => b.Litra))
                               )
                       .FirstOrDefault();
                        if (cmimRef > 0)
                        {
                            CurrCmimRef currCmimRef = new CurrCmimRef()
                            {
                                CmimRef = cmimRef,
                                CurrencyId = CurrencyId
                            };
                            currCmimRefs.Add(currCmimRef);
                        }
                    }
                    //krijimi i naftes se blere (negative)
                    NaftaStock naftablereStock = new NaftaStock()
                    {
                        Litra = naftastock.Litra,
                        // Pagesa = pagesaNaftaMbetur,
                        PagesaKryer = naftastock.PagesaKryer,
                        //  CurrencyId = naftastock.CurrencyId,
                        RrugaId = editing.RrugaId,
                        BlereShiturSelect = "Blere",
                        BlereShiturId = blereShitur.BlereShiturId
                    };

                    if (currCmimRefs.Count > 0)
                    {
                        var currCmimRef = currCmimRefs.FirstOrDefault();
                        naftablereStock.Pagesa = naftablereStock.Litra * currCmimRef.CmimRef;
                        naftablereStock.CurrencyId = currCmimRef.CurrencyId;
                        naftablereStock.Shenime = "cmim ref";
                        //shtohet pagesa per litrat (negative) me cmimin nga cmimi references te naftes si shpenzim
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == currCmimRef.CurrencyId);
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + (0 - naftablereStock.Pagesa);


                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + (0 - naftablereStock.Pagesa);
                    }
                    else
                    {
                        decimal cmim = (0 - naftastock.Pagesa) / (0 - naftastock.Litra);
                        naftablereStock.Pagesa = naftastock.Litra * cmim;
                        naftablereStock.Shenime = "cmim nafta blere nga rruga";
                        //shtohet pagesa per litrat (negative) me cmim nga nafta blere nga rruga te naftes si shpenzim
                        var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == naftastock.CurrencyId);
                        naftablereStock.CurrencyId = naftastock.CurrencyId;
                        rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa - naftablereStock.Pagesa;

                        if (naftastock.PagesaKryer == true)
                        {
                            rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale - naftablereStock.Pagesa;
                        }


                    }

                    naftastock.BlereShiturSelect = "Shitur";
                    //shto shtije cmim 0 litra +
                    naftastock.Litra = 0 - naftastock.Litra;
                    naftastock.Pagesa = 0;
                    naftastock.BlereShiturId = blereShitur.BlereShiturId;

                    _context.Add(naftastock);
                    _context.Add(naftablereStock);
                    _context.SaveChanges();

                    
                }

            }


            /////// Fitime part 

            // Shpenzim part      dogana 
            foreach (var PagesaDoganaVM in editing.PagesaDoganaVM)
            {
                //if (PagesaDoganaVM.PagesaKryer == false)
                //{
                //    PagesaKryer = false;
                //}
                //var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaDoganaVM.CurrencyId);
                //rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaDoganaVM.Pagesa;

                var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaDoganaVM.CurrencyId);
                rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaDoganaVM.Pagesa;
                if (PagesaDoganaVM.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + PagesaDoganaVM.Pagesa;
                }

            }

            // Shpenzim part   Shofer RRUGA
            foreach (var ShoferitRrugaVM in editing.ShoferitRrugaVM)
            {
                foreach (var PagesaShoferitVM in ShoferitRrugaVM.pagesaShoferitVM)
                {
                    //if (PagesaShoferitVM.PagesaKryer == false)
                    //{
                    //    PagesaKryer = false;
                    //}
                    //var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaShoferitVM.CurrencyId);
                    //rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaShoferitVM.Pagesa;
                    var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == PagesaShoferitVM.CurrencyId);
                    rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + PagesaShoferitVM.Pagesa;
                    if (PagesaShoferitVM.PagesaKryer == false)
                    {
                        PagesaKryer = false;
                    }
                    else
                    {
                        rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + PagesaShoferitVM.Pagesa;
                    }
                }
            }

            // Fitim part  Pika Rruga
            if (editing.PikaRrugasVM != null)
            {
                foreach (var PikaRrugaVM in editing.PikaRrugasVM)
                {
                    foreach (var PikaRrugaPagesaVM in PikaRrugaVM.PikaRrugaPagesaVMs)
                    {
                        //if (PikaRrugaPagesaVM.PagesaKryer == false)
                        //{
                        //    PagesaKryer = false;
                        //}
                        //var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == PikaRrugaPagesaVM.CurrencyId);
                        //rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + PikaRrugaPagesaVM.Pagesa;

                        var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == PikaRrugaPagesaVM.CurrencyId);
                        rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + PikaRrugaPagesaVM.Pagesa;
                        if (PikaRrugaPagesaVM.PagesaKryer == false)
                        {
                            PagesaKryer = false;
                        }
                        else
                        {
                            rrugaFitimeXhiro.PagesaReale = rrugaFitimeXhiro.PagesaReale + PikaRrugaPagesaVM.Pagesa;
                        }
                    }

                }
            }
            //  Shpenzim part    RrugaShpenzimeEkstras
            foreach (var RrugaShpenzimeEkstra in editing.RrugaShpenzimeEkstras)
            {
                //if (RrugaShpenzimeEkstra.PagesaKryer == false)
                //{
                //    PagesaKryer = false;
                //}
                //var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == RrugaShpenzimeEkstra.CurrencyId);
                //rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + RrugaShpenzimeEkstra.Pagesa;
                var rrugaFitimeShpenzim = rrugaFitimeShpenzims.FirstOrDefault(e => e.CurrencyId == RrugaShpenzimeEkstra.CurrencyId);
                rrugaFitimeShpenzim.Pagesa = rrugaFitimeShpenzim.Pagesa + RrugaShpenzimeEkstra.Pagesa;
                if (RrugaShpenzimeEkstra.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeShpenzim.PagesaReale = rrugaFitimeShpenzim.PagesaReale + RrugaShpenzimeEkstra.Pagesa;

                }
            }
            //  Fitim part    RrugaFitimeEkstras
            foreach (var RrugaFitimeEkstra in editing.RrugaFitimeEkstras)
            {
                //if (RrugaFitimeEkstra.PagesaKryer == false)
                //{
                //    PagesaKryer = false;
                //}
                //var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == RrugaFitimeEkstra.CurrencyId);
                //rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + RrugaFitimeEkstra.Pagesa;
                var rrugaFitimeXhiro = rrugaFitimeXhiros.FirstOrDefault(e => e.CurrencyId == RrugaFitimeEkstra.CurrencyId);
                rrugaFitimeXhiro.Pagesa = rrugaFitimeXhiro.Pagesa + RrugaFitimeEkstra.Pagesa;
                if (RrugaFitimeEkstra.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
                else
                {
                    rrugaFitimeXhiro.PagesaReale = rrugaFitimeXhiro.PagesaReale + RrugaFitimeEkstra.Pagesa;

                }
            }

            foreach (var RrugaNafte in editing.Nafta)
            {
                if (RrugaNafte.PagesaKryer == false)
                {
                    PagesaKryer = false;
                }
            }
            // to calulate fitime 
            foreach (var rrugaFitimeXhiro in rrugaFitimeXhiros)
            {
                var rrugaFitimecurr = rrugaFitime.FirstOrDefault(e => e.CurrencyId == rrugaFitimeXhiro.CurrencyId);
                rrugaFitimecurr.Pagesa = rrugaFitimecurr.Pagesa + rrugaFitimeXhiro.Pagesa;
                rrugaFitimecurr.PagesaReale = rrugaFitimecurr.PagesaReale + rrugaFitimeXhiro.PagesaReale;
            }
            foreach (var rrugaFitimeShpenzim in rrugaFitimeShpenzims)
            {
                var rrugaFitimecurr = rrugaFitime.FirstOrDefault(e => e.CurrencyId == rrugaFitimeShpenzim.CurrencyId);
                rrugaFitimecurr.Pagesa = rrugaFitimecurr.Pagesa - rrugaFitimeShpenzim.Pagesa;
                rrugaFitimecurr.PagesaReale = rrugaFitimecurr.PagesaReale - rrugaFitimeShpenzim.PagesaReale;
            }

            editing.RrugaFitimes = rrugaFitime;
            editing.PagesaKryer = PagesaKryer;
            _context.SaveChanges();
            return RedirectToAction("AllRrugaJoModel");

        }




        public IActionResult FshiRrugaJoModel(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            Rruga removingShofer = _context.Rrugas
                .Include(e=>e.Nafta)
                .FirstOrDefault(p => p.RrugaId == id);
            _context.Rrugas.Remove(removingShofer);
            _context.SaveChanges();
            return RedirectToAction("AllRrugaJoModel");

        }

    //    public JsonResult GetPikaRrugaPagesa(int pikaShkarkimiId)
    //    {
    //        var pikaRrugaPagesaItems = // Retrieve PikaRrugaPagesa items based on the selected PikaShkarkimiId

    //return Json(pikaRrugaPagesaItems);
    //    }





    }
    public class CurrCmimRef
    {
        public decimal CmimRef { get; set; }
        public int CurrencyId { get; set; }
    }
}
