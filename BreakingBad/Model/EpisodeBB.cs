using System;
using System.Collections.Generic;
using System.Text;

namespace BreakingBad.Model
{
    //Provides details for episodes.
    class EpisodeBB
    {
        public int Episode_id { get; set; }

        public string Title { get; set; }

        public int Season { get; set; }

        public int Episode { get; set; }

        public string Air_date { get; set; }

        public string Series { get; set; }

        public List<string> Characters { get; set; }

    }
}
