using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.People
{
    public class PeopleIndexViewModel
    {
        public List<PersonModel> People { get; set; }
        public PersonModel MaxPerson { get; set; }
        public PersonModel MinPerson { get; set; }
    }
}
