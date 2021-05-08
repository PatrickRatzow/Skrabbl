using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model.Errors
{
    public class LobbyIsFullException : Exception
    {
        public LobbyIsFullException()
        {

        }

        public LobbyIsFullException(string message) : base(message)
        {

        }

        public LobbyIsFullException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
