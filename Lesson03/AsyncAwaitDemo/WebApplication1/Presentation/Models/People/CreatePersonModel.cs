using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.People
{
    public class CreatePersonModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }
    }
}
