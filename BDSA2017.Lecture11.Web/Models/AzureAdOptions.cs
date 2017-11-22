using System.Collections.Generic;

namespace BDSA2017.Lecture11.Web.Models
{
    public class AzureAdOptions
    {
        public string Instance { get; set; }

        public string Domain { get; set; }

        public string TenantId { get; set; }

        public string ClientId { get; set; }

        public string Audience { get; set; }

        public string Authority => $"{Instance}{TenantId}";
    }
}
