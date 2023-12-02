using System.Reflection;

namespace ArchTests
{
    [TestFixture]
    public class ValidateInterfaceImplementationsTest
    {
        [Test]
        public void Validate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var interfaceTypes = assembly.GetTypes().Where(type => type.IsInterface).ToList();

            foreach (var interfaceType in interfaceTypes)
            {
                var implementingClasses = assembly.GetTypes().Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface);

                foreach (var implementingClass in implementingClasses)
                {
                    var interfaceMethods = interfaceType.GetMethods().Select(method => method.Name).ToList();
                    var classMethods = implementingClass.GetMethods().Select(method => method.Name).ToList();

                    var missingMethods = interfaceMethods.Except(classMethods).ToList();

                    Assert.IsEmpty(missingMethods, $"Class {implementingClass.Name} is missing implementations for the following methods from interface {interfaceType.Name}: {string.Join(", ", missingMethods)}");
                }
            }
        }
    }
}
