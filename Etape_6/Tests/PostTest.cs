using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Tests
{
    public class PostTest
    {
        private readonly ITestOutputHelper output;
        public PostTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void getPostsTest ()
        {

        }
    }
}
