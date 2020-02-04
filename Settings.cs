using System.Collections.Generic;

namespace Fic.XTB.PowerBiEmbedder
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings {
        public Settings() {
            OrgSettings = new List<OrgSettings>();
        }
        public string LastUsedOrganizationWebappUrl { get; set; }
        public string CurrentOrg { get; set; }
        public List<OrgSettings> OrgSettings { get; set; }
    }

    public class OrgSettings {
        public string Organization { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string RedirectUrl { get; set; }
    }
}