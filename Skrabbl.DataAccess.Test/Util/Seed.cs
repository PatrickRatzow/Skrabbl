using System;

namespace Skrabbl.DataAccess.Test
{
    internal class Seed : Attribute
    {
        public int Order { get; }

        public Seed(int order)
        {
            Order = order;
        }
    }
}