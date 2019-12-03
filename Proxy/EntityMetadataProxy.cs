using Microsoft.Xrm.Sdk.Metadata;

namespace Fic.XTB.PowerBiEmbedder.Proxy
{
    internal class EntityMetadataProxy
    {
        public EntityMetadata Metadata;

        public EntityMetadataProxy(EntityMetadata entityMetadata)
        {
            Metadata = entityMetadata;
        }

        public override string ToString()
        {
            if (Metadata != null)
            {
                if (!string.IsNullOrEmpty(Metadata?.DisplayName?.UserLocalizedLabel?.Label))
                {
                    return $"{Metadata.DisplayName.UserLocalizedLabel.Label} ({Metadata.LogicalName})";
                }
                return Metadata.LogicalName;
            }
            return base.ToString();
        }
    }
}
