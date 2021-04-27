using System;

namespace Skrabbl.DataAccess.Test
{
    internal class SeedAttribute : Attribute
    {
        public int Order { get; }

        public SeedAttribute(int order)
        {
            Order = order;
        }
    }
}