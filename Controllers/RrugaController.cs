﻿using BioLab.Models;
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
                .Include(e=>e.Nafta)
                .Where(e => e.Model == false)
                                    .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true)
                .ToList();
            //foreach (var shofer in shofers)
            //{
            //    if(shofer.Nafta.Litra)
            //}

            if (shofers != null)
            {
                ViewBag.Shofers = shofers;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Shofers = shofers.Where(s => s.Emri!.Contains(searchString))
                                        .Where(m => searchFirstTime != DateTime.MinValue ? m.CreatedDate > searchFirstTime : true)
                    .Where(m => searchSecondTime != DateTime.MinValue ? m.CreatedDate > searchSecondTime : true);
            }


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
            // to calculate nafta per tu shitur

            RrugaFitime rrugaFitime = new RrugaFitime() { };
/////
            decimal naftaBlereLitra = 0;
            decimal NaftaShpenzuarLitra = marrngaadd.NaftaShpenzuarLitra;
            foreach (var Nafta in marrngaadd.Nafta)
            {
                Nafta.BlereShiturSelect = "Blere";
                naftaBlereLitra = Nafta.Litra + naftaBlereLitra;
            }
            if (marrngaadd.Nafta.Count() == 1)
            {
                marrngaadd.Nafta.First().Litra = naftaBlereLitra - marrngaadd.NaftaShpenzuarLitra;
            }
            if (marrngaadd.Nafta.Count() >1)
            {
                int naftaCount = marrngaadd.Nafta.Count();
                for (int i = 0; i < marrngaadd.Nafta.Count(); i++)
                {
                    if (i < naftaCount-1 )
                    {
                        if (marrngaadd.Nafta[i].Litra < NaftaShpenzuarLitra)
                        {
                            decimal litra = marrngaadd.Nafta[i].Litra;
                            NaftaShpenzuarLitra = NaftaShpenzuarLitra - litra;
                            marrngaadd.Nafta[i].Litra = 0;
                        }
                        else
                        {
                            marrngaadd.Nafta[i].Litra = marrngaadd.Nafta[i].Litra - NaftaShpenzuarLitra;
                        }
                    }
                    else 
                    {
                        
                            marrngaadd.Nafta[i].Litra = marrngaadd.Nafta[i].Litra- NaftaShpenzuarLitra;
                        
                    }

                }
                //foreach (var Nafta in marrngaadd.Nafta)
                //{
                //     if(Nafta.Litra< NaftaShpenzuarLitra)
                //    {
                //        decimal litra = Nafta.Litra;
                //        NaftaShpenzuarLitra = NaftaShpenzuarLitra - litra;
                //        Nafta.Litra = 0;
                //    }
                //}

            }

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

                if (Nafta.Pagesa == 0)
                {
                    Nafta.PagesaKryer = true;
                }
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
           



            //if (ModelState.IsValid)
            //{
            //    marrngaadd.Model = false;
            //    var shofer = _context.Shofers.FirstOrDefault(sh => sh.ShoferId == marrngaadd.ShoferId);
            //    var pikaShkarkimi = _context.PikaShkarkimis.FirstOrDefault(sh => sh.PikaShkarkimiId == marrngaadd.PikaShkarkimiId);

            //    decimal shpenzime = 0;
            //    if (marrngaadd.NaftaBlereLitra != 0)
            //    {

            //        if (marrngaadd.NaftaBlereCmim == 0)
            //        {
            //            var cmimRef = _context.Naftas
            //       //.Where(e => e.Litra > 0 && e.Leke > 0)
            //       .GroupBy(e => e.BlereShiturSelect == "Blere")
            //       .Select(e =>
            //       (e.Sum(b => b.Leke) / e.Sum(b => b.Litra))
            //               )
            //       .FirstOrDefault();
            //            marrngaadd.NaftaBlereCmim = cmimRef;
            //        }
            //    shpenzime = marrngaadd.PagesaShoferit + marrngaadd.Dogana + marrngaadd.shpenzimeEkstra + (marrngaadd.NaftaShpenzuarLitra * marrngaadd.NaftaBlereCmim);
            //    }

            //    var xhiro = pikaShkarkimi.Pagesa + marrngaadd.FitimeEkstra;

            //    marrngaadd.Fitime = xhiro - shpenzime;
            //    marrngaadd.Shpenzime = shpenzime;
            //    marrngaadd.Xhiro = xhiro;

            //    if (marrngaadd.NaftaBlereLitra != 0)
            //    {
            //        if(marrngaadd.NaftaBlereLitra > marrngaadd.NaftaPerTuShiturLitra)
            //        {

            //        marrngaadd.NaftaPerTuShiturLitra = marrngaadd.NaftaBlereLitra - marrngaadd.NaftaShpenzuarLitra;

            //        _context.Add(marrngaadd);
            //        _context.SaveChanges();

            //        Nafta nafta = new Nafta
            //        {
            //            BlereShiturSelect = "Blere",
            //            Cmimi = marrngaadd.NaftaBlereCmim,
            //            Leke = (marrngaadd.NaftaPerTuShiturLitra * marrngaadd.NaftaBlereCmim),
            //            Litra = marrngaadd.NaftaPerTuShiturLitra,
            //            RrugaId = marrngaadd.RrugaId
            //        };
            //        _context.Add(nafta);
            //        _context.SaveChanges();
            //        }
            //        else
            //        {
            //       // marrngaadd.NaftaPerTuShiturLitra = marrngaadd.NaftaBlereLitra - marrngaadd.NaftaShpenzuarLitra;

            //        }

            //    }
            //    else
            //    {
            //        marrngaadd.NaftaPerTuShiturLitra = 0;
            //        _context.Add(marrngaadd);
            //        _context.SaveChanges();
            //    }
            return View("AllRrugaJoModel");

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
            var rruga = _context.Rrugas.FirstOrDefault(e => e.RrugaId == id);
            //ViewBag.id = id;
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
            //            Selected = shofer.ShoferId == rruga.ShoferId ? true : false
            //        });

            //    }
            //    ViewBag.shofers = shofers;
            //}
            //var AllPika = _context.PikaShkarkimis.ToList();
            //if (AllPika != null)
            //{
            //    List<SelectListItem> pikas = new List<SelectListItem>();
            //    foreach (var pika in AllPika)
            //    {

            //        pikas.Add(new SelectListItem
            //        {
            //            Text = pika.Emri,
            //            Value = pika.PikaShkarkimiId.ToString(),
            //            Selected = pika.PikaShkarkimiId == rruga.PikaShkarkimiId ? true : false
            //        });

            //    }
            //    ViewBag.Pikat = pikas;
            //}
            return View(rruga);
        }

        [HttpPost]
        public IActionResult EditedRrugaJoModel(int id, Rruga marrngaadd)
        {
            if (ModelState.IsValid)
            {
                //bejm kontrollin nese ekziston nje analize me kte emer e krijuar nga admini i loguar
                if (_context.Rrugas.Where(sh => sh.RrugaId != id && sh.Model==false).Any(u => u.Emri == marrngaadd.Emri))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Emri", "Ekziston nje user me kete emer!");

                    return RedirectToAction("EditRrugaJoModel", new { id = id });
                }
                //marrim nga db anzlizen qe duam te bejm edit dhe vendosim vlerat qe marim nga forma
                Rruga editing = _context.Rrugas.FirstOrDefault(p => p.RrugaId == id);
                editing.Emri = marrngaadd.Emri;
             //   editing.Dogana = marrngaadd.Dogana;
                editing.NaftaShpenzuarLitra = marrngaadd.NaftaShpenzuarLitra;
            //    editing.NaftaPerTuShiturLitra = marrngaadd.NaftaPerTuShiturLitra;
             //   editing.NaftaBlereCmim = marrngaadd.NaftaBlereCmim;
             //   editing.NaftaBlereLitra = marrngaadd.NaftaBlereLitra;
                //editing.PikaShkarkimiId = marrngaadd.PikaShkarkimiId;
                //editing.ShoferId = marrngaadd.ShoferId;
                editing.shenime = marrngaadd.shenime;
              //  editing.shpenzimeEkstra = marrngaadd.shpenzimeEkstra;
              //  editing.FitimeEkstra = marrngaadd.FitimeEkstra;


                //var shofer = _context.Shofers.FirstOrDefault(sh => sh.ShoferId == marrngaadd.ShoferId);
                //var pikaShkarkimi = _context.PikaShkarkimis.FirstOrDefault(sh => sh.PikaShkarkimiId == marrngaadd.PikaShkarkimiId);

                //var shpenzime = shofer.Pagesa + marrngaadd.Dogana + marrngaadd.shpenzimeEkstra + marrngaadd.NaftaShpenzuarLitra * marrngaadd.NaftaBlereCmim;
                //var xhiro = pikaShkarkimi.Pagesa + marrngaadd.FitimeEkstra;

                //editing.Fitime = xhiro - shpenzime;
                //editing.Shpenzime = shpenzime;
                //editing.Xhiro = xhiro;

                //if (marrngaadd.NaftaBlereLitra != 0)
                //{
                //    editing.NaftaPerTuShiturLitra = marrngaadd.NaftaBlereLitra - marrngaadd.NaftaShpenzuarLitra;

                //    var nafta = _context.Naftas.Find(marrngaadd.RrugaId);
                //    if (nafta != null)
                //    {
                //        nafta.Cmimi = marrngaadd.NaftaBlereCmim;
                //        nafta.Leke = (marrngaadd.NaftaPerTuShiturLitra * marrngaadd.NaftaBlereCmim);
                //        nafta.Litra = marrngaadd.NaftaPerTuShiturLitra;
                //    }
                //}

                _context.SaveChanges();
                return RedirectToAction("AllRrugaJoModel");
            }
            return RedirectToAction("EditRrugaJoModel", new { id = id });
        }




        public IActionResult FshiRrugaJoModel(int id)
        {

            //fshijme analizen e marre nga db me analizId si parametri id
            Rruga removingShofer = _context.Rrugas.FirstOrDefault(p => p.RrugaId == id);
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
}
