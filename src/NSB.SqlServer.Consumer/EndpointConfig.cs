
namespace NSB.SqlServer.Consumer
{
    using NServiceBus;

    /// <summary>
    /// Configuring Endpoint as a server: so it will be setup as a transactional endnpoit using impersonation and not purging messages on startup
    /// </summary>
    public class EndpointConfig
        : IConfigureThisEndpoint, AsA_Server, UsingTransport<SqlServer>
    { }
}
