using Fic.XTB.PowerBiEmbedder.Model;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    public class PageProxy
    {
        public string Text { get; set; }
        public PbiPage Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
