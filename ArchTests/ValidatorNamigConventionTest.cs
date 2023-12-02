using NetArchTest.Rules;
using OnlineLibary.Modules.Catalog.Application.Configuration.Commands;
using OnlineLibary.Modules.Catalog.Application.Configuration.Queries;
using OnlineLibrary.Modules.UserAccess.Application.ChangeUsername;

namespace ArchTests
{
    [TestFixture]
    public class ValidatorNamingConventionTest
    {
        [Test]
        public void Command_And_Query_Handlers_Should_Not_Be_Public()
        {
            var types = Types.InAssembly(typeof(ChangeUsernameCommand).Assembly)
                .That()
                    .ImplementInterface(typeof(IQueryHandler<,>))
                        .Or()
                    .ImplementInterface(typeof(ICommandHandler<>))
                        .Or()
                    .ImplementInterface(typeof(ICommandHandler<,>))
                .Should().NotBePublic().GetResult().FailingTypes;

            Assert.That(types, Is.Null.Or.Empty);

        }
        
    }
}