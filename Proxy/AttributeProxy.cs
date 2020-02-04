using Microsoft.Xrm.Sdk.Metadata;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    public class AttributeProxy
    {
        internal AttributeMetadata AttributeMetadata;

        public AttributeProxy(AttributeMetadata metadata)
        {
            AttributeMetadata = metadata;
        }

        public string DisplayName { get { return AttributeMetadata.DisplayName?.UserLocalizedLabel?.Label; } }
        public string LogicalName { get { return AttributeMetadata.LogicalName; } }

        public override string ToString()
        {
            return $"{LogicalName}";
        }
    }
}
