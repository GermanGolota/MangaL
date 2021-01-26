using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.Commands;
using Infrastructure.Hashing;
using MediatR;

namespace Infrastructure.Handlers
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
