using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Tests
{
    public class TagTest
    {
        private readonly ITestOutputHelper output;
        public TagTest(ITestOutputHelper output)
        {
            this.output = output;
        }
    }
}
