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

## Key generation

```c#
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
```

Sample program output:

```
Random seed: 34Pm89637ZZnx9tiZJyh8Bdpo9L4u
Key #0 : (1MaQGd5kPDxdHFdks2kZ1MASBGxdCtKHxt, Kz4Y1VtNcVLk4zKhJCszUaQN2kaLfcYurXavuLpsJScHyjd8waY7)
Key #1 : (1NjkSVtDR8Ck18cqSE75SGP8QKGXkCn2Vb, L1hjC8XttieQioyqohF8YxU8ovaUUHEdGrMiLbRFV8cC3YTZ5BEQ)
Key #2 : (1EphUXTzEhDZQQ74fEftz97HE5p2itMZXV, L51rgZWzi4Mcy5g6s3VZnEFnBELV4kJdSndT3jsMjgWEx4xG1mrc)
Key #3 : (1N8zUro3QQA8uN5GfEe34fn3RnEat19inr, L453BxZ3gFMD7JGLDrxEGkWTatQHkYoTkiAu8QMAnZqwQLuHnKru)
Key #4 : (18ztrdy2p1x6AKVG4etxQvw9Mtvnj4dpgY, KyJwhg3cSjpAYpcuoetjkapRL6bVsfgdGb2aCtcdA3BGMNhu9KGJ)
Key #5 : (1CW15wzX79Pzry6ydF7FE6LRUVbbdTNXo3, Kym9mpu4YUpBQzr1r3UjZa6gXKxoJDfUZAtHJ6g5VJcoUvjkLZ1G)
Key #6 : (16DjtL5Mgyh1w4LnK3XeCXF4qXYyUmKFVd, L1aJQ8pSTRetv3TUcxdHvF1tnaM6GL2vKewt41w7Ykk1x8ktSyj5)
Key #7 : (13C3tXw8kekCFYF3Ju4USTq9Hs2Cav28kv, L2STZkYATxmKadsw3vkqaJonKyHjEWqPuZDi26Ht3DfWi8jWutQk)
Key #8 : (1PYtZrLYcan6McVqBPu76QjevUM2zB9LzP, L3yR9VLmqmCAM3kktjj2KbMzW3YHH1kdJzZ1eox38eMF3DopH4Fd)
Key #9 : (1PNABGMnNmXT5Ss3GBtmmwxxabbgG9o9WD, Kye9szX34n2fRNkF2JPzUegSP73d8sajQdGpEGGRKV41ntKU3HR7)
```
