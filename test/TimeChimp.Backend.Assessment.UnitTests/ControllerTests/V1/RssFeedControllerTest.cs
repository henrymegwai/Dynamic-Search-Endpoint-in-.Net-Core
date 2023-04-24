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
using System.Collections.Generic;

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

        [Fact]
        public void SearchV2()
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
            ExecutionResponse<PaginatedList<RssFeedItem>> response = new ExecutionResponse<PaginatedList<RssFeedItem>>();
            response.Data = returenedResult;
            response.PageNumber = returenedResult.PageIndex;
            response.ItemCount = returenedResult.Items;
            response.Status = true;
            response.Message = returenedResult.Count > 0 ? "Successful" : "No record(s) found";

            mock.Setup(p => p.SearchRssFeedsV2(searchquery, currentFilter, sortBy, pageNumber, pageSize)).Returns(response);
            RssFeedController emp = new RssFeedController(mock.Object);



            //Call the tested method
            var result = emp.SearchV2(searchquery, sortBy, pageNumber, pageSize);

            //Check the result
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            //assert that result is 200 ok staus
            Assert.True(okResult.StatusCode == 200);
            //Assert that result value matches mocked result.
            Assert.True(okResult.Value.Equals(response));



            // Test for no record found
            // Arrange
            string searchquery2 = "!";

            ExecutionResponse<PaginatedList<RssFeedItem>> anotherResponse = new ExecutionResponse<PaginatedList<RssFeedItem>>();
            anotherResponse.Data = new PaginatedList<RssFeedItem>(new List<RssFeedItem> { }, 0, 1, 20);
            anotherResponse.PageNumber = 1;
            anotherResponse.ItemCount = 0;
            anotherResponse.Status = true;
            anotherResponse.Message = anotherResponse.Data.Count > 0 ? "Successful" : "No record(s) found";

           
            mock.Setup(p => p.SearchRssFeedsV2(searchquery2, currentFilter, sortBy, pageNumber, pageSize)).Returns(anotherResponse);
            RssFeedController rssFeedController = new RssFeedController(mock.Object);



            //Call the tested method
            var searchResult = rssFeedController.SearchV2(searchquery2, sortBy, pageNumber, pageSize);

            //Check the result
            var expectedResult = searchResult as OkObjectResult;
            //assert that result is 200 ok staus
            Assert.True(expectedResult.StatusCode == 200);
            var executedResponse = expectedResult.Value as ExecutionResponse<PaginatedList<RssFeedItem>>;
            //Assert that result value returns no record.
            Assert.True(executedResponse.Message.Equals("No record(s) found"));

        }
    }
}
