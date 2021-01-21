using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using BugsFrontend.Interfaces;
using BugsFrontend.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BugsFrontendTests
{
    public class BugsApiRequestTests : BugsApiRequestMockData
    {
        [Theory]
        [AutoNSubstituteData]
        public void GetBugsAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut)
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
        public void UpdateBugAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut, IFixture fixture)
        {
            //Act
            var actual = sut.UpdateBugAsync(1, fixture.Create<string>());
            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void CreateBugAsync_Should_not_beCompleted_Succesfully_since_app_is_not_upp_on_docker_network([Frozen] BugsApiRequest sut, IFixture fixture)
        {
            //Act
            var actual = sut.CreateBugAsync(fixture.Create<string>());
            //Assert
            actual.IsCompletedSuccessfully.Should().BeFalse();
        }

        [Theory]
        [AutoNSubstituteData]
        public void GetBugsAsync_should_excecute_properly_if_correct_response_from_api(IFixture fixture)
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.GetBugsAsync()).Returns(GetBugsAsync());
            //act
            var actual = request.Object.GetBugsAsync();
            //Assert
            actual.Result.Any(x => x.Id == 1);
            actual.Result.Any(x => x.Name.Equals(fixture.Create<string>()));
        }

        [Theory]
        [AutoNSubstituteData]
        public void CreateBugsAsync_should_not_throw_exception_if_correct_response_from_api(IFixture fixture, BugsApiRequest sut, BugModel bugModel)
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.CreateBugAsync(fixture.Create<string>())).Returns(CreateBugAsync(fixture.Create<string>()));
            //Act
            Action actual = () => sut.CreateBugAsync(fixture.Create<string>());
            //Assert
            actual.Should().NotThrow();
        }

        [Theory]
        [AutoNSubstituteData]
        public void DeleteBugsAsync_should_be_faulted_if_Incorrect_response_from_api(IFixture fixture, BugsApiRequest sut)
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.DeleteBugAsync(1)).Returns(DeleteBugAsync(1));
            //Act
            var actual = sut.CreateBugAsync(fixture.Create<string>());
            //Assert
            actual.Status.Should().Be(TaskStatus.Faulted);
        }

        [Theory]
        [AutoNSubstituteData]
        public void UpdateBugsAsync_should_be_faulted_if_Incorrect_response_from_api(IFixture fixture, BugsApiRequest sut)
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.UpdateBugAsync(fixture.Create<int>(), fixture.Create<string>())).Returns(UpdateBugAsync(fixture.Create<int>(), fixture.Create<string>()));
            //Act
            var actual = sut.UpdateBugAsync(fixture.Create<int>(), fixture.Create<string>());
            //Assert
            actual.Status.Should().Be(TaskStatus.Faulted);
        }

        [Theory]
        [AutoNSubstituteData]
        public void CreateBugsAsync_should_throw_exception_if_Incorrect_response_from_api(IFixture fixture, BugsApiRequest sut)
        {
            //Arrange
            var request = new Mock<IBugsApiRequest>();
            request.Setup(x => x.CreateBugAsync(fixture.Create<string>())).Returns(CreateBugAsync(fixture.Create<string>()));
            //Act
            var actual = sut.CreateBugAsync(fixture.Create<string>());
            //Assert
            actual.Status.Should().Be(TaskStatus.Faulted);
        }
    }
}