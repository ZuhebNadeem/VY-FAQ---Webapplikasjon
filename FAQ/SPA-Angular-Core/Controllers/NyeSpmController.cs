using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SPA_Angular_Core;
using SPA_Angular_Core.Models;


namespace FAQ.Controllers
{
    [Route("api/[controller]")]
    public class NyeSpmController : Controller
    {
        private KundeContext _context;

            public NyeSpmController(KundeContext context)
            {
                _context = context;
            }

            // GET api/NyeSpm. Når man skal hente alle nye spm. 
            [HttpGet]
            public JsonResult Get()
            {
                var kundeDb = new DB(_context);
                List<NyeSpm> alleNyeSpm = kundeDb.hentAlleNyeSpm();
                return Json(alleNyeSpm);
            }
    }
}