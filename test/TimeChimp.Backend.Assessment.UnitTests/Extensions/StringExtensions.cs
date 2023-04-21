using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace TimeChimp.Backend.Assessment.UnitTests.Extensions
{
    public static class StringExtensions
    {
        private static string GetResourcePath(Assembly assembly, string resourceName)
        {
            var stringBuilder = new StringBuilder($"{assembly.GetName().Name}.Resources");

            stringBuilder.Append($".{resourceName}");
            var path = stringBuilder.ToString();

            return path;
        }

        public static T AsObject<T>(this string resourceName, bool pathByMethod = false) where T : new()
        {
            var assembly = Assembly.GetCallingAssembly();

            if (pathByMethod)
            {
                var frame = new StackFrame(3); //Due to async handler skip last 3 frames
                var method = frame.GetMethod();
                var type = method.DeclaringType;

                resourceName = $"{type.Name}.{method.Name}.{resourceName}";
            }

            //Use calling assembly from entry method; can be a different project / assembly
            string path = GetResourcePath(assembly, resourceName);

            using (var stream = assembly.GetManifestResourceStream(path))
            using (var streamReader = new StreamReader(stream))
            {
                var content = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
