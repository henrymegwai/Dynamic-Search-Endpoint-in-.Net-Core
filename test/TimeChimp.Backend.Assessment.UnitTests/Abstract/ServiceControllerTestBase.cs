using Xunit;

namespace TimeChimp.Backend.Assessment.UnitTests.Abstract
{

    public abstract class ServiceControllerTestBase<T> : IClassFixture<T> where T : ServiceFixture
    {
        public T ServiceFixture;

        protected ServiceControllerTestBase(T serviceFixture)
        {
            ServiceFixture = serviceFixture;
        }
    }
}
