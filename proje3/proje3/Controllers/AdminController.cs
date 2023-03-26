using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Neo4j.Driver;
using proje3.GarphDatabase;

namespace proje3.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddResearcherAsync(string id, string firstname, string lastname)
        {

            using (var aura = new Neo4jAura())
            {
                await aura.CreateResearcher(id, firstname, lastname);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> AddPublicationAsync(string id, string name, int year)
        {

            using (var aura = new Neo4jAura())
            {
                await aura.CreatePublication(id, name, year);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> AddGenreAsync(string id, string name, string place)
        {

            using (var aura = new Neo4jAura())
            {
                await aura.CreateGenre(id, name, place);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> CreateConnectionAsync(string researcherId, string publicationId)
        {

            using (var aura = new Neo4jAura())
            {
                await aura.CreateConnection(researcherId, publicationId);
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}