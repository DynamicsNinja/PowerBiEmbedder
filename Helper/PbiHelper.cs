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

            var response = _client.GetAsync("https://api.powerbi.com/v1.0/myorg/groups").GetAwaiter().GetResult();
            if(!response.IsSuccessStatusCode) { throw new Exception($"Fetching groups from Power BI resulted as {response.ReasonPhrase}");}

            var jsonResponse =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var pbiGroupsResponse = JsonConvert.DeserializeObject<PbiGroupsResponse>(jsonResponse);

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

        public List<PbiReport> GetReportsFromMyWorkspace() {
            var response = _client.GetAsync("https://api.powerbi.com/v1.0/myorg/reports").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode) { throw new Exception($"Fetching my reports from 'My Workspace' resulted as {response.ReasonPhrase}"); }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var pbiGroupsResponse = JsonConvert.DeserializeObject<PbiReportsResponse>(jsonResponse);

            foreach (var report in pbiGroupsResponse.Reports){
                var pages = GetReportPages(report.Id);
                report.Pages = pages;
            }

            return pbiGroupsResponse.Reports;
        }

        public List<PbiReport> Getreports(string groupId) {
            var response = _client.GetAsync($"https://api.powerbi.com/v1.0/myorg/groups/{groupId}/reports").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode) { throw new Exception($"Fetching reports from group resulted as {response.ReasonPhrase}"); }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (jsonResponse == "") { return null; }

            var reports = JsonConvert.DeserializeObject<PbiReportsResponse>(jsonResponse);

            foreach (var report in reports.Reports)
            {
                var pages = GetReportPages(groupId,report.Id);
                report.Pages = pages;
            }

            return reports.Reports.OrderBy(r => r.Name).ToList();
        }

        public List<PbiPage> GetReportPages(string reportId) {
            var response = _client.GetAsync($"https://api.powerbi.com/v1.0/myorg/reports/{reportId}/pages").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode) { throw new Exception($"Fetching report pages resulted as {response.ReasonPhrase}"); }
            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var pages = JsonConvert.DeserializeObject<PbiPagesResponse>(jsonResponse).Pages;

            return pages;
        }

        public List<PbiPage> GetReportPages(string groupId,string reportId){
            var response = _client.GetAsync($"https://api.powerbi.com/v1.0/myorg/groups/{groupId}/reports/{reportId}/pages").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode) { throw new Exception($"Fetching report pages resulted as {response.ReasonPhrase}"); }
            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var pages = JsonConvert.DeserializeObject<PbiPagesResponse>(jsonResponse).Pages;

            return pages;
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
