using System;
using System.Collections.Generic;
using System.Text;

namespace BreakingBad.Model
{
    //Represents deaths (circumstances)
    class DeathBB
    {
        public int Death_id { get; set; }

        public string Death { get; set; }

        public string Cause { get; set; }

        public string Responsible { get; set; }

        public string Last_Words { get; set; }

        public string Series { get; set; }

        public int Season { get; set; }

        public int Episode { get; set; }

        public int Number_Of_Deaths { get; set; }

    }
}
