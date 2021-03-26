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

        }

        [TestMethod]
        public async void GetDonationByIdAsync_ShouldReturnCorrectDonation_WhenIDIsValid()
        {
            //arrange
            int id = 2;

            //act
            var result = await donationBL.GetDonationByIdAsync(id);

            //assert
            Assert.AreEqual(id, result.Id);


        }


        [TestMethod]
        public async void GetDonationsByUserAsync_ShouldReturnNull_WhenEmailIsNull()
        {
            //arrange
            string email = null;

            //act
            var result = await donationBL.GetDonationsByUserAsync(email);

            //assert
            Assert.IsNull(result);

        }



        [TestMethod]
        public async void GetDonationsByUserAsync_ShouldReturnCorrectDonations_WhenEmailIsValid()
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


        //[TestMethod]
        //public async void GetDonationsAsync_ShouldSucceed_WhenDonationAmountIsGreaterThanZeroAsync()
        //{
        //    //arrange
        //   // decimal amount = 100m;

        //    //act
        //    var result = await donationBL.GetDonationsAsync();

        //    //assert
        //    foreach (var donation in result)
        //    {
        //        Assert.AreEqual(amount, donation.Amount);

        //    } 
        //}

        [TestMethod]
        public async void GetDonationsByCharityAsync_ShouldReturnCorrectCharity_WhenCharityIsValid()
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
            
        }



        [TestMethod]
        public async void DeleteCharityAsync_ShouldReturnCharityToBeDeleted_WhenCharity2BDeletedIsValid()
        {
            //arrange
            Charity charity2BDeleted = new Charity();

            //act 
            var result = await charityBL.DeleteCharityAsync(charity2BDeleted);

            //assert
            Assert.IsNotNull(result);

            //foreach (var charity in result) 
            //{
            //    Assert.AreEqual(charity2BDeleted, charity._repo);
            //}


        }


        [TestMethod]
        public async void DeleteCharityAsync_ShouldReturnNull_WhenCharity2BDeletedIsNull()
        {
            //assert
            Charity charity2BDeleted = null;

            //act
            var result = await charityBL.DeleteCharityAsync(charity2BDeleted);

            //assert
            Assert.IsNull(charity2BDeleted);
        }



        [TestMethod]
        public async void GetCharitiesAsync_ShouldReturnCharities_WhenValid()
        {
            ///////////////
        }


        [TestMethod]
        public async void GetCharitiesByCategoryAsync_ShouldReturnCharities_WhenCategoryIsValid()
        {
            //arrange
            string category = "category";

            //act
            var result = await charityBL.GetCharitiesByCategoryAsync(category);

            //assert
            Assert.IsNotNull(result);

            //foreach (var charity in result)
            //{
            //    Assert.AreEqual(category, charity.Category);
            //}
            //////////////////////
        }

        [TestMethod]
        public async void GetCharitiesAsync_ShouldFail_WhenStringNameIsNullAsync()
        {
            try
            {
                //arrange
                int ID = 1;
                string name = null;

                //act 
                var result = await charityBL.GetCharitiesAsync();
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.IsNotNull("Name must contain a value.", ex.Message);
            }


        }

        [TestMethod]
        public async void GetCharitiesByCategoryAsync_ShouldReturnNull_WhenCategoryIsNull()
        {

            //arrange
            string category = null;

            //act
            var result = await charityBL.GetCharitiesByCategoryAsync(category);

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async void GetCharitiesAsync_ShouldSucceed_WhenFieldsAreNotNullAsync()
        {
            //arrange
            Charity charity = new Charity();
            int ID = 1;
            string name = "name";
            string missionStatement = "mission statement";
            string website = "www.website.com";
            string category = "category";
            string logourl = "www.logoURL.com";


            //act
            var result = await charityBL.GetCharitiesAsync();

            //assert
            foreach (var charities in result)
            {
                Assert.AreEqual(ID, charity.Id);
                Assert.AreEqual(name, charity.Name);
                Assert.AreEqual(missionStatement, charity.Missionstatement);
                Assert.AreEqual(website, charity.Website);
                Assert.AreEqual(category, charity.Category);
                Assert.AreEqual(logourl, charity.Logourl);
            }
        }


        [TestMethod]
        public async void GetCharityByIdAsync_ShouldReturnCharity_WhenIDIsValid()
        {
            //assert
            int ID = 2;

            //act
            var result = await charityBL.GetCharityByIdAsync(ID);

            //assert
            Assert.IsNotNull(result);

            //foreach (var charity in result)
            //{
            //    Assert.AreEqual(ID, charity.Id);
            //}
        }


        [TestMethod]
        public async void GetCharityByEidAsync_ShouldReturnCharity_WhenEIDIsValid()
        {
            //assert
            string eid = "EID";

            //act
            var result = await charityBL.GetCharityByEidAsync(eid);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void GetCharityByEidAsync_ShouldReturnNull_WhenEIDIsNull()
        {
            //arrange 
            string eid = null;

            //act
            var result = await charityBL.GetCharityByEidAsync(eid);

            //assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public async void GetCharityByNameAsync_ShouldReturnCharity_WhenNameIsValid()
        {
            //arrange
            string name = "charityName";

            //act
            var result = await charityBL.GetCharityByNameAsync(name);

            //assert
            Assert.IsNotNull(result);
        
        }

        [TestMethod]
        public async void GetCharityByNameAsync_ShouldReturnNull_WhenNameIsNull()
        {
            //arrange
            string name = null;

            //act
            var result = await charityBL.GetCharityByNameAsync(name);

            //assert
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public async void GetCharityByWebsiteAsync_ShouldReturnCharity_WhenWebsiteIsValid()
        {
            //arrange
            string website = "www.website.com";

            //act
            var result = await charityBL.GetCharityByWebsiteAsync(website);

            //assert
            Assert.IsNotNull(result);
        }
             
        [TestMethod]
        public async void GetCharityByWebsiteAsync_ShouldReturnNull_WhenWebsiteIsNull()
        {
            //arrange
            string website = null;

            //act
            var result = await charityBL.GetCharityByWebsiteAsync(website);

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async void UpdateCharityAsync_ShouldUpdateCharity_WhenCharityIsValid()
        {
            //arrange
            Charity charity2BUpdated = new Charity();

            //act
            var result = await charityBL.UpdateCharityAsync(charity2BUpdated);

            //assert
            Assert.AreEqual(result, charity2BUpdated.Id);

        }

        [TestMethod]
        public async void UpdateCharityAsync_ShouldReturnNull_WhenCharityIsNull()
        {
            //arrange
            Charity charity2BUpdated = null;

            //act
            var result = await charityBL.UpdateCharityAsync(charity2BUpdated);

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async void DeleteLocationAsync_ShouldDeleteLocation_WhenLocationIsValid()
        {
            //arrange
            Location location2BDeleted = new Location();

            //act
            var result = await locationBL.DeleteLocationAsync(location2BDeleted);

            //assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async void DeleteLocationAsync_ShouldReturnNull_WhenLocationIsNull()
        {
            //arrange
            Location location2BDeleted = null;

            //act
            var result = await locationBL.DeleteLocationAsync(location2BDeleted);

            //assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async void GetLocationByCityStateAsync_ShouldReturnLocation_WhenCityAndStateAreBothValid() //
        {
            //arrange
            string city = "city";
            string state = "state";

            //act
            var result = await locationBL.GetLocationByCityStateAsync(city, state);

            ////assert
            //foreach (var locations in result)
            //{
            //    Assert.AreEqual(city, locations.city);
            //    Assert.AreEqual(state, locations.state);
            //}
            
        }

        [TestMethod]
        public async void GetLocationByCityStateAsync_ShouldReturnNull_WhenCityOrStateIsNull() //
        {
            //arrange
            string city = null;
            string state = "state";
      

        }
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
