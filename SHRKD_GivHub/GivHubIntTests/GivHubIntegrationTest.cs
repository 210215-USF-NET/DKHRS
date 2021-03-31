using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GivHubTests
{
    [TestClass]
    public class GivHubIntegrationTest
    {
        /*
         * locationorders depends on locationID , nullable int
                make sure it returns no orders  when the id is null
         */
        [TestMethod]
        public void SendDonation_ShouldFail_WhenAmountIsZero()
        {   //arrange 
            decimal amount = 0m;
            int? userID = null;
            int? charityID = null;

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

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenCityIsNull()
        {
            //arrange
            string city = null;
            int? charityID = null;

            //act
            //assert
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenZipCodeIsNull()
        {
            //arrange
            string zipCode = null;
            int? charityID = null;

            //act
            //assert
        }


    }

}
