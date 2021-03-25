
using GivHubBL;
using GivHubDL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;



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
        public async System.Threading.Tasks.Task GetDonationsASync_ShouldFail_WhenAmountIsZeroAsync()
        {
            //arrange
            var donationRepoMoq = new Mock<DonationRepoDB>();
            DonationBL donationBL = new DonationBL(donationRepoMoq.Object);
            decimal amount = 0m;


            //act
            var result = await donationBL.GetDonationsAsync();

            //assert
            Assert.
        }

    }
}