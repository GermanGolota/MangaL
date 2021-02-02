using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Commands
{
    public class RegisterUserCommand:IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; private set; }
        public RegisterUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
