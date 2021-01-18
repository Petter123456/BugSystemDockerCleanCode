//using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugsFrontend.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Configuration;
using Moq;
using FluentAssertions;
using BugsFrontend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BugsFrontend.Models;
using BugsFrontendTests;
using Xunit;
using AutoFixture.Xunit2;
using AutoFixture;
using NSubstitute;
using Newtonsoft.Json;
using System.Net;

namespace BugsFrontend.Pages.Tests
{
    public class BugsApiRequestTests : BugsApiRequestMockData
    {

        [Theory]
        [AutoNSubstituteData]
        public void GetBugsAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network ([Frozen] BugsApiRequest sut)
        {
            //Act
            var actual = sut.GetBugsAsync();

            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void DeleteBugAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut)
        {
            //Act
            var actual = sut.DeleteBugAsync(1);
            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void UpdateBugAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut)
        {
            //Arrange

            //Act
            var actual = sut.UpdateBugAsync(1, "no product");
            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void CreateBugAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut)
        {
            //Arrange

            //Act
            var actual = sut.CreateBugAsync("no product");
            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void GetBugsAsync_should_excecute_properly_if_correct_response_from_api()
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.GetBugsAsync()).Returns(GetBugsAsync());
            //act
            var actual = request.Object.GetBugsAsync();
            //Assert
            actual.Result.Any(x => x.Id == 1);
            actual.Result.Any(x => x.Name.Equals("no product"));
        }
    }
}