using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nethereumTest
{
    [FunctionOutput]
    public class tweet
    {
        [Parameter("uint", "id", 1)]
        public int id { get; set; }

        [Parameter("address", "author", 2)]
        public string author { get; set; }

        [Parameter("string", "content", 3)]
        public string content { get; set; }

        [Parameter("uint256", "timestamp", 4)]
        public int timestamp { get; set; }

        [Parameter("uint", "likes", 5)]
        public int likes { get; set; }
    }
}