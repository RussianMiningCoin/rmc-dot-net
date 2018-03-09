# RMC .NET library

The purpose of this code is primarily for signing transactions, and while the code can do a bit more than what is shown here, at this stage, it may change without warning.

## Transaction signing

Signing is done with either ecdsa/rfc6979 or ed25519. See [ripple-keypairs](https://github.com/ripple/ripple-keypairs) for how to generate a seed/secret, encoded in base58. 

```c#
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
```

Sample program output:

```
AF8B613F2AD03B51985906B9DA478592E2D9FA02D59CBC42B6CBBC2D3405DAA7
{
  "TransactionType": "Payment",
  "Flags": 2147483648,
  "Sequence": 1,
  "Amount": "1000",
  "Fee": "10",
  "SigningPubKey": "0303DD2483F6AB9D60061BC21DEFDD8A8E7DC2FA38FBFB8C42517AB4970E2470F6",
  "TxnSignature": "3045022100B206BF40F7889B10E7636FD4BA18882CBDFF3375469B5AC62BFC1B38BE21243A022060C47E35ED4B7A233D1F3156EB49E53F516D3FC72F0B1D61186380520E5D01BE",
  "Account": "19HVVcvqHvFocvwpdN4nB5AXXgg19qqpw3",
  "Destination": "15ipnwHm6VV8TR8bpbkG5zo4FbgT5ZqrFq"
}
120000228000000024000000016140000000000003E868400000000000000A73210303DD2483F6AB9D60061BC21DEFDD8A8E7DC2FA38FBFB8C42517AB4970E2470F674473045022100B206BF40F7889B10E7636FD4BA18882CBDFF3375469B5AC62BFC1B38BE21243A022060C47E35ED4B7A233D1F3156EB49E53F516D3FC72F0B1D61186380520E5D01BE81145ADF72606B9167952F3D2B801DA4CB6F920AAD2A831433C973CB52E70070222D0849F409273C3062B96D
```

