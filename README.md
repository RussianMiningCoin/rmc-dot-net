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
Random seed: 33DaCjeQEqgz2Jm6gghTmX9q96nUt
Root key : (1J5uiC62HH4hg3dr5PAbSAiwuLpjrK8bDe, KysDFbhTfQiXJodG7JTFRj4qpDDSYi3jL6CsqrVaTkhdRa7H2EAa)
Key #0 : (18aefeyQF3kXPzfLkSyJC7eCjuzEEUKvPx, L31Fqy4yj8vBHnHuUqd1ozWA13zURbby6QL9SQWBxbp8ZdH8R4Tv)
Key #1 : (18NR3WNhijaVEXBJaXtyhfjdZfzUVFEBnw, L5NpPsB2QPuVUrn3pkJGboGDWD5RTVaG73xumSKDFYG4hyrfKwtm)
Key #2 : (1Kxmqz9muFb8jVjQPpSYomYUrJXvyLqUP9, L3kEoZVX8cLTeK9XmgMihfFvbU7XAjyFRFq7X7V5HhsY8CMS7GmT)
Key #3 : (1GVJaE94AZr4WJ8KcseiNycjsv6FYdQWoy, KyWqadwpbxLodjzMw55kMfXmn68tcmHzufhqQFB38VbKTdkB7f8X)
Key #4 : (15Uj4dXeicqcj7VccysN396Fq8JujpKaP8, Kx7nPts9sdJVYQLB8ciYSZPYE2U4jGj9T6vWPU5cfyjRWg9yqbei)
Key #5 : (1LrudRaGea5bEAQnxajJb4tQfnF2bDv9M3, KzQatBXjoyrHrxfUcePFaGN71jivmaYJSp38XgEHhFGap1fBYNGw)
Key #6 : (14NcVjEZDKyHferh9SbXjamqUwyzUrh2Ls, L1ovdgpbqKeouNeSMiFNqZKX5uDM3VPuCm7FRXA4k65F3PcWxwfE)
Key #7 : (1JmEFYsFS76jyXNpANWzyBTPWWFFZtVi6c, KwKL4hD77q8V6S1yacUMcQT2eNpjRvHPvKNr9ut8iNi3odzyFQLH)
Key #8 : (12iToVB9bMn2cjHnZPdCBUujgmjtVvZCzt, KxQH1iQdESNHkrq3WxDXrsnfd5Lcz7uGhwNLPRbidk6sUXg4BC5T)
Key #9 : (1PXBCv2vYi7mpTT2GqAigVXyDw2FDFBMjX, KxpG5KbHFJaM19NQAvVW51FFJBSXmP2eFtjLnLYZpxkbCnqJZ4SN)
Derived seed: 35tDMF67PWq8XmqY88CjBPZspfwX2
Root key : (19HVVcvqHvFocvwpdN4nB5AXXgg19qqpw3, KzTZRtFzsus7RmRtVyspFtZLgM6VPgdZ6rSwWH2dHBEFrrLPpbaD)
Key #0 : (1Da1f7YnkpDBXLUk63B4dP2NQKik8fMHDZ, KwYU7jb4PxEhyNYAiNrPQZGHHE35Z4g5F2ncpG8eYqL24MUHFcJx)
Key #1 : (18Ew6xrwtnXw2bHPCksTWeE24wjxoprLoF, L1eFj1BoCZxVJRgJMJ1vyHrpGPznN5kuVoDMGJzgeFtCW7iAZHdz)
Key #2 : (15Nr7JmY2JRVezsZffyL7YPVJspzCchYyD, L2pAES9wjXG7jjQE6QTfMH89zdvtbNLb2YaPr74H9uWZRZzA5ZfF)
Key #3 : (1CcZhx4FYwWRVjUQvcitPYBcWCss8dvQhD, Ky6PWeAdZuo2crWQnzeCJzMzUhYk9cFAFbpeqWic66gv82dDtRXs)
Key #4 : (1Q3T3NxMPwaMvh5U979JBGgFfcEtzjRaEE, Kywp6nRRTuw26sHTBwUJb2KMyNQbZ8QnEJyagqV3XsTth8XEYsjz)
Key #5 : (1JnsMj3oFY59pyo8nzTg5HZzuT3aSvJzvt, L15CD1ATiGEGBhaVHiZzyHDcmkpX2oayC2msX8cnoWz8CFF43c79)
Key #6 : (1LmieUWojZK3JFKiGsXJKyhXSJ7kbJapRB, L48nVJnpHaBspgRZ7EujpEFrXpssaLT1HDB7GMq63pHJLRKgJzAp)
Key #7 : (12Hu2a7YC87Ub7s8jvmNcZ4xEuqWqciNm1, KzPQDSkEXDqkgDsv2pMJPtVBzChyxHpjbUTNpkiXM74Rw4SXzBVc)
Key #8 : (1BNxk7LrqfHAYnScKTdqftmQgkBjSnp5nJ, L2bMxzKtoNmgkH4ocHxzGKSQknJdh576MXtJF53JBoEoBgkw4ry9)
Key #9 : (1KtwuNS89HEB2k8oMBpZGUAcABu9rTNBQd, L5KuKKapfq2JMAfciRfqyyLRdBJ64uoiYjzxG5Y9U9h3rBNHRLXu)
```
