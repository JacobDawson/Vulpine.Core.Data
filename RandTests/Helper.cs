using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.RandGen;

namespace RandTests
{
    public static class Helper
    {
        public static int Get32bits(this VRandom rng)
        {
            int x1 = rng.NextInt();
            int x2 = rng.NextInt();

            return (x1 << 1) | (x2 & 1);
        }
    }
}
