using Neo4j.Driver;
using proje3.GarphDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace proje3.Models
{
    public class Publication
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<Researcher> Researchers { get; set; }

        public Publication(string id, string name, int year)
        {
            Id = id;
            Name = name;
            Year = year;

            Researchers = new List<Researcher>();
            FindResearchers(Id);
            
        }

        public async Task FindResearchers (string id)
        {
            using (var aura = new Neo4jAura())
            {
                List<IRecord> researchers = await aura.FindResearcherWithPublication(id);
                foreach (var result in researchers)
                {
                    Researchers.Add(new Researcher(result["a"].As<INode>().Properties["ArastirmaciID"].ToString(), result["a"].As<INode>().Properties["ArastirmaciAdi"].ToString(), result["a"].As<INode>().Properties["ArastirmaciSoyadi"].ToString()));
                }
            }
        }
    }
}