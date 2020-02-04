using Fic.XTB.PowerBiEmbedder.Model;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    class ReportProxy
    {
        public string Text { get; set; }
        public PbiReport Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
