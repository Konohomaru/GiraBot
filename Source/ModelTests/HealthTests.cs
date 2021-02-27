using Model;
using Xunit;

namespace ModelTests
{
	public class HealthTests
    {
        [Fact]
        public void Status_Get_Ok()
        {
            Assert.Equal("Ok", Health.Status);
        }
    }
}
