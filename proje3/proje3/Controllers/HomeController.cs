using Neo4j.Driver;
using proje3.GarphDatabase;
using proje3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace proje3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SearchAsync(string text)
        {
            List<Researcher> researchers = new List<Researcher>();
            List<Publication> publications = new List<Publication>();
           
            using (var aura = new Neo4jAura())
            {
                List<IRecord> res = await aura.FindResearcherWithName(text);
                foreach (var result in res)
                {
                    researchers.Add(new Researcher(result["a"].As<INode>().Properties["ArastirmaciID"].ToString(), result["a"].As<INode>().Properties["ArastirmaciAdi"].ToString(), result["a"].As<INode>().Properties["ArastirmaciSoyadi"].ToString()));
                }
            }

            using (var aura = new Neo4jAura())
            {
                List<IRecord> res = await aura.FindResearcherWithLastname(text);
                foreach (var result in res)
                {
                    researchers.Add(new Researcher(result["a"].As<INode>().Properties["ArastirmaciID"].ToString(), result["a"].As<INode>().Properties["ArastirmaciAdi"].ToString(), result["a"].As<INode>().Properties["ArastirmaciSoyadi"].ToString()));
                }
            }

            using (var aura = new Neo4jAura())
            {
                List<IRecord> pubs = await aura.FindPublicationsWithFirstname(text);
                foreach (var result in pubs)
                {
                    publications.Add(new Publication(result["y"].As<INode>().Properties["YayinID"].ToString(), result["y"].As<INode>().Properties["YayinAdi"].ToString(), Convert.ToInt32(result["y"].As<INode>().Properties["YayinYili"])));
                }
            }

            using (var aura = new Neo4jAura())
            {
                List<IRecord> pubs = await aura.FindPublicationsWithLastname(text);
                foreach (var result in pubs)
                {
                    publications.Add(new Publication(result["y"].As<INode>().Properties["YayinID"].ToString(), result["y"].As<INode>().Properties["YayinAdi"].ToString(), Convert.ToInt32(result["y"].As<INode>().Properties["YayinYili"])));
                }
            }

            using (var aura = new Neo4jAura())
            {
                List<IRecord> pubs = await aura.FindPublicationsWithName(text);
                foreach (var result in pubs)
                {
                    publications.Add(new Publication(result["y"].As<INode>().Properties["YayinID"].ToString(), result["y"].As<INode>().Properties["YayinAdi"].ToString(), Convert.ToInt32(result["y"].As<INode>().Properties["YayinYili"])));
                }
            }

            try
            {
                using (var aura = new Neo4jAura())
                {
                    List<IRecord> pubs = await aura.FindPublicationsWithYear(Convert.ToInt32(text));
                    foreach (var result in pubs)
                    {
                        publications.Add(new Publication(result["y"].As<INode>().Properties["YayinID"].ToString(), result["y"].As<INode>().Properties["YayinAdi"].ToString(), Convert.ToInt32(result["y"].As<INode>().Properties["YayinYili"])));
                    }
                }
            } 
            catch
            {

            }
            
            ViewData["researchers"] = researchers;
            ViewData["publications"] = publications;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Researcher(string id)
        {
            Researcher researcher = null;
            using (var aura = new Neo4jAura())
            {
                List<IRecord> res = await aura.FindResearcherWithId(id);
                foreach (var result in res)
                {
                    researcher = new Researcher(result["a"].As<INode>().Properties["ArastirmaciID"].ToString(), result["a"].As<INode>().Properties["ArastirmaciAdi"].ToString(), result["a"].As<INode>().Properties["ArastirmaciSoyadi"].ToString());
                }
            }

            List<Publication> publics = new List<Publication>();
            using (var aura = new Neo4jAura())
            {
                List<IRecord> pubs = await aura.FindPublicationsWithResId(id);
                foreach (var result in pubs)
                {
                    publics.Add(new Publication(result["y"].As<INode>().Properties["YayinID"].ToString(), result["y"].As<INode>().Properties["YayinAdi"].ToString(), Convert.ToInt32(result["y"].As<INode>().Properties["YayinYili"])));
                }
            }

            ViewData["publics"] = publics;
            ViewData["researcher"] = researcher;

            return View();
        }
    }
}