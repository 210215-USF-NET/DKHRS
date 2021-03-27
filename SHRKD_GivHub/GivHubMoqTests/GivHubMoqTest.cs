
using GivHubBL;
using GivHubDL;
using GivHubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SHRKD_GivHub.Controllers;
using System;
using System.Collections.Generic;
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
    
        [TestMethod]
        public async Task AddCharityAsync_ShouldReturnCreatedAtActionResult_WhenCharityIsValid()
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = new Charity();
            charityBLMock.Setup(i => i.AddCharityAsync(charity)).ReturnsAsync(charity);
            var charityController = new CharityController(charityBLMock.Object);

            //act
            var result = await charityController.AddCharityAsync(charity);

            //assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
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
        public async Task DeleteCharityAsync_ShouldReturnNoContentWhenCharityIDIsValid(int id) ///
        {
            //arrange
            var charityBLMock = new Mock<ICharityBL>();
            Charity charity = new Charity();
  
           // charityBLMock.Setup(i => i.DeleteCharityAsync(charity))
             //            .Returns(NoContentResult);
            var charityController = new CharityController(charityBLMock.Object);
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

    }
}