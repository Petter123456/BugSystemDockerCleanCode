﻿using BugsApi.Data;
using BugsApi.Models;
using BugsApi.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugsApi.Controllers.Tests
{
    [TestClass()]
    public class BugApiTests : BugApiTestsMock
    {
        [TestMethod()]
        public void Get_Should_not_return_null()
        {
            //Arrange     
            var repository = new Mock<IBugsRepository>();

            repository.Setup(x => x.Get()).Returns(Get());

            //Act
            var actual = repository.Object.Get();

            //Assert
            actual.Should().NotBeNull();
        }

        [TestMethod()]
        public void Update_Should_return_Succesfull_if_bug_exists()
        {
            //Arrange    
            var bug = new BugModel { Id = 1, Name = "no product" };
            var repository = new Mock<IBugsRepository>();

            repository.Setup(x => x.Update(1, bug)).Returns(Update(1, bug));

            //Act
            var actual = repository.Object.Update(1, bug);

            //Assert
            actual.IsCompletedSuccessfully.Should().BeTrue(); 
        }


        [TestMethod()]
        public void Update_Should_return_Null_if_bug_does_not_exists()
        {
            //Arrange    
            var bug = new BugModel { Id = 2, Name = "no product" };
            var repository = new Mock<IBugsRepository>();

            repository.Setup(x => x.Update(2, bug)).Returns(Update(2, bug));

            //Act
            var actual = repository.Object.Update(2, bug);

            //Assert
            actual.Result.Should().BeNull();
        }

        [TestMethod()]
        public void Delete_Should_return_Succesfull_if_bug_does_exists()
        {
            //Arrange    
            var repository = new Mock<IBugsRepository>();

            repository.Setup(x => x.Delete(2)).Returns(Delete(2));

            //Act
            var actual = repository.Object.Delete(2);

            //Assert
            actual.IsCompletedSuccessfully.Should().BeTrue();
        }

        [TestMethod()]
        public void Delete_Should_return_Null_if_bug_does_not_exists()
        {
            //Arrange    
            var repository = new Mock<IBugsRepository>();

            repository.Setup(x => x.Delete(5)).Returns(Delete(5));

            //Act
            var actual = repository.Object.Delete(5);

            //Assert
            actual.Result.Should().BeNull();
        }

        public void Create_Should_return_Succesfull_if_bug_is_entered_Correctly()
        {
            //Arrange    
            var repository = new Mock<IBugsRepository>();
            var bug = new BugModel { Id = 1, Name = "No Images" }; 

            repository.Setup(x => x.Create(bug)).Returns(Create(bug));

            //Act
            var actual = repository.Object.Create(bug);

            //Assert
            actual.Should().Be(true);
        }

        [TestMethod()]
        public void Create_Should_return_Null_if_bug_does_is_null()
        {
            //Arrange    
            var repository = new Mock<IBugsRepository>();
            BugModel bug = null;

            repository.Setup(x => x.Create(bug)).Returns(Create(bug));

            //Act
            var actual = repository.Object.Create(bug);

            //Assert
            actual.Result.Should().BeNull();
        }
    }
}