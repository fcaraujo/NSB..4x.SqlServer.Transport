
namespace NSB.SqlServer.Producer
{
    using NServiceBus;

    /// <summary>
    /// Configuring endpoint as a publisher: it's compatible with AsA_Server, it will be setup as a transactional endnpoit using impersonation and not purging messages on startup
    /// </summary>
    public class EndpointConfig
        : IConfigureThisEndpoint, AsA_Publisher, UsingTransport<SqlServer>
    { }
}
