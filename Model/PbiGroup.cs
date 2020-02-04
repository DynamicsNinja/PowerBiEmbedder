using System.Collections.Generic;

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
