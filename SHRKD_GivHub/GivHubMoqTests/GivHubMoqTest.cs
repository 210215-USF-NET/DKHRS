
using GivHubBL;
using GivHubDL;
using GivHubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SHRKD_GivHub.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GivHubMoqTests
{
    /*

        Arrange is all about setting up the things u need for the test.
        Act is doing the thing u wanna test
        Assert is comparing the actual results to the expected outcome

     */

    [TestClass]
    public class GivHubMoqTests
    {
        public GivHubMoqTests()
        {
            //settings 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("GHDB");
            var options = new DbContextOptionsBuilder<GHDBContext>()
              .UseNpgsql(connectionString)
              .Options;
            var ghDBContext = new GHDBContext(options);

        }


        /***************************
         * Charity Controller
        **************************/

        [TestMethod]
        public async Task AddCharityAsync_ShouldReturnStatusCode400_WhenCharityIsNull()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = null;
            charityBLMock.Setup(i => i.AddCharityAsync(charity)).Throws(new Exception());
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.AddCharityAsync(charity);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public async Task DeleteCharityAsync_ShouldReturnNoContent_WhenCharityIDIsValid() ///
        {
            //arrange
            int id = 2;
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.DeleteCharityAsync(charity))
                         .ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.DeleteCharityAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }



        [TestMethod]
        public async Task DeleteCharityAsync_ShouldReturnStatusCode500_WhenCharityIDIsInvalid()
        {

            //arrange
            int id = -1;
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = null;
            charityBLMock.Setup(i => i.DeleteCharityAsync(charity))
                         .Throws(new Exception());
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.DeleteCharityAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);


        }

        [TestMethod]
        public async Task AddCharityAsync_ShouldReturnStatusCode200_WhenCharityIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity[] charities = new Charity[1];
            Charity charity = new Charity()  //getcharitybynameasync(CH.NAME)
            {
                Name = "Veteran Charity",
                Missionstatement = "mission statement",
                Logourl = "logourl",
                Category = "category",
                EID = "eid",
                Website = "website"
            };
            Charity findCharity = null;
            charities[0] = charity;
            string thisJSON = JsonConvert.SerializeObject(charities);
            charityBLMock.Setup(i => i.GetCharityByNameAsync(charity.Name)).ReturnsAsync(findCharity);
            charityBLMock.Setup(i => i.AddCharityAsync(charity)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.AddCharityAsync(thisJSON);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);

        }


        [TestMethod]
        public async Task AddCharityAsync_ShouldReturnStatusCode400_WhenThisJSONIsNull()
        {
            var charityBLMock = new Mock<ICharityBL>();
            Charity[] charities = new Charity[1];
            Charity charity = new Charity()  //getcharitybynameasync(CH.NAME)
            {
                Name = "Veteran Charity",
                Missionstatement = "mission statement",
                Logourl = "logourl",
                Category = "category",
                EID = "eid",
                Website = "website"
            };
            Charity findCharity = null;
            charities[0] = charity;
            string thisJSON = null;
            charityBLMock.Setup(i => i.GetCharityByNameAsync(charity.Name)).ReturnsAsync(findCharity);
            charityBLMock.Setup(i => i.AddCharityAsync(charity)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.AddCharityAsync(thisJSON);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);


        }







        [TestMethod]
        public async Task GetCharitiesByCategoryAsync_ShouldReturnOKCharityActionResult_WhenCategoryIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string category = "category";
            var charities = new List<Charity>();
            charityBLMock.Setup(i => i.GetCharitiesByCategoryAsync(category)).ReturnsAsync(charities);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharitiesByCategoryAsync(category);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task GetCharitiesByCategoryAsync_ShouldReturnNotFound_WhenCategoryIsNull()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string category = null;
            List<Charity> charities = null;
            charityBLMock.Setup(i => i.GetCharitiesByCategoryAsync(category)).ReturnsAsync(charities);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharitiesByCategoryAsync(category);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }


        [TestMethod]
        public async Task GetCharityByIdAsync_ShouldReturnOKCharity_WhenIDIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            int id = 2;
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.GetCharityByIdAsync(id)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByIdAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task GetCharityByIdAsync_ShouldReturnNotFound_WhenIDIsInvalid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            int id = -2;
            Charity charity = null;
            charityBLMock.Setup(i => i.GetCharityByIdAsync(id)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByIdAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task GetCharityByEidAsync_ShouldReturnOKCharityActionResult_WhenEIDIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string eid = "EID";
            var charity = new Charity();
            charityBLMock.Setup(i => i.GetCharityByEidAsync(eid)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByEidAsync(eid);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task GetCharityByEidAsync_ShouldReturnNotFound_WhenEIDIsNull() ////
        {

            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string eid = null;
            Charity charity = null;
            charityBLMock.Setup(i => i.GetCharityByEidAsync(eid)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByEidAsync(eid);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));


        }

        [TestMethod]
        public async Task GetCharityByNameAsync_ShouldReturnOKActionResult_WhenNameIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string name = "name";
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.GetCharityByNameAsync(name)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByNameAsync(name);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task GetCharityByNameAsync_ShouldReturnNotFound_WhenNameIsNull()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string name = null;
            Charity charity = null;
            charityBLMock.Setup(i => i.GetCharityByNameAsync(name)).ReturnsAsync(charity);
            CharityController charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByNameAsync(name);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task GetDonationsASync_ShouldAlwaysReturnOKObjectResult()
        {
            //arrange
            var donationBLMock = new Mock<IDonationBL>();
            var charityBLMock = new Mock<ICharityBL>();
            List<Donation> donations = new List<Donation>();
            donationBLMock.Setup(i => i.GetDonationsAsync()).ReturnsAsync(donations);
            var donationController = new DonationController(donationBLMock.Object, charityBLMock.Object);

            //act
            var result = await donationController.GetDonationsAsync();

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCharityByWebsiteAsync_ShouldReturnOKActionResult_WhenWebsiteIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string website = "website";
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.GetCharityByWebsiteAsync(website)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByWebsiteAsync(website);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task GetCharityByWebsiteAsync_ShouldReturnNotFound_WhenWebsiteIsNull()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            string website = null;
            Charity charity = null;
            charityBLMock.Setup(i => i.GetCharityByWebsiteAsync(website)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.GetCharityByWebsiteAsync(website);

            //assert iactionresult
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateCharityAsync_ShouldReturnNoContentResult_WhenCharityIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.UpdateCharityAsync(charity)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.UpdateCharityAsync(charity);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task UpdateCharityAsync_ShouldReturnStatusCode500_WhenCharityIsNull()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = null;
            charityBLMock.Setup(i => i.UpdateCharityAsync(charity)).Throws(new Exception());
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.UpdateCharityAsync(charity);


            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
        }




        /************************
         Location Controller
       ************************/
        [TestMethod]
        public async Task AddLocationAsync_Should_ReturnCreatedAtActionResult_WhenLocation_IsValid()
        {
            //arrange
            var locationBLMock = new Mock<ILocationBL>();
            Location location = new Location();
            locationBLMock.Setup(i => i.AddLocationAsync(location)).ReturnsAsync(location);
            var locationController = new LocationController(locationBLMock.Object);

            //act
            var result = await locationController.AddLocationAsync(location);

            //assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

            
         
       
        


        [TestMethod]
        public async Task AddLocationAsync_ShouldReturnStatusCode400_WhenLocation_IsInvalid()
        {
            //arrange
            var locationBLMock = new Mock<ILocationBL>();
            Location location = null;
            locationBLMock.Setup(i => i.AddLocationAsync(location)).Throws(new Exception());
            var locationController = new LocationController(locationBLMock.Object);

            //act
            var result = await locationController.AddLocationAsync(location);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);
        }






        [TestMethod]
        public async Task DeleteLocationAsync_ShouldReturnNoContent_WhenLocationIDIsValid()
        {
            //arrange
            int id = 2;
            var locationBLMock = new Mock<ILocationBL>();
            Location location = new Location();
            locationBLMock.Setup(i => i.DeleteLocationAsync(location)).ReturnsAsync(location);
            var locationController = new LocationController(locationBLMock.Object);

            //act
            var result = await locationController.DeleteLocationAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteLocationAsync_ShouldReturnStatusCode500_WhenLocationIDIsInvalid()
        {
            //arrange
            var locationBLMock = new Mock<ILocationBL>();
            int id = -3;
            Location location = null;
            locationBLMock.Setup(i => i.DeleteLocationAsync(location)).Throws(new Exception());
            var locationController = new LocationController(locationBLMock.Object);

            //act
            var result = await locationController.DeleteLocationAsync(id);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);

        }


        /**********************************
                Follow Controller
         **********************************/
        [TestMethod]
        public async Task AddFollowAsync_ShouldReturnCreatedAtActionResult_WhenFollowIsValid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            List<Follow> follows = new List<Follow>();
            var follow = new Follow()
            {
                UserEmail = "user email",
                FollowingEmail = "following email"
            };
            followBLMock.Setup(i => i.GetUserFollowsAsync(follow.UserEmail)).ReturnsAsync(follows);
            followBLMock.Setup(i => i.AddFollowAsync(follow)).ReturnsAsync(follow);
            FollowController followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.AddFollowAsync(follow);

            //assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }


        [TestMethod]
        public async Task AddFollowAsync_ShouldReturnStatusCode400_WhenUserEmailAndFollowingEmail_AreTheSame()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            List<Follow> follows = new List<Follow>();
            var follow = new Follow()
            {
                UserEmail = "same email",
                FollowingEmail = "same email"
            };
            followBLMock.Setup(i => i.GetUserFollowsAsync(follow.UserEmail)).ReturnsAsync(follows);
            followBLMock.Setup(i => i.AddFollowAsync(follow)).ReturnsAsync(follow);
            FollowController followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.AddFollowAsync(follow);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);
        }


        [TestMethod]
        public async Task AddFollowAsync_ShouldReturnStatusCode400_WhenFollowAlreadyExists()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            List<Follow> follows = new List<Follow>();
            var follow = new Follow()
            {
                UserEmail = "user email",
                FollowingEmail = "following email"
            };
            follows.Add(follow);
            followBLMock.Setup(i => i.GetUserFollowsAsync(follow.UserEmail)).ReturnsAsync(follows);
            followBLMock.Setup(i => i.AddFollowAsync(follow)).ReturnsAsync(follow);
            FollowController followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.AddFollowAsync(follow);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);
        }


        [TestMethod]
        public async Task GetUserFollowsAsync_ShouldReturnOKFollowing_WhenEmailIsValid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string email = "email";
            List<Follow> follows = new List<Follow>();
            followBLMock.Setup(i => i.GetUserFollowsAsync(email)).ReturnsAsync(follows);
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.GetUserFollowsAsync(email);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
        [TestMethod]
        public async Task GetUserFollowsAsync_ShouldReturnNotFound_WhenEmailIsNull()///////////////
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string email = null;
            List<Follow> follows = null;
            followBLMock.Setup(i => i.GetUserFollowsAsync(email)).ReturnsAsync(follows);
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.GetUserFollowsAsync(email);


            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }



        [TestMethod]
        public async Task GetFollowingUserSubscriptionsAsync_ShouldReturnOKObjectResult_WhenEmailIsValid()  //////////
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string email = "email";
            List<Follow> follows = new List<Follow>();
            //followBLMock.Setup(i => i.GetFollowingUserSubscriptions(email)).ReturnsAsync(follows);
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.GetFollowingUserSubscriptionsAsync(email);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }






        [TestMethod]
        public async Task DeleteFollowAsync_ShouldReturnNoContent_WhenUserEmailAndFollowEmail_AreBothValid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string userEmail = "userEmail";
            string followEmail = "followEmail";
            Follow follow = new Follow();
            followBLMock.Setup(i => i.DeleteFollowAsync(follow)).ReturnsAsync(follow);
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.DeleteFollowAsync(userEmail, followEmail);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));


        }


        [TestMethod]
        public async Task DeleteFollowAsync_ShouldReturnStatusCode500_WhenFollowEmailOrUserEmail_IsInvalid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string userEmail = "userEmail";
            string followEmail = null;
            Follow follow = null;
            followBLMock.Setup(i => i.GetSingleUserFollowAsync(userEmail, followEmail)).ReturnsAsync(follow);
            followBLMock.Setup(i => i.DeleteFollowAsync(follow)).Throws(new Exception());
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.DeleteFollowAsync(userEmail, followEmail);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);



        }



        /************************
             Search History Controller
           ************************/

        [TestMethod]
        public async Task AddSearchHistoryAsync_ShouldReturnCreatedAtActionResult_WhenSearchHistoryIsValid()
        {
            //arrange 
            var searchHistoryBLMock = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = new SearchHistory();
            searchHistoryBLMock.Setup(i => i.AddSearchHistoryAsync(searchHistory)).ReturnsAsync(searchHistory);
            var searchHistoryController = new SearchHistoryController(searchHistoryBLMock.Object);

            //act
            var result = await searchHistoryController.AddSearchHistoryAsync(searchHistory);

            //assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));

        }


        [TestMethod]
        public async Task AddSearchHistoryAsync_ShouldReturnStatusCode400_WhenSearchHistoryIsNull()
        {
            //arrange 
            var searchHistoryBLMock = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = null;
            searchHistoryBLMock.Setup(i => i.AddSearchHistoryAsync(searchHistory)).Throws(new Exception());
            var searchHistoryController = new SearchHistoryController(searchHistoryBLMock.Object);

            //act
            var result = await searchHistoryController.AddSearchHistoryAsync(searchHistory);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(400, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public async Task DeleteSearchHistoryAsync_ShouldReturnNoContent_WhenEmailAndID_AreBothValid()
        {
            //arrange
            var searchHistoryBLMock = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = new SearchHistory();
            string email = "any email";
            int id = 2;
            searchHistoryBLMock.Setup(i => i.GetUserSingleSearchHistoryAsync(email, id)).ReturnsAsync(searchHistory);
            searchHistoryBLMock.Setup(i => i.DeleteSearchHistoryAsync(searchHistory)).ReturnsAsync(searchHistory);
            SearchHistoryController searchHistoryController = new SearchHistoryController(searchHistoryBLMock.Object);

            //act
            var result = await searchHistoryController.DeleteSearchHistoryAsync(email, id);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }


        [TestMethod]
        public async Task DeleteSearchHistoryAsync_ShouldReturnStatusCode500_WhenEmailOrID_IsInvalid()
        {
            //arrange
            var searchHistoryBLMock = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = null;
            string email = "any email";
            int id = 2;
            searchHistoryBLMock.Setup(i => i.GetUserSingleSearchHistoryAsync(email, id)).ReturnsAsync(searchHistory);
            searchHistoryBLMock.Setup(i => i.DeleteSearchHistoryAsync(searchHistory)).Throws(new Exception());
            SearchHistoryController searchHistoryController = new SearchHistoryController(searchHistoryBLMock.Object);

            //act
            var result = await searchHistoryController.DeleteSearchHistoryAsync(email, id);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);

        }


        [TestMethod]
        public async Task GetSearchHistoriesAsync_ShouldReturnOKObjectResult()
        {
            //arrange
            var searchHistoryBLMock = new Mock<ISearchHistoryBL>();
            List<SearchHistory> searchHistories = new List<SearchHistory>();
            searchHistoryBLMock.Setup(i => i.GetSearchHistoriesAsync()).ReturnsAsync(searchHistories);
            SearchHistoryController searchHistoryController = new SearchHistoryController(searchHistoryBLMock.Object);

            //act
            var result = await searchHistoryController.GetSearchHistoriesAsync();

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateSearchHistoryAsync_ShouldReturnNoContent_WhenSearchHistory_IsValid()
        {
            //arrange
            var searchHistoryBLM = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = new SearchHistory();
            searchHistoryBLM.Setup(i => i.UpdateSearchHistoryAsync(searchHistory)).ReturnsAsync(searchHistory);
            var searchHistoryController = new SearchHistoryController(searchHistoryBLM.Object);

            //act
            var result = await searchHistoryController.UpdateSearchHistoryAsync(searchHistory);

            //assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));

        }

        [TestMethod]
        public async Task UpdateSearchHistoryAsync_ShouldReturnStatusCode500_WhenSearchHistory_IsInvalid()
        {
            //arrange
            var searchHistoryBLM = new Mock<ISearchHistoryBL>();
            SearchHistory searchHistory = null;
            searchHistoryBLM.Setup(i => i.UpdateSearchHistoryAsync(searchHistory)).Throws(new Exception());
            var searchHistoryController = new SearchHistoryController(searchHistoryBLM.Object);

            //act
            var result = await searchHistoryController.UpdateSearchHistoryAsync(searchHistory);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public async Task GetSearchHistoriesByUserAsync_ShouldReturnOKObjectResult_WhenEmail_IsValid()
        {
            //arrange
            var searchHistoryBLM = new Mock<ISearchHistoryBL>();
            string email = "email";
            List<SearchHistory> searchHistories = new List<SearchHistory>();
            searchHistoryBLM.Setup(i => i.GetSearchHistoriesByUserAsync(email)).ReturnsAsync(searchHistories);
            var searchHistoryController = new SearchHistoryController(searchHistoryBLM.Object);

            //act
            var result = await searchHistoryController.GetSearchHistoriesByUserAsync(email);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }


        [TestMethod]
        public async Task GetSearchHistoriesByUserAsync_ShouldReturnNotFound_WhenListIsInvalid()
        {
            //arrange
            var searchHistoryBLM = new Mock<ISearchHistoryBL>();
            string email = "email";
            List<SearchHistory> searchHistories = null;
            searchHistoryBLM.Setup(i => i.GetSearchHistoriesByUserAsync(email)).ReturnsAsync(searchHistories);
            var searchHistoryController = new SearchHistoryController(searchHistoryBLM.Object);

            //act
            var result = await searchHistoryController.GetSearchHistoriesByUserAsync(email);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        /**************************
         * Subscription Controller
         * ***********************/

        [TestMethod]
        public async Task<IActionResult> AddSubscriptionAsync_ShouldReturnCreatedAtActionResult_WhenSubscriptionIsValid()
        {
            var subscriptionBLMock = new Mock<ISubscriptionBL>();
        }

        /*
          public async Task<IActionResult> AddSubscriptionAsync([FromBody] Subscription subscription)
        {
            try
            {
                var findSub = await _subBL.GetSingleUserSubscription(subscription.Email, subscription.CharityId);
                if (findSub != null) return NotFound();
                await _subBL.AddSubscriptionAsync(subscription);
                return CreatedAtAction("AddSubscription", subscription);
            }
            catch
            {
                return StatusCode(400);
            }
        }
         */

    }
}