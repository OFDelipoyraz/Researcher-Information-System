using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje3.Models
{
    public class Researcher
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Researcher(string id, string firstname, string lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}