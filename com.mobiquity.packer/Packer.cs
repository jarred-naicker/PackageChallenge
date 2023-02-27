using com.mobiquity.packer.Utils;

namespace com.mobiquity.packer;

public class Packer
{
    /// <summary>
    ///     Takes a file path and returns a string of comma-separated package results. Each
    ///     package result is a string of integers, representing the indexes of the items packed
    ///     in the package.
    /// </summary>
    /// <param name="filePath">The path to the file to be processed.</param>
    /// <returns>A string of comma-separated package results.</returns>
    public string pack(string filePath)
    {
        // Call the asynchronous method in FileProcessorUtil to convert the file contents
        // to package objects. The method returns a Task that will contain the packages
        // when it completes.
        var fileToPackages = FileProcessorUtil.FileToPackagesAsync(filePath);

        // Wait for the task to complete and retrieve the result, which is the list of
        // package objects.
        fileToPackages.Wait();
        var packages = fileToPackages.Result;

        // Iterate through each package and pack its items. Then, add the package's
        // packaged indexes result to a list of package results.
        var packageResults = new List<string>();
        foreach (var package in packages)
        {
            package.PackItems();
            //package.PackItemsAlternative();

            packageResults.Add(package.GetPackagedIndexesResult());
        }

        // Return a string of comma-separated package results.
        return string.Join("\n", packageResults);
    }
}