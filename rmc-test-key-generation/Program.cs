using System;
using RMC.Signing;

namespace rmc_test_key_generation
{
    class Program
    {
        static void Main(string[] args)
        {
            var seed = Seed.FromRandom();
            var rootKeyPair = seed.RootKeyPair();

            Console.WriteLine("Random seed: {0}", seed.ToString());
            Console.WriteLine("Root key : ({0}, {1})", rootKeyPair.Id(), rootKeyPair.ToString());

            for (var index = 0; index < 10; index++)
            {
                var keyPair = seed.KeyPair(index);
                Console.WriteLine("Key #{0} : ({1}, {2})", index, keyPair.Id(), keyPair.ToString());
            }

            var seed2 = Seed.FromPassPhrase("HisDivineShadow");
            var rootKeyPair2 = seed2.RootKeyPair();

            Console.WriteLine("Derived seed: {0}", seed2.ToString());
            Console.WriteLine("Root key : ({0}, {1})", rootKeyPair2.Id(), rootKeyPair2.ToString());

            for (var index = 0; index < 10; index++)
            {
                var keyPair = seed2.KeyPair(index);
                Console.WriteLine("Key #{0} : ({1}, {2})", index, keyPair.Id(), keyPair.ToString());
            }
        }
    }
}
