using Model;
using Xunit;

namespace ModelTests
{
	public class CheckTests
    {
        [Fact]
        public void Status_Get_Ok()
        {
            Assert.Equal("Ok...", Check.Status);
        }
    }
}
