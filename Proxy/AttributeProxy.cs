using Microsoft.Xrm.Sdk.Metadata;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    public class AttributeProxy
    {
        internal AttributeMetadata attributeMetadata;

        public AttributeProxy(AttributeMetadata metadata)
        {
            attributeMetadata = metadata;
        }

        public string DisplayName { get { return attributeMetadata.DisplayName?.UserLocalizedLabel?.Label; } }
        public string LogicalName { get { return attributeMetadata.LogicalName; } }

        public override string ToString()
        {
            return $"{LogicalName}";
        }
    }
}
