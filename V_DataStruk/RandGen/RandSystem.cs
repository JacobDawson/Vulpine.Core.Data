using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.RandGen
{
    public class RandSystem : VRandom
    {
        //stores our internal PRNG
        private Random rng;

        //stores our inital seed value
        private int seed;

        public RandSystem()
        {
            seed = (int)(DateTime.Now.Ticks % Int32.MaxValue);
            rng = new Random(seed);
        }

        public RandSystem(int seed)
        {
            this.seed = seed;
            this.rng = new Random(seed);
        }


        public override int Seed
        {
            get { return seed; }
        }

        public override double NextDouble()
        {
            return rng.NextDouble();
        }

        public override int NextInt()
        {
            return rng.Next();
        }
    }
}
