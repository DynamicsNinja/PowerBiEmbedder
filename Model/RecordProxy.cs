namespace Fic.XTB.PowerBiEmbedder.Model
{
    public class RecordProxy
    {
        public string DisplayName { get; set; }
        public string Field { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
