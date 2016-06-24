using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Vulpine.Core.Data;

namespace PrimeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenPrimes1(1000);
            //GenPrimes2(1000000);

            CheckPrimes1(1000000, 257);

            //MakeFile_Base3();
            //MakeFile_Base10();


            Console.ReadLine();
        }

        public static void GenPrimes1(int max)
        {
            int x = 2;

            DateTime start = DateTime.Now;

            for (int i = 0; i < max; i++)
            {
                x = Prime.NextPrime(x);
                Console.WriteLine(x);
            }

            TimeSpan time = DateTime.Now - start;
            Console.WriteLine();
            Console.WriteLine("Generated in {0} seconds. ", time.TotalSeconds);

        }

        public static void GenPrimes2(int max)
        {
            int x = 2;

            DateTime start = DateTime.Now;

            for (int i = 0; i < max; i++)
            {
                x = Prime.NextPrime(x);
                if (i % 10000 == 0) Console.WriteLine("{0} primes generated. ", i);

            }

            TimeSpan time = DateTime.Now - start;
            Console.WriteLine();
            Console.WriteLine("Generated in {0} seconds. ", time.TotalSeconds);
        }

        public static void CheckPrimes1(int max, int b)
        {
            int[] counts = new int[b];
            for (int i = 0; i < b; i++) counts[i] = 0;

            int x = 2;

            for (int i = 1; i <= max; i++)
            {
                x = Prime.NextPrime(x);
                counts[x % b] += 1;

                if (i % 65536 == 0) Console.WriteLine("{0} primes generated. ", i);
            }

            Console.WriteLine();

            for (int i = 0; i < b; i++)
            {
                double percent = ((double)counts[i] / max) * 100.0;
                Console.WriteLine("{0} == {1:00.000}% ({2})", i, percent, counts[i]); 
            }
        }

        public static void MakeFile_Base3()
        {
            int max = 83886080;
            string file = "primes3.rng";

            //Console.WriteLine("Output File Name:");
            //string file = Console.ReadLine();

            Stream output = File.Create(file, 16384);
            BinaryWriter bw = new BinaryWriter(output);

            int x = 2;
            byte b = 0;

            for (int i = 1; i <= max; i++)
            {
                x = Prime.NextPrime(x);
                b <<= 1;

                if (x % 3 == 1) b |= 0x0;
                if (x % 3 == 2) b |= 0x1;

                if (i % 8 == 0)
                {
                    //write out the byte and reset
                    bw.Write(b);
                    b = 0;
                }

                if (i % 65536 == 0)
                {
                    double per = ((double)i / max) * 100.0;
                    Console.WriteLine("{0} primes generated. (x = {1}) [{2:00.0}%]", i, x, per);
                    bw.Flush();
                }
            }

            bw.Close();
            bw.Dispose();

            Console.WriteLine("Done!");
        }


        public static void MakeFile_Base10()
        {
            int max = 41943040;
            string file = "primes10.rng";

            //Console.WriteLine("Output File Name:");
            //string file = Console.ReadLine();

            Stream output = File.Create(file, 16384);
            BinaryWriter bw = new BinaryWriter(output);

            int x = 2;
            byte b = 0;

            for (int i = 1; i <= max; i++)
            {
                x = Prime.NextPrime(x);
                b <<= 2;

                if (x % 10 == 1) b |= 0x0; //00
                if (x % 10 == 3) b |= 0x1; //01
                if (x % 10 == 7) b |= 0x2; //10
                if (x % 10 == 9) b |= 0x3; //11

                if (i % 4 == 0)
                {
                    //write out the byte and reset
                    bw.Write(b);
                    b = 0;
                }

                if (i % 32768 == 0)
                {
                    double per = ((double)i / max) * 100.0;
                    Console.WriteLine("{0} primes generated. (x = {1}) [{2:00.0}%]", i, x, per);
                    bw.Flush();
                }
            }

            bw.Close();
            bw.Dispose();

            Console.WriteLine("Done!");
        }
    }
}
