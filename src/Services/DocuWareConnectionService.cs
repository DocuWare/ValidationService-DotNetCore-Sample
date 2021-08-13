using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using DocuWare.Platform.ServerClient;
using Microsoft.Extensions.Configuration;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Services
{
    public class DocuWareConnectionService : IDocuWareConnectionService, IDisposable
    {
        private ServiceConnection _serviceConnection;
        private readonly DwConnectionInformationModel _dwConnectionInformation;
        private readonly DwHttpClientConfigurationModel _dwHttpClientConfiguration;
        private HttpClientHandler _httpClientHandler;

        public ServiceConnection GetServiceConnection()
        {
            // Reconnect http client when cookies expired
            if (_httpClientHandler.CookieContainer.GetCookies(new Uri(_dwConnectionInformation.Address)).Any(c => c.Expires < DateTime.Now))
            {
                _serviceConnection = CreateServiceConnectionUsernamePassword();
            }

            return _serviceConnection;
        }

        public ServiceConnection CreateServiceConnectionUsernamePassword()
        {
            _httpClientHandler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true,
            };

            foreach (DwCookieModel cookie in _dwHttpClientConfiguration.Cookies)
            {
                _httpClientHandler.CookieContainer.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
            }

            return ServiceConnection.Create(new Uri(_dwConnectionInformation.Address),
                _dwConnectionInformation.Username,
                _dwConnectionInformation.Password,
                httpClientHandler: _httpClientHandler);
        }

        public DocuWareConnectionService(IConfiguration configuration)
        {
            _dwConnectionInformation = GetAppSettingsByModel<DwConnectionInformationModel>(configuration, DwConnectionInformationModel.Position);
            _dwHttpClientConfiguration = GetAppSettingsByModel<DwHttpClientConfigurationModel>(configuration, DwHttpClientConfigurationModel.Position);

            _serviceConnection = CreateServiceConnectionUsernamePassword();
        }

        private T GetAppSettingsByModel<T>(IConfiguration configuration, string position) where T : new()
        {
            return configuration.GetSection(position).Get<T>();
        }

        public void Dispose()
        {
            Console.WriteLine(string.Format("{0}.Dispose() - Disconnect: {1}",
                nameof(ServiceConnection),
                _serviceConnection?.Organizations.FirstOrDefault()
                    ?.Name));

            _serviceConnection?.DisconnectAsync();
        }
    }
}
