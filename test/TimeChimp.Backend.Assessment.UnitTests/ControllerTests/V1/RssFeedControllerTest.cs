using TimeChimp.Backend.Assessment.Controllers.V1;
using TimeChimp.Backend.Assessment.UnitTests.Abstract;
using TimeChimp.Backend.Assessment.UnitTests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Stashbox.Mocking.Moq;
using Xunit;
using TimeChimp.Core.Utlilities;
using Moq;
using TimeChimp.Core.Interfaces.IManagers;
using TimeChimp.Core.Models;

namespace TimeChimp.Backend.Assessment.UnitTests.ControllerTests.V1
{
    public class RssFeedControllerFixture : ServiceFixture
    {
    }

    public class RssFeedControllerTest : ServiceControllerTestBase<RssFeedControllerFixture>
    {
        public RssFeedControllerTest(RssFeedControllerFixture serviceFixture)
            : base(serviceFixture) { }

        #region Property  
        public Mock<IRssFeedManager> mock = new Mock<IRssFeedManager>();
        #endregion

        [Fact]
        public void Search()
        {
            //using var stash = StashMoq.Create();
            //var controller = stash.GetWithParamOverrides<RssFeedController>().BypassStub(stash);

            // Arrange
            string searchquery = "";
            string sortBy = "title";
            string currentFilter = "All";
            int pageNumber = 1;
            int pageSize = 20;
            var listofRssFeeds = GetRssFeedItemsList.GetList();
            var returenedResult = PaginatedList<RssFeedItem>.CreateAsync(listofRssFeeds, pageNumber, pageSize);
            

            mock.Setup(p => p.SearchRssFeeds(searchquery, currentFilter, sortBy, pageNumber, pageSize)).Returns(returenedResult);
            RssFeedController emp = new RssFeedController(mock.Object);


            
            //Call the tested method
            var result = emp.Search(searchquery, sortBy, pageNumber, pageSize);

            //Check the result
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            //assert that result is 200 ok staus
            Assert.True(okResult.StatusCode == 200);
            //Assert that result value matches mocked result.
            Assert.True(okResult.Value.Equals(returenedResult));
        }
    }
}
