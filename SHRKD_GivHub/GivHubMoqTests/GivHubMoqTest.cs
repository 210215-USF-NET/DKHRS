
using GivHubBL;
using GivHubDL;
using GivHubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.AddCharityAsync(charity)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.AddCharityAsync(charity);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);

 /*
         
           public async Task<IActionResult> AddCharityAsync([FromBody] object thisJSON)
        {
            try
            {
                var charities = JsonConvert.DeserializeObject<Charity[]>(thisJSON.ToString());
                foreach (Charity ch in charities)
                {

                    var findCharity = await _charBL.GetCharityByNameAsync(ch.Name);
                    if (findCharity == null)
                    {
                        await _charBL.AddCharityAsync(ch);
                    }

                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }
         
         */

        }

       





        [TestMethod]
        public async Task GetCharitiesByCategoryAsync_ShouldReturnOKCharityActionResult_WhenCategoryIsValid()  /////
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
        public async Task GetCharitiesByCategoryAsync_ShouldReturnNotFound_WhenCategoryIsNull() /////
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
        public async Task GetCharityByIdAsync_ShouldReturnOKCharity_WhenIDIsValid()//test ID?
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
            List<Donation> donations = new List<Donation>();
            donationBLMock.Setup(i => i.GetDonationsAsync()).ReturnsAsync(donations);
            var donationController = new DonationController(donationBLMock.Object);

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
            /*
         
         public async Task<IActionResult> AddLocationAsync([FromBody] Location location)
        {
            try
            {
                await _locBL.AddLocationAsync(location);
                return CreatedAtAction("AddLocation", location);
            }
            catch
            {
                return StatusCode(400);
            }
        }
         
         */}


        [TestMethod]
        public async Task AddLocationAsync_ShouldReturnStatusCode400_WhenLocation_IsInvalid()
        {
            //arrange
            var locationBLMock = new Mock<ILocationBL>();
            Location location = null;
            locationBLMock.Setup(i => i.AddLocationAsync(location)).ReturnsAsync(location);
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

        /*
         
          // POST api/<FollowController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddFollowAsync([FromBody] Follow fol)
        {
            if (!(fol.UserEmail.Equals(fol.FollowingEmail)))
            {
                try
                {
                    await _folBL.AddFollowAsync(fol);
                    return CreatedAtAction("AddFollow", fol);
                }
                catch
                {
                    return StatusCode(400);
                }
            }
            return StatusCode(400);
        }
         
         */


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
            List<Follow> follows = new List<Follow>();
            followBLMock.Setup(i => i.GetUserFollowsAsync(email)).Throws(new Exception());
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.GetUserFollowsAsync(email);


                //assert
            Assert.AreEqual(result, typeof(NotFoundResult));
        /*        
         *        [HttpGet("{email}/following")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserFollowsAsync(string email)
        {
            var following = await _folBL.GetUserFollowsAsync(email);
            if (following == null) return NotFound();
            return Ok(following);
        }*/
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
        public async Task GetFollowingUserSubscriptionsAsync_ShouldReturnNull_WhenEmailIsInvalid() //////////////////
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string email = null;
            Follow follow = new Follow();
            followBLMock.Setup(i => i.GetFollowingUserSubscriptions(email)).Throws(new Exception());
            var followController =  new FollowController(followBLMock.Object);

            //act
            var result = await followController.GetFollowingUserSubscriptionsAsync(email);

            //assert
           
            
        }

        /*

       public async Task<IActionResult> GetFollowingUserSubscriptionsAsync(string email)
        {
            return Ok(await _folBL.GetFollowingUserSubscriptions(email));
        }
    }

         */



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
        public async Task DeleteFollowAsync_ShouldReturnStatusCode500_WhenUserEmail_IsInvalid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string userEmail = null;
            string followEmail = "followEmail";
            Follow follow = new Follow();
            followBLMock.Setup(i => i.DeleteFollowAsync(follow)).Throws(new Exception());
            var followController = new FollowController(followBLMock.Object);

            //act
            var result = await followController.DeleteFollowAsync(userEmail, followEmail);

            //assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
        }


        [TestMethod]
        public async Task DeleteFollowAsync_ShouldReturnStatusCode500_WhenFollowEmail_IsInvalid()
        {
            //arrange
            var followBLMock = new Mock<IFollowBL>();
            string userEmail = "userEmail";
            string followEmail = null;
            Follow follow = new Follow();
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
    }
}