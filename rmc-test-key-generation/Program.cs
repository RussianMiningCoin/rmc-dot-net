using System;
using RMC.Signing;

namespace rmc_test_key_generation
{
    class Program
    {
        static void Main(string[] args)
        {
            var seed = Seed.FromRandom();

            Console.WriteLine("Random seed: {0}", seed.ToString());

            for (var index = 0; index < 10; index++)
            {
                var keyPair = seed.KeyPair(index);
                Console.WriteLine("Key #{0} : ({1}, {2})", index, keyPair.Id(), keyPair.ToString());
            }
        }
    }
}
