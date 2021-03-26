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
        public GivHubIntegrationTests()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("GHDB");
            var options = new DbContextOptionsBuilder<GHDBContext>()
              .UseNpgsql(connectionString)
              .Options;
            var ghDBContext = new GHDBContext(options);
            DonationRepoDB donationRepoDB = new DonationRepoDB(ghDBContext);
            donationBL = new DonationBL(donationRepoDB);
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



        // public async Task<Donation> AddDonationAsync(Donation newDonation)










        //[TestMethod]
        //public async Task GetDonationsAsync_ShouldSucceed_WhenDonationIsGreaterThanZeroAsync()
        //{
        //    //arrange
        //    decimal amount = 3m;
        //    int donationID = 1;


        //    var options = new DbContextOptionsBuilder<GHDBContext>()
        //    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
        //    .Options;

        //    var ghDBContext = new GHDBContext(options);
        //    DonationRepoDB donationRepoDB = new DonationRepoDB(ghDBContext);
        //    DonationBL donationBL = new DonationBL(donationRepoDB);
        //    Donation donation = new Donation();
        //    donation.Amount = amount;
        //    donation.Id = donationID;

        //    //act

        //    var result = await donationBL.GetDonationsAsync();

        //    //assert
        //    Assert.AreEqual(donation.Amount, amount);


        //}

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

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public async Task GetSearchHistoriesAsync_ShouldFail_WhenPhraseIsNullAsync()
        {
            try
            {
                //arrange
                int iD = 1;
                string phrase = null;
                string email = "123@email.com";

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();
                var connectionString = config.GetConnectionString("GHDB");

                var options = new DbContextOptionsBuilder<GHDBContext>()
                  .UseNpgsql(connectionString)
                   .Options;

                var ghDBContext = new GHDBContext(options);
                SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
                SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
                SearchHistory searchHistory = new SearchHistory();
                searchHistory.Phrase = phrase;
                searchHistory.Id = iD;
                searchHistory.Email = email;

                //act 

                var result = await searchHistoryBL.GetSearchHistoriesAsync();

            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Phrase must not be null.", ex.Message);
            }


        }

        public async Task GetSearchHistoriesAsync_ShouldFail_WhenEmailIsNullAsync()
        {
            try
            {
                //arrange
                int iD = 1;
                string phrase = "Phrase";
                string email = null;
                var options = new DbContextOptionsBuilder<GHDBContext>()
                  .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                   .Options;

                var ghDBContext = new GHDBContext(options);
                SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
                SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
                SearchHistory searchHistory = new SearchHistory();
                searchHistory.Phrase = phrase;
                searchHistory.Id = iD;
                searchHistory.Email = email;

                //act 

                var result = await searchHistoryBL.GetSearchHistoriesAsync();

            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Email must not be null.", ex.Message);
            }


        }


        [TestMethod]
        public async Task GetLocationsAsync_ShouldFail_WhenStateIsNullAsync()
        {
            try
            {
                //arrange
                string state = null;
                string zipCode = "1234567";
                string city = "city";


                var options = new DbContextOptionsBuilder<GHDBContext>()
                .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                .Options;
                var ghDBContext = new GHDBContext(options);
                LocationRepoDB locationRepoDB = new LocationRepoDB(ghDBContext);
                LocationBL locationBL = new LocationBL(locationRepoDB);
                Location location = new Location();

                location.City = city;
                location.State = state;
                location.Zipcode = zipCode;

                //act 
                var result = await locationBL.GetLocationsAsync();
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("State cannot be null.", ex.Message);
            }

        }


        [TestMethod]
        public async Task GetLocationsAsync_ShouldReturnLocation_WhenCityStateZipIsNotNullAsync()
        {
            //arrange
            string state = "state";
            string zipCode = "1234567";
            string city = "city";


            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            var ghDBContext = new GHDBContext(options);
            LocationRepoDB locationRepoDB = new LocationRepoDB(ghDBContext);
            LocationBL locationBL = new LocationBL(locationRepoDB);
            Location location = new Location();

            location.City = city;
            location.State = state;
            location.Zipcode = zipCode;

            //act
            var result = await locationBL.GetLocationsAsync();


            //assert
            Assert.IsNotNull(location.City);
            Assert.IsNotNull(location.State);
            Assert.IsNotNull(location.Zipcode);
        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public async Task GetCharitiesAsync_ShouldFail_WhenStringNameIsNullAsync()
        {
            try
            {
                //arrange
                int ID = 1;
                string name = null;
                string missionStatement = "mission statement";
                string website = "www.website.com";
                string category = "category";
                string logourl = "www.logoURL.com";


                var options = new DbContextOptionsBuilder<GHDBContext>()
                .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                .Options;
                var ghDBContext = new GHDBContext(options);
                CharityRepoDB charityRepoDB = new CharityRepoDB(ghDBContext);
                CharityBL charityBL = new CharityBL(charityRepoDB);
                Charity charity = new Charity();

                charity.Category = category;
                charity.Id = ID;
                charity.Name = name;
                charity.Website = website;
                charity.Logourl = logourl;
                charity.Missionstatement = missionStatement;

                //act 
                var result = await charityBL.GetCharitiesAsync();
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Name must contain a value.", ex.Message);
            }


        }

        [TestMethod]
        public async Task GetCharitiesAsync_ShouldFail_WhenIDIsNullAsync()
        {
            //arrange
            int? ID = null;
            string name = "name";
            string missionStatement = "mission statement";
            string website = "www.website.com";
            string category = "category";
            string logourl = "www.logoURL.com";


            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            var ghDBContext = new GHDBContext(options);
            CharityRepoDB charityRepoDB = new CharityRepoDB(ghDBContext);
            CharityBL charityBL = new CharityBL(charityRepoDB);
            Charity charity = new Charity();

            charity.Category = category;
            charity.Id = (int)ID;
            charity.Name = name;
            charity.Website = website;
            charity.Logourl = logourl;
            charity.Missionstatement = missionStatement;

            //act
            var result = await charityBL.GetCharitiesAsync();

            //assert
           // Assert.IsTrue(result.Charities == null || !result.Charities.Any());
        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public async Task GetCharitiesAsync_ShouldSucceed_WhenFieldsAreNotNullAsync()
        {
            //arrange
            int ID = 1;
            string name = "name";
            string missionStatement = "mission statement";
            string website = "www.website.com";
            string category = "category";
            string logourl = "www.logoURL.com";

            var options = new DbContextOptionsBuilder<GHDBContext>()
           .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
           .Options;
            var ghDBContext = new GHDBContext(options);
            CharityRepoDB charityRepoDB = new CharityRepoDB(ghDBContext);
            CharityBL charityBL = new CharityBL(charityRepoDB);
            Charity charity = new Charity();

            charity.Category = category;
            charity.Id = ID;
            charity.Name = name;
            charity.Website = website;
            charity.Logourl = logourl;
            charity.Missionstatement = missionStatement;

            //act
            var result = await charityBL.GetCharitiesAsync();

            //assert
            Assert.IsNotNull(charity.Name);
            Assert.IsNotNull(charity.Missionstatement);
            Assert.IsNotNull(charity.Logourl);
            Assert.IsNotNull(charity.Website);
            Assert.IsNotNull(charity.Category);
            Assert.IsNotNull(charity.Id);

        }

        [TestMethod]
        public async Task GetSubscriptionsAsync_ShouldReturnNull_WhenEmailIsNullAsync()
        {

            try
            {
                //arrange
                int ID = 1;
                string email = null;


                var options = new DbContextOptionsBuilder<GHDBContext>()
                .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                .Options;
                var ghDBContext = new GHDBContext(options);
                SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
                SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
                Subscription subscription = new Subscription();

                subscription.Id = ID;
                subscription.Email = email;

                //act
                var result = await subscriptionBL.GetSubscriptionsAsync();
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.IsNotNull("Email must not be null.", ex.Message);
            }

        }


        [TestMethod]
        public async Task GetSubscriptionsAsync_ShouldReturnNull_WhenIDIsNullAsync()
        {


            //arrange
            int? ID = null;
            string email = "email@email.com";


            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            var ghDBContext = new GHDBContext(options);
            SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
            SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
            Subscription subscription = new Subscription();

            subscription.Id = (int)ID;
            subscription.Email = email;

            //act
            var result = await subscriptionBL.GetSubscriptionsAsync();



            //assert
            Assert.AreEqual(ID, subscription.Id);


        }


        [TestMethod]
        public async Task GetSubscriptionsAsync_ShouldSucceed_WhenIDandEmailIsNotNull()
        {


            //arrange
            int ID = 1;
            string email = "email@email.com";


            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            var ghDBContext = new GHDBContext(options);
            SubscriptionRepoDB subscriptionRepoDB = new SubscriptionRepoDB(ghDBContext);
            SubscriptionBL subscriptionBL = new SubscriptionBL(subscriptionRepoDB);
            Subscription subscription = new Subscription();

            subscription.Id = ID;
            subscription.Email = email;

            //act
            var result = await subscriptionBL.GetSubscriptionsAsync();



            //assert
            Assert.AreEqual(ID, subscription.Id);
            Assert.AreEqual(email, subscription.Email);


        }





        /***********************************************************************************
       
        ************************************************************************************/

        //[TestMethod]
        //public void CreateSubscription_ShouldReturnNull_WhenFirstNameIsNull()
        //{
        //    //arrange

        //    string firstName = null;
        //    string lastName = "lastname";
        //    string email = "123@mail.com";
        //    int locationID = 1;
        //    var options = new DbContextOptionsBuilder<GHDBContext>()
        //    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
        //    .Options;

        //    //act
        //    //assert

        //}




        //[TestMethod]
        //public void CreateSubscription_ShouldReturnNull_WhenLastNameIsNull()
        //{
        //    //arrange
        //    string firstName = "firstname";
        //    string lastName = null;
        //    string email = "123@mail.com";
        //    int locationID = 1;

        //    var options = new DbContextOptionsBuilder<GHDBContext>()
        //    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
        //    .Options;
        //    //act
        //    //assert
        //}





        //[TestMethod]
        //public void CreateSubscription_IsTrue_WhenFieldsAreNotNull() 
        //{ }









    }

}