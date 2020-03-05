using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SPA_Angular_Core;
using SPA_Angular_Core.Models;

namespace FAQ.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private KundeContext _context;

        public HomeController(KundeContext context)
        {
            _context = context;
        } 


        // GET api/Kunde. Når man skal hente alle FAQ.
        [HttpGet]
        public JsonResult Get()
        {
            var kundeDb = new DB(_context);
            List<DomeneFAQ> alleFAQ = kundeDb.hentAlleFAQ();
            return Json(alleFAQ);
        }


        // Put api/Kunde. Når man skal lagre antall tommel opp. 
        [HttpPut]
        public JsonResult Put([FromBody] DomeneFAQ innAntallLikerKlikk)
        {
            var kundeDb = new DB(_context);
            bool OK = kundeDb.lagreAntallLikerKlikk(innAntallLikerKlikk);

            if (OK)
            {
                return Json("OK");
            }
            return Json("Kunne ikke endre kunden i DB");
        }



        // POST api/Kunde. Når man skal lagre nye spørsmål fra en kunde. 
        [HttpPost]
        public JsonResult Post([FromBody] NyeSpm innSpm)
        {
            if (ModelState.IsValid)
            {
                var kundeDb = new DB(_context);
                bool OK = kundeDb.lagreNyttSpm(innSpm);
                if (OK)
                {
                    return Json("OK");
                }
            }

            return Json("Kunne ikke sette inn kunden i DB");
        }




    }

}