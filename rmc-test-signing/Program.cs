using System;

using RMC.TxSigning;
using Newtonsoft.Json.Linq;

namespace rmc_test_signing
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = "35tDMF67PWq8XmqY88CjBPZspfwX2"; // HisDivineShadow
            var unsignedTxJson = @"{
                'Account': '19HVVcvqHvFocvwpdN4nB5AXXgg19qqpw3',
                'Amount': '1000',
                'Destination': '15ipnwHm6VV8TR8bpbkG5zo4FbgT5ZqrFq',
                'Fee': '10',
                'Flags': 2147483648,
                'Sequence': 1,
                'TransactionType' : 'Payment'
            }";

            var signed = TxSigner.SignJson(JObject.Parse(unsignedTxJson), secret);

            Console.WriteLine(signed.Hash);
            Console.WriteLine(signed.TxJson);
            Console.WriteLine(signed.TxBlob);

        }
    }
}
