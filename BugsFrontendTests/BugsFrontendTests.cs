using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace BugsFrontend.Pages.Tests
{
    //[TestClass()]
    //public class BugsFrontendTests
    //{
    //    [TestMethod()]
    //    public void If_no_id_matches_db_when_calling_delete_throw_exception()
    //    {
    //        //Arrange
    //        var configuration = new Mock<IConfiguration>();

    //        HttpClient http = new HttpClient(); 
    //        var sut = new BugViewModel((IConfiguration)configuration, http);
    //        //act
    //        var actual = sut.OnGetDelete(1);

    //        //Assert
    //        actual.Should().NotBeNull();
    //    }
    //}
}