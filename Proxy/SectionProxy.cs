using Fic.XTB.PowerBiEmbedder.Model;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    class SectionProxy
    {
        public FormTabColumnSection Section { get; set; }
        public string Text { get; set; }
        public object Id { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
