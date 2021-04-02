using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GivHubModels;
using Microsoft.EntityFrameworkCore;
using GivHubDL;
using GivHubBL;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GivHubTests
{
    [TestClass]
    public class GivHubIntegrationTests
    /*

        Arrange is all about setting up the things u need for the test.
        Act is doing the thing u wanna test
        Assert is comparing the actual results to the expected outcome

     */
    {
        private DonationBL donationBL = null;
        private CharityBL charityBL = null;
        private LocationBL locationBL = null;
        private SearchHistoryBL searchHistoryBL = null;

        public GivHubIntegrationTests()
        {
            //settings 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("GHDB");
            var options = new DbContextOptionsBuilder<GHDBContext>()
              .UseNpgsql(connectionString)
              .Options;
            var ghDBContext = new GHDBContext(options);

            //donations
            DonationRepoDB donationRepoDB = new DonationRepoDB(ghDBContext);
            donationBL = new DonationBL(donationRepoDB);
            Donation donation = new Donation();

            //charities
            CharityRepoDB charityRepoDB = new CharityRepoDB(ghDBContext);
            charityBL = new CharityBL(charityRepoDB);
            Charity charity = new Charity();

            //locations
            LocationRepoDB locationRepoDB = new LocationRepoDB(ghDBContext);
            locationBL = new LocationBL(locationRepoDB);
            Location location = new Location();

            //search Histories
            SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
            searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
            SearchHistory searchHistory = new SearchHistory();
        }

        [TestMethod]
        public async Task GetDonationByIdAsync_ShouldReturnCorrectDonation_WhenIDIsValid()
        {
            //arrange
            int id = 7;

            //act
            var result = await donationBL.GetDonationByIdAsync(id);

            //assert
            Assert.AreEqual(id, result.Id);


        }


        [TestMethod]
        public async Task GetDonationsByUserAsync_ShouldReturnNull_WhenEmailIsNull()
        {
            //arrange
            string email = null;

            //act
            var result = await donationBL.GetDonationsByUserAsync(email);

            //assert
            Assert.AreEqual(0, result.Count);

        }



        [TestMethod]
        public async Task GetDonationsByUserAsync_ShouldReturnCorrectDonations_WhenEmailIsValid()
        {
            //arrange
            string email = "email@email.com";

            //act
            var result = await donationBL.GetDonationsByUserAsync(email);

            //assert
            foreach (var donation in result)
            {
                Assert.AreEqual(email, donation.Email);

            }

        }


       

        [TestMethod]
        public async Task GetDonationsByCharityAsync_ShouldReturnCorrectCharity_WhenCharityIsValid()
        {
            //arrange
            int charityID = 2;

            //act
            var result = await donationBL.GetDonationsByCharityAsync(charityID);

            //assert
            foreach (var donation in result)
            {
                Assert.AreEqual(charityID, donation.CharityId);
            }

            //areequal is a stricter assertion than isnotnull..When I ask for donationID 2 I'm telling to areequal: If I give u an id make sure u find that id
           // use foreach when dealing with lists
        }



        [TestMethod]        
        [ExpectedException (typeof(ArgumentNullException))]
        public async Task DeleteCharityAsync_ShouldThrowArgumentNullException_WhenCharity2BDeletedIsNull() 
        {
            //assert
            Charity charity2BDeleted = null;

            //act
            var result = await charityBL.DeleteCharityAsync(charity2BDeleted);

            //assert
            //Assert.IsNull(result);

        }



        [TestMethod]
        public async Task GetCharitiesByCategoryAsync_ShouldReturnCharities_WhenCategoryIsValid()
        {
            //arrange
            string category = "category";

            //act
            var result = await charityBL.GetCharitiesByCategoryAsync(category);

            //assert
            foreach (var charity in result)
            {
                Assert.AreEqual(category, charity.Category);
            }
            
        }

     

        [TestMethod]
        public async Task GetCharitiesByCategoryAsync_ShouldReturnNull_WhenCategoryIsNull()
        {

            //arrange
            string category = null;

            //act
            var result = await charityBL.GetCharitiesByCategoryAsync(category);

            //assert
            Assert.AreEqual(0,result.Count);
        }



        [TestMethod]
        public async Task GetCharityByIdAsync_ShouldReturnCharity_WhenIDIsValid()
        {
            //assert
            int ID = 33;

            //act
            var result = await charityBL.GetCharityByIdAsync(ID);

            //assert
            Assert.AreEqual(ID, result.Id);
        }


        [TestMethod]
        public async Task GetCharityByEidAsync_ShouldReturnCharity_WhenEIDIsValid()
        {
            //assert
            string eid = "330473813";

            //act
            var result = await charityBL.GetCharityByEidAsync(eid);

            //assert
            Assert.AreEqual(eid ,result.EID);
        }

        [TestMethod]
        public async Task GetCharityByEidAsync_ShouldReturnNull_WhenEIDIsNull()
        {
            //arrange 
            string eid = null;

            //act
            var result = await charityBL.GetCharityByEidAsync(eid);

            //assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public async Task GetCharityByNameAsync_ShouldReturnCharity_WhenNameIsValid()
        {
            //arrange
            string name = "CANCER CLIK";

            //act
            var result = await charityBL.GetCharityByNameAsync(name);

            //assert
            Assert.AreEqual(name, result.Name);
        
        }

        [TestMethod]
        public async Task GetCharityByNameAsync_ShouldReturnNull_WhenNameIsNull()
        {
            //arrange
            string name = null;

            //act
            var result = await charityBL.GetCharityByNameAsync(name);

            //assert
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public async Task GetCharityByWebsiteAsync_ShouldReturnCharity_WhenWebsiteIsValid()
        {
            //arrange
            string website = "http://www.orghunter.com/organization/954167790";

            //act
            var result = await charityBL.GetCharityByWebsiteAsync(website);

            //assert
            Assert.AreEqual(website, result.Website);
        }
             
        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException)) ]
        public async Task GetCharityByWebsiteAsync_ShouldReturnNull_WhenWebsiteIsNull()
        {
            //arrange
            string website = null;

            //act
            var result = await charityBL.GetCharityByWebsiteAsync(website);

            //assert
           // Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateCharityAsync_ShouldUpdateCharity_WhenCharityIsValid()
        {
            //arrange
            Charity charity2BUpdated = new Charity();
            charity2BUpdated.Id = 7;
            charity2BUpdated.EID = "472293681";
            charity2BUpdated.Category = "Diseases, Disorders, Medical Disciplines";
            charity2BUpdated.Logourl = "none";
            charity2BUpdated.Missionstatement = "none";
            charity2BUpdated.Website = "http://www.orghunter.com/organization/472293681";

            string newName = new Guid().ToString();  //guid is a random string
            charity2BUpdated.Name = newName;

            //act
            var result = await charityBL.UpdateCharityAsync(charity2BUpdated);

            //assert
            Assert.AreEqual(newName, result.Name);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task UpdateCharityAsync_ShouldReturnNull_WhenCharityIsNull()
        {
            //arrange
            Charity charity2BUpdated = null;

            //act
            var result = await charityBL.UpdateCharityAsync(charity2BUpdated);

            //assert expected exception

            
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteLocationAsync_ShouldReturnNull_WhenLocationIsNull()
        {
            //arrange
            Location location2BDeleted = null;

            //act
            var result = await locationBL.DeleteLocationAsync(location2BDeleted);

            //assert expected exception
           

        }

        [TestMethod]
        public async Task GetLocationByCityStateAsync_ShouldReturnLocation_WhenCityAndStateAreBothValid() 
        {
            //arrange
            string city = "Austin";
            string state = "Texas";

            //act
            var result = await locationBL.GetLocationByCityStateAsync(city, state);

            ////assert
         
            Assert.AreEqual(city, result.City);
            Assert.AreEqual(state, result.State);
            
            
        }

        [TestMethod]
        public async Task GetLocationByCityStateAsync_ShouldReturnNull_WhenCityIsNull() 
        {
            //arrange
            string city = null;
            string state = "TX";

            //act
            var result = await locationBL.GetLocationByCityStateAsync(city, state);

            //assert
            Assert.IsNull(result);
      

        }

        [TestMethod]
        public async Task GetLocationByCityStateAsync_ShouldReturnNull_WhenStateIsNull() 
        {
            //arrange
            string city = "Austin";
            string state = null;

            //act
            var result = await locationBL.GetLocationByCityStateAsync(city, state);

            //assert
            Assert.IsNull(result);


        }

        [TestMethod]
        public async Task GetLocationByIdAsync_ShouldReturnID_WhenIDisValid()
        {
            //arrange 
            int id = 6;

            //act 
            var result = await locationBL.GetLocationByIdAsync(id);

            //assert
            Assert.AreEqual(id, result.Id);

        }

        [TestMethod]
        public async Task UpdateLocationAsync_ShouldReturnUpdatedLocation_WhenLocationIsValid()
        {
            Location location2BUpdated = new Location();
            //arrange
            location2BUpdated.Id = 6;
            location2BUpdated.CharityId = 6;
            string newCity = new Guid().ToString();  //guid is a random string
            string newState = new Guid().ToString();  //guid is a random string
            string newZipCode = new Guid().ToString();  //guid is a random string


            location2BUpdated.City = newCity;
            location2BUpdated.State = newState;
            location2BUpdated.Zipcode = newZipCode;

            //act
            var result = await locationBL.UpdateLocationAsync(location2BUpdated);

            //assert
            Assert.AreEqual(newCity, result.City);
            Assert.AreEqual(newState, result.State);
            Assert.AreEqual(newZipCode, result.Zipcode);

            //cleanup
            location2BUpdated.City = "Austin";
            location2BUpdated.State = "Texas";
            await locationBL.UpdateLocationAsync(location2BUpdated);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task UpdateLocationAsync_ShouldReturnNullWhenLocationIsNull()
        {
            //arrange
            Location location2BUpdated = null;

            //act
            var result = await locationBL.UpdateLocationAsync(location2BUpdated);

            //assert expected exception

        }


        //TEST NULL PARAM FOR DELETE
        /*
         
          // DELETE api/<FollowController>/
        [HttpDelete("{useremail},{followemail}")]
        public async Task<IActionResult> DeleteFollowAsync(string useremail, string followemail)
        {
            try
            {
                var follow = await _folBL.GetSingleUserFollowAsync(useremail, followemail);
                await _folBL.DeleteFollowAsync(follow);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
         
         */


    }


}









//[TestMethod]
//public async Task GetSearchHistoriesAsync_ShouldSucceed_WhenPhraseAndEmailIsNotNullAsync()
//{

//    //arrange
//    int iD = 1;
//    string phrase = "Phrase";
//    string email = "123@email.com";
//    var options = new DbContextOptionsBuilder<GHDBContext>()
//    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//    .Options;

//    var ghDBContext = new GHDBContext(options);
//    SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
//    SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
//    SearchHistory searchHistory = new SearchHistory();
//    searchHistory.Phrase = phrase;
//    searchHistory.Id = iD;
//    searchHistory.Email = email;

//    //act
//    var result = await searchHistoryBL.GetSearchHistoriesAsync();
//    //assert
//    Assert.IsNotNull(searchHistory.Phrase, searchHistory.Email);
//}

//[TestMethod]
////[ExpectedException(typeof(Exception))]
////
//public async Task GetSearchHistoriesAsync_ShouldFail_WhenPhraseIsNullAsync()
//{
//    try
//    {
//        //arrange
//        int iD = 1;
//        string phrase = null;
//        string email = "123@email.com";

//        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
//        var config = builder.Build();
//        var connectionString = config.GetConnectionString("GHDB");

//        var options = new DbContextOptionsBuilder<GHDBContext>()
//          .UseNpgsql(connectionString)
//           .Options;

//        var ghDBContext = new GHDBContext(options);
//        SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
//        SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
//        SearchHistory searchHistory = new SearchHistory();
//        searchHistory.Phrase = phrase;
//        searchHistory.Id = iD;
//        searchHistory.Email = email;

//        //act 

//        var result = await searchHistoryBL.GetSearchHistoriesAsync();

//    }
//    catch (System.Exception ex)
//    {
//        //assert
//        Assert.AreEqual("Phrase must not be null.", ex.Message);
//    }


//}

//[TestMethod]
//public async Task GetSearchHistoriesAsync_ShouldFail_WhenEmailIsNullAsync()
//{
//    try
//    {
//        //arrange
//        int iD = 1;
//        string phrase = "Phrase";
//        string email = null;
//        var options = new DbContextOptionsBuilder<GHDBContext>()
//          .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//           .Options;

//        var ghDBContext = new GHDBContext(options);
//        SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
//        SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
//        SearchHistory searchHistory = new SearchHistory();
//        searchHistory.Phrase = phrase;
//        searchHistory.Id = iD;
//        searchHistory.Email = email;

//        //act 

//        var result = await searchHistoryBL.GetSearchHistoriesAsync();

//    }
//    catch (System.Exception ex)
//    {
//        //assert
//        Assert.AreEqual("Email must not be null.", ex.Message);
//    }


//}


//[TestMethod]
//public async Task GetLocationsAsync_ShouldFail_WhenStateIsNullAsync()
//{
//    try
//    {
//        //arrange
//        string state = null;
//        string zipCode = "1234567";
//        string city = "city";


//        var options = new DbContextOptionsBuilder<GHDBContext>()
//        .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//        .Options;
//        var ghDBContext = new GHDBContext(options);
//        LocationRepoDB locationRepoDB = new LocationRepoDB(ghDBContext);
//        LocationBL locationBL = new LocationBL(locationRepoDB);
//        Location location = new Location();

//        location.City = city;
//        location.State = state;
//        location.Zipcode = zipCode;

//        //act 
//        var result = await locationBL.GetLocationsAsync();
//    }
//    catch (System.Exception ex)
//    {
//        //assert
//        Assert.AreEqual("State cannot be null.", ex.Message);
//    }

//}


//[TestMethod]
//public async Task GetLocationsAsync_ShouldReturnLocation_WhenCityStateZipIsNotNullAsync()
//{
//    //arrange
//    string state = "state";
//    string zipCode = "1234567";
//    string city = "city";


//    var options = new DbContextOptionsBuilder<GHDBContext>()
//    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//    .Options;
//    var ghDBContext = new GHDBContext(options);
//    LocationRepoDB locationRepoDB = new LocationRepoDB(ghDBContext);
//    LocationBL locationBL = new LocationBL(locationRepoDB);
//    Location location = new Location();

//    location.City = city;
//    location.State = state;
//    location.Zipcode = zipCode;

//    //act
//    var result = await locationBL.GetLocationsAsync();


//    //assert
//    Assert.IsNotNull(location.City);
//    Assert.IsNotNull(location.State);
//    Assert.IsNotNull(location.Zipcode);
//}




//[TestMethod]
//public async Task GetSubscriptionsAsync_ShouldReturnNull_WhenEmailIsNullAsync()
//{

//    try
//    {
//        //arrange
//        int ID = 1;
//        string email = null;


//        var options = new DbContextOptionsBuilder<GHDBContext>()
//        .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//        .Options;
//        var ghDBContext = new GHDBContext(options);
//        SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
//        SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
//        Subscription subscription = new Subscription();

//        subscription.Id = ID;
//        subscription.Email = email;

//        //act
//        var result = await subscriptionBL.GetSubscriptionsAsync();
//    }
//    catch (System.Exception ex)
//    {
//        //assert
//        Assert.IsNotNull("Email must not be null.", ex.Message);
//    }

//}


//[TestMethod]
//public async Task GetSubscriptionsAsync_ShouldReturnNull_WhenIDIsNullAsync()
//{


//    //arrange
//    int? ID = null;
//    string email = "email@email.com";


//    var options = new DbContextOptionsBuilder<GHDBContext>()
//    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//    .Options;
//    var ghDBContext = new GHDBContext(options);
//    SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
//    SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
//    Subscription subscription = new Subscription();

//    subscription.Id = (int)ID;
//    subscription.Email = email;

//    //act
//    var result = await subscriptionBL.GetSubscriptionsAsync();



//    //assert
//    Assert.AreEqual(ID, subscription.Id);


//}


//[TestMethod]
//public async Task GetSubscriptionsAsync_ShouldSucceed_WhenIDandEmailIsNotNull()
//{


//    //arrange
//    int ID = 1;
//    string email = "email@email.com";


//    var options = new DbContextOptionsBuilder<GHDBContext>()
//    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
//    .Options;
//    var ghDBContext = new GHDBContext(options);
//    SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
//    SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
//    Subscription subscription = new Subscription();

//    subscription.Id = ID;
//    subscription.Email = email;

//    //act
//    var result = await subscriptionBL.GetSubscriptionsAsync();



//    //assert
//    Assert.AreEqual(ID, subscription.Id);
//    Assert.AreEqual(email, subscription.Email);


//}





///***********************************************************************************
       
//        ************************************************************************************/

////[TestMethod]
////public void CreateSubscription_ShouldReturnNull_WhenFirstNameIsNull()
////{
////    //arrange

////    string firstName = null;
////    string lastName = "lastname";
////    string email = "123@mail.com";
////    int locationID = 1;
////    var options = new DbContextOptionsBuilder<GHDBContext>()
////    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
////    .Options;

////    //act
////    //assert

////}




////[TestMethod]
////public void CreateSubscription_ShouldReturnNull_WhenLastNameIsNull()
////{
////    //arrange
////    string firstName = "firstname";
////    string lastName = null;
////    string email = "123@mail.com";
////    int locationID = 1;

////    var options = new DbContextOptionsBuilder<GHDBContext>()
////    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
////    .Options;
////    //act
////    //assert
////}





////[TestMethod]
////public void CreateSubscription_IsTrue_WhenFieldsAreNotNull() 
////{ }
