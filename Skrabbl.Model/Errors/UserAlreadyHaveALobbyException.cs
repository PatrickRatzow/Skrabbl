using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model.Errors
{
    public class UserAlreadyHaveALobbyException : Exception
    {
        public UserAlreadyHaveALobbyException()
        {

        }

        public UserAlreadyHaveALobbyException(string message) : base(message)
        {

        }

        public UserAlreadyHaveALobbyException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
