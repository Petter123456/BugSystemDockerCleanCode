using AutoFixture;
using AutoFixture.Kernel;
using BugsFrontend.Interfaces;
using BugsFrontend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BugsFrontendTests
{
    public class BugsApiRequestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var bug = fixture.Build<BugModel>().CreateMany();
            var listOfBugs = fixture.Build<List<BugModel>>().Create();


            IConfiguration configRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "BugClientUrl", "http://localhost:80/bugsapi/" },
                    { "Loggers:0", "1" },
                    { "Loggers:1", "2" },
                    { "Loggers:2", "3" }
                })
                .Build();

            var x = new BugsApiRequest(configRoot, new HttpClient()); 

            fixture.Register(() => bug);
            fixture.Register(() => configRoot);
            fixture.Register(() => x);
            fixture.Register(() => listOfBugs);


        }
    }

    internal class YourConcreteImplementation
    {
    }
}
