//using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;



namespace GivHubTests
{
    [TestClass]
    public class GivHubIntegrationTest
    {
        /*
        
         */
        [TestMethod]
        public void SendDonation_ShouldFail_WhenAmountIsZero()
        {   
        }

        [TestMethod]
      
        public void GetSearchHistory_ShouldFail_WhenPhraseIsNull()
        {
            


        }

        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenFirstNameIsNull()
        {
           

        }

        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenLastNameIsNull()
        {
           
        }

        [TestMethod]
        public void CreateAccount_ShouldReturnNull_WhenEmailIsNull()
        {
      
        }

        /***********************************************************************************
       
        ************************************************************************************/



        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenStateIsNull()
        {
          
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenCityIsNull()
        {
            
        }

        [TestMethod]
        public void GetCharityLocation_ShouldFail_WhenZipCodeIsNull()
        {
           
        }


    }

}
