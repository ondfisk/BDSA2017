using System;

namespace BDSA2017.Lecture11.App.Models
{
    public interface ISettings
    {
        Uri ApiBaseAddress { get; }
        string ApiResource { get; }
        string Authority { get; }
        string ClientId { get; }
        string Instance { get; }
        string RedirectUri { get; }
        string Tenant { get; }
        string WebAccountProviderId { get; }
    }
}