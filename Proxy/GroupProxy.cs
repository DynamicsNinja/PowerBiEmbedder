using Fic.XTB.PowerBiEmbedder.Model;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    public class GroupProxy
    {
        public string Text { get; set; }
        public PbiGroup Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
