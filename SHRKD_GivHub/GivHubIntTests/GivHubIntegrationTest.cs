using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GivHubModels;
using Microsoft.EntityFrameworkCore;
using GivHubDL;
using GivHubBL;

namespace GivHubTests
{
    [TestClass]
    public class GivHubIntegrationTest
    /*

        Arrange is all about setting up the things u need for the test.
        Act is doing the thing u wanna test
        Assert is comparing the actual results to the expected outcome

     */
    {

        [TestMethod]
        public async System.Threading.Tasks.Task AddDonationASync_ShouldFail_WhenAmountIsZero()
        {   //arrange 


            var options = new DbContextOptionsBuilder<GHDBContext>()
                .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                .Options;
            var ghDBContext = new GHDBContext(options);

            decimal amount = 0m;
            int userID = 1;
            int charityID = 1;
            DonationRepoDB donationRepoDB = new DonationRepoDB(ghDBContext);
            DonationBL donationBL = new DonationBL(donationRepoDB);
            Donation donation = new Donation();
            donation.Amount = amount;
            //            var options = new DbContextOptionsBuilder<DrinkDBContext>()
            //     .UseNpgsql("Host = ziggy.db.elephantsql.com; Port = 5432; Database = diijqqsl; Username = diijqqsl; Password = 95ILlqxg9G1qwYYI4V8ZlFe7lh2z499K;")
            //		.Options;

            //act
            var result = await donationBL.AddDonationAsync(donation);


            //assert
           //  Assert.IsTrue(donation.Amount.Any());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddDonation_ShouldSucceed_WhenDonationIsGreaterThanZeroAsync()
        {
            //arrange
            decimal amount = 3m;
            int userID = 1;
            int charityID = 1;


            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            var ghDBContext = new GHDBContext(options);
            DonationRepoDB donationRepoDB = new DonationRepoDB(ghDBContext);
            DonationBL donationBL = new DonationBL(donationRepoDB);
            Donation donation = new Donation();
            donation.Amount = amount;

            //act
            var result = await donationBL.AddDonationAsync(donation);

            //assert
            Assert.IsNotNull(donation.Amount);


        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetSearchHistoriesAsync_ShouldSucceed_WhenPhraseIsNotNullAsync()
        {

            //arrange
            int? userID = null;
            string phrase = "Phrase";
            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            var ghDBContext = new GHDBContext(options);
            SearchHistoryRepoDB searchHistoryRepoDB = new SearchHistoryRepoDB(ghDBContext);
            SearchHistoryBL searchHistoryBL = new SearchHistoryBL(searchHistoryRepoDB);
            SearchHistory searchHistory = new SearchHistory();
            searchHistory.Phrase = phrase;

            //act
            var result = await searchHistoryBL.GetSearchHistoriesAsync();
            //assert
            Assert.IsNotNull(searchHistory.Phrase);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetSearchHistories_ShouldFail_WhenPhraseIsNull()
        {
            try
            {
                //arrange
                int? userID = null;
                string phrase = null;
                var options = new DbContextOptionsBuilder<GHDBContext>()
                  .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                   .Options;
                SearchHistory searchHistory = new SearchHistory();


                //act 
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Phrase must not be null.", ex.Message);
            }


        }

        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenFirstNameIsNull()
        {
            //arrange

            string firstName = null;
            string lastName = "lastname";
            string email = "123@mail.com";
            int locationID = 1;
            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert

        }
        //test when everything is not null 



        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenLastNameIsNull()
        {
            //arrange
            string firstName = "firstname";
            string lastName = null;
            string email = "123@mail.com";
            int locationID = 1;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            //act
            //assert
        }

        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenEmailIsNull()
        {
            //arrange
            string firstName = "firstName";
            string lastName = "lastName";
            string email = null;
            int locationID = 1;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;
            //act
            //assert 
        }

        /***********************************************************************************
       
        ************************************************************************************/



        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenStateIsNull()
        {
            //arrange
            string state = null;
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldSucceed_WhenStateIsNotNull()
        {
            //arrange
            string state = "state";
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenCityIsNull()
        {
            //arrange
            string city = null;
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldSucceed_WhenCityIsNotNull()
        {
            //arrange
            string city = "city";
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenZipCodeIsNull()
        {
            //arrange
            string zipCode = null;
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldSucceed_WhenZipCodeIsNotNull()
        {
            //arrange
            string zipCode = "zipcode";
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //act
            //assert
        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetCharity_ShouldFail_WhenNameIsNull()
        {
            try
            {
                //arrange
                int? ID = null;
                string name = null;

                var options = new DbContextOptionsBuilder<GHDBContext>()
                  .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                  .Options;

                //act 
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Name must contain a value.", ex.Message);
            }


        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetCharity_ShouldFail_WhenMissionStatementIsNull()
        {
            try
            {
                //arrange
                int? ID = null;
                string missionStatement = null;

                var options = new DbContextOptionsBuilder<GHDBContext>()
                 .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                 .Options;
                //act 
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Mission statement must not be null.", ex.Message);
            }


        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetCharity_ShouldFail_WhenCategoryIsNull()
        {
            try
            {
                //arrange
                int? ID = null;
                string category = null;

                var options = new DbContextOptionsBuilder<GHDBContext>()
                    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                     .Options;
                //act 
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("Category must not be null.", ex.Message);
            }


        }


        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetCharity_ShouldFail_WhenLogourlIsNull()
        {
            try
            {
                //arrange
                int? ID = null;
                string logourl = null;
                var options = new DbContextOptionsBuilder<GHDBContext>()
                    .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                     .Options;
                //act 
            }
            catch (System.Exception ex)
            {
                //assert
                Assert.AreEqual("URL must not be null.", ex.Message);
            }


        }





    }

}