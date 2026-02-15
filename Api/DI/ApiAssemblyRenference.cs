using System.Reflection;

namespace Api.DI
{
    public class ApiAssemblyReference
    {
        internal static readonly Assembly Assembly = typeof(ApiAssemblyReference).Assembly;
    }
}