namespace Graph.Community.Test
{
  internal class ResourceManager
  {
    internal static string GetHttpResponseContent(string responseSourceFilename)
    {
      var resourcePath = @"Mocks\" + responseSourceFilename;

      var contentString = GetEmbeddedResource(resourcePath);
      return contentString;
    }

#pragma warning disable CA1307 // Specify StringComparison
    private static string GetEmbeddedResource(string resourcePath)
    {
      var baseNamespace = "Graph.Community.Test";
      var resourceName = $"{baseNamespace}.{resourcePath.Replace("\\", ".").Replace("/", ".")}";
      var _assembly = System.Reflection.Assembly.GetExecutingAssembly();
      var _textStreamReader = new System.IO.StreamReader(_assembly.GetManifestResourceStream(resourceName));
      return _textStreamReader.ReadToEnd();
    }
#pragma warning restore CA1307 // Specify StringComparison
  }
}
