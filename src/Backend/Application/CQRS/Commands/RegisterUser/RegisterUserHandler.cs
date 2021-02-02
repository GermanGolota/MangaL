using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepo _repo;
        private readonly IHasher _hasher;

        public RegisterUserHandler( IUserRepo repo, IHasher hasher)
        {
            this._repo = repo;
            this._hasher = hasher;
        }
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            string id = Guid.NewGuid().ToString();
            User user = new User
            {
                PasswordHash = await _hasher.Hash(request.Password),
                Username = request.Username,
                Id = id
            };

            await _repo.SaveUserAsync(user);

            return id;
        }
    }
}
