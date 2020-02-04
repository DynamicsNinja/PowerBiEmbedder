using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class PbiGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<PbiReport> Reports { get; set; }

        public PbiGroup()
        {
            Reports = new List<PbiReport>();
        }
    }
}
