using DocuWare.Platform.ServerClient;

namespace ValidationServiceDotNetCoreSample.Interfaces
{
    /// <summary>
    /// This service is handling the connection to the DocuWare System
    /// </summary>
    public interface IDocuWareConnectionService
    {
        /// <summary>
        /// Connects and returns the service connection
        /// </summary>
        /// <returns>The DocuWare Service Connection</returns>
        public ServiceConnection GetServiceConnection();
        /// <summary>
        /// Connects and returns the service connection by using username and password
        /// </summary>
        /// <returns>The DocuWare Service Connection</returns>
        public ServiceConnection CreateServiceConnectionUsernamePassword();
    }
}