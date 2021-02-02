using Core.Entities;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserRepo
    {
        Task SaveUserAsync(User user);
    }
}
