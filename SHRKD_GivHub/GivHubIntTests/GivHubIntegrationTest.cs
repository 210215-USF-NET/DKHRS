using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GivHubModels;
using Microsoft.EntityFrameworkCore;
using GivHubDL;

namespace GivHubTests
{
    [TestClass]
    public class GivHubIntegrationTest
    {

        [TestMethod]
        public void SendDonation_ShouldFail_WhenAmountIsZero()
        {   //arrange 

            var options = new DbContextOptionsBuilder<GHDBContext>()
                .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
                .Options;
            var ghDBContext = new GHDBContext(options);

            decimal amount = 0m;
            int? userID = null;
            int? charityID = null;

            Donation donation = new Donation();

            //            var options = new DbContextOptionsBuilder<DrinkDBContext>()
            //     .UseNpgsql("Host = ziggy.db.elephantsql.com; Port = 5432; Database = diijqqsl; Username = diijqqsl; Password = 95ILlqxg9G1qwYYI4V8ZlFe7lh2z499K;")
            //		.Options;

            //act
            var result = donation.Amount;


            //assert
           // Assert.IsTrue(donation.Amount.Any());
        }

        [TestMethod]
        public void SendDonation_ShouldFail_WhenAmountIsNotDecimal()
        {   //arrange


            decimal? amount = null;
            int? userID = null;
            int? charityID = null;
            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            Donation donation = new Donation();



            //WILL THIS EVEN PASS?
        }

        [TestMethod]
        public void SendDonation_ShouldSucceed_WhenDonationIsGreaterThanZero()
        {
            //arrange
            decimal amount = 1m;
            int? userID = null;
            int? charityID = null;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            Donation donation = new Donation();

        }

        [TestMethod]
        public void GetSearchHistory_ShouldSucceed_WhenPhraseIsNotNull()
        {

            //arrange
            int? userID = null;
            string phrase = "Phrase";

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            SearchHistory searchHistory = new SearchHistory();

            //act
            //assert
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        //
        public void GetSearchHistory_ShouldFail_WhenPhraseIsNull()
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





        [TestMethod]
        public void CreateAccount_ShouldCreate_WhenFirstNameIsNotNull()
        {
            //arrange
            string firstName = "firstName";
            string lastName = null;
            string email = null;
            int locationID = 1;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //is it worth it to test locationID = null;?	

            //act
            
            //assert
        }

        [TestMethod]
        public void CreateAccount_ShouldCreate_WhenLastNameIsNotNull()
        {
            //arrange
            string firstName = null;
            string lastName = "lastName";
            string email = null;
            int locationID = 1;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //is it worth it to test locationID = null;?	

            //act
            //assert
        }

        [TestMethod]
        public void CreateAccount_ShouldCreate_WhenEmailIsNotNull()
        {
            //arrange
            string firstName = "firstName";
            string lastName = null;
            string email = null;
            int locationID = 1;

            var options = new DbContextOptionsBuilder<GHDBContext>()
            .UseNpgsql("Host = queenie.db.elephantsql.com; Port=5432; Database=ufmbvrid; Username=ufmbvrid; Password=5wlreA7C8L5JcTuYANpZLj8rlHNFd1SQ;")
            .Options;

            //is it worth it to test locationID = null;?	

            //act
            //assert
        }

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