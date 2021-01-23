using DataAccess;

namespace MangaLWebAPI.Configuration
{
    public interface ISQLClientFactory
    {
        ISQLClient CreateClient();
    }
}