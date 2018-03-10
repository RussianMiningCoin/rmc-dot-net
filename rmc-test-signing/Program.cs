using System;

using RMC.TxSigning;
using RMC.Signing;
using Newtonsoft.Json.Linq;

namespace rmc_test_signing
{
    class Program
    {
        static void Main(string[] args)
        {
            var unsignedTxJson = @"{
                'Account': '19HVVcvqHvFocvwpdN4nB5AXXgg19qqpw3',
                'Amount': '1000',
                'Destination': '15ipnwHm6VV8TR8bpbkG5zo4FbgT5ZqrFq',
                'Fee': '10',
                'Flags': 2147483648,
                'Sequence': 1,
                'TransactionType' : 'Payment'
            }";

            // Sign with secret seed
            var secret = "35tDMF67PWq8XmqY88CjBPZspfwX2"; // HisDivineShadow
            var signed1 = TxSigner.SignJson(JObject.Parse(unsignedTxJson), secret);

            Console.WriteLine(signed1.Hash);
            Console.WriteLine(signed1.TxJson);
            Console.WriteLine(signed1.TxBlob);

            // Sign with keypair
            var keyPair = new KeyPair("KzTZRtFzsus7RmRtVyspFtZLgM6VPgdZ6rSwWH2dHBEFrrLPpbaD");
            var signed2 = TxSigner.SignJson(JObject.Parse(unsignedTxJson), keyPair);

            Console.WriteLine(signed2.Hash);
            Console.WriteLine(signed2.TxJson);
            Console.WriteLine(signed2.TxBlob);
        }
    }
}
