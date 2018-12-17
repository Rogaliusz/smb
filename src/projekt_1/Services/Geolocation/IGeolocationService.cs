using System;
using System.Threading.Tasks;

namespace projekt_1.Services.Geolocation
{
    public interface IGeolocationService : IService
    {
        Task StartListeningGeolocationAsync();
        Task StopListeningGeolocationAsync();
        Task<IGeopoint> GetCurrentGeolocationAsync();
    }
}
