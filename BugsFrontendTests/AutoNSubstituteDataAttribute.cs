using System;
using AutoFixture;
using AutoFixture.Xunit2;

namespace BugsFrontendTests
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute(Type dataCustomizationType)
            : this(fixture => fixture.Customize((ICustomization)Activator.CreateInstance(dataCustomizationType)))
        {
        }

        protected AutoNSubstituteDataAttribute(Action<IFixture> fixtureAction) : base(() =>
        {
            var fixture = new Fixture();
            fixture
            .Customize(new BugsApiRequestCustomization());

            fixtureAction(fixture);
            return fixture; 
        })
        { 
        }

        public AutoNSubstituteDataAttribute() : this(Fixture => { }) { 
        }
    }
}