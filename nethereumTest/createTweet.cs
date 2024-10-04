using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nethereumTest
{
    [Function("createTweet")]
    public class createTweet : FunctionMessage
    {
        [Parameter("string", "_msg", 1)]
        public string msg {  get; set; }
    }
}
