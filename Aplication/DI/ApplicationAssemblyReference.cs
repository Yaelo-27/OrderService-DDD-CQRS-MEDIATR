using System.Reflection;

namespace Aplication.DI
{
    // This class is responsible for registering the application's services and dependencies.
    public class ApplicationAssemblyReference
    {
        // This is a marker class used to reference the assembly where the application's services are defined.
        internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
    }
}