

namespace Infrastructure.Configuration
{
    public interface ISQLClientFactory
    {
        ISQLClient CreateClient();
    }
}