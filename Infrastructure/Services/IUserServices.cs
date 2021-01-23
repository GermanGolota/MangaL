using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserServices
    {
        Task RegisterUser(UserRegistrationModel user);
    }
}