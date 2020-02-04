using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Fic.XTB.PowerBiEmbedder.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net.Http;


namespace Fic.XTB.PowerBiEmbedder.Helper
{
    public class PbiHelper
    {
        private string _returnUri;
        private string _tenantID;
        private string _clientId;

        private HttpClient _client;

        public PbiHelper(string appId, string tenantId, string redirectUrl) {
            _clientId = appId;
            _tenantID = tenantId;
            _returnUri = redirectUrl;

            GetClient();
        }

        public List<PbiGroup> GetGroups()
        {
            var groups = new List<PbiGroup>();

            dynamic response = _client.GetAsync("https://api.powerbi.com/v1.0/myorg/groups").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var pbiGroupsResponse = (PbiGroupsResponse)JsonConvert.DeserializeObject<PbiGroupsResponse>(response);

            groups.Add(new PbiGroup
            {
                Name = "My Workspace",
                Id = Guid.Empty.ToString("D"),
                Reports = GetReportsFromMyWorkspace()
            });

            foreach (var group in pbiGroupsResponse.Groups)
            {
                var reports = Getreports(group.Id);

                if (reports == null) { continue; }
                group.Reports.AddRange(reports);
            }

            groups.AddRange(pbiGroupsResponse.Groups.OrderBy(g => g.Name));

            return groups.Where(g=>g.Reports.Count > 0).ToList();
        }

        public List<PbiReport> GetReportsFromMyWorkspace()
        {
            dynamic response = _client.GetAsync("https://api.powerbi.com/v1.0/myorg/reports").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var pbiGroupsResponse = (PbiReportsResponse)JsonConvert.DeserializeObject<PbiReportsResponse>(response);

            return pbiGroupsResponse.Reports;
        }

        public List<PbiReport> Getreports(string groupId)
        {
            dynamic response = _client.GetAsync($"https://api.powerbi.com/v1.0/myorg/groups/{groupId}/reports").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response == "") { return null; }

            var reports = (PbiReportsResponse)JsonConvert.DeserializeObject<PbiReportsResponse>(response);

            return reports.Reports.OrderBy(r => r.Name).ToList();
        }

        public void GetClient()
        {
            var token = GetInteractiveClientToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("accept", "application/json");
            _client =  client;
        }

        private string GetInteractiveClientToken()
        {
            AuthenticationContext ac = new AuthenticationContext($"https://login.microsoftonline.com/{_tenantID}");
            try
            {
                return ac.AcquireTokenSilentAsync("https://analysis.windows.net/powerbi/api", _clientId).GetAwaiter().GetResult().AccessToken;
            }
            catch (AdalException adalException)
            {
                if (adalException.ErrorCode == AdalError.FailedToAcquireTokenSilently
                    || adalException.ErrorCode == AdalError.InteractionRequired)
                {
                    return ac.AcquireTokenAsync("https://analysis.windows.net/powerbi/api", _clientId, new Uri(_returnUri),
                        new PlatformParameters(PromptBehavior.Auto)).GetAwaiter().GetResult().AccessToken;
                }
            }

            return null;
        }
    }
}
