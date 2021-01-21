using AutoFixture;
using BugsFrontend.Interfaces;
using BugsFrontend.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using AutoFixture.AutoMoq;
using Moq;

namespace BugsFrontendTests
{
    public class BugsApiRequestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            IConfiguration configRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "BugClientUrl", "http://localhost:80/bugsapi/" },
                    { "Loggers:0", "1" },
                    { "Loggers:1", "2" },
                    { "Loggers:2", "3" }
                })
                .Build();

            var autoMockCuztomisation = fixture.Customize(new AutoMoqCustomization());
            var bug = fixture.Build<BugModel>().CreateMany();
            var listOfBugs = fixture.Build<List<BugModel>>().Create();
            var messageHandler = new Mock<HttpMessageHandler>();    
            var httpClient = new HttpClient(messageHandler.Object); 
            var bugApiRequest = new BugsApiRequest(configRoot, httpClient);
            
            fixture.Register(() => autoMockCuztomisation);
            fixture.Register(() => bug);
            fixture.Register(() => configRoot);
            fixture.Register(() => listOfBugs);
            fixture.Register(() => httpClient);
            fixture.Register(() => bugApiRequest);
        }
    }
}
