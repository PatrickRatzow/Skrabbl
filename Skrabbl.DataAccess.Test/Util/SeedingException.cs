using System;

namespace Skrabbl.DataAccess.Test
{
    public class SeedingException : Exception
    {
        public SeedingException()
        {
        }

        public SeedingException(string message) : base(message)
        {
        }

        public SeedingException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}