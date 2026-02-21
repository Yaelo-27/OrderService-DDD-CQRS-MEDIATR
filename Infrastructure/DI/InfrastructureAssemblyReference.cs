using System.Reflection;

namespace Infrastructure.DI;
internal class InfrastructureAssemblyReference
{
    internal static readonly Assembly assembly = typeof(InfrastructureAssemblyReference).Assembly;
}