using System.Text;
using com.mobiquity.packer.Entities;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Lib;

namespace com.mobiquity.packer.Utils;

public static class FileProcessorUtil
{
    /// <summary>
    ///     Processes the lines of a file asynchronously.
    /// </summary>
    /// <param name="filePath">The path of the file to process.</param>
    /// <param name="lineProcessor">The function to call for each line of the file.</param>
    public static async Task ProcessFileAsync(string filePath, Func<string, ValueTask> lineProcessor)
    {
        try
        {
            // Open the file stream and reader using the UTF8 encoding.
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(fileStream, Encoding.UTF8);

            string line;
            // Read each line asynchronously and call the lineProcessor function.
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                await lineProcessor(line).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new APIException($"Error processing file '{filePath}'", ex);
        }
    }

    /// <summary>
    ///     Reads a file and converts each line into a <see cref="Package" /> object asynchronously.
    /// </summary>
    /// <param name="filePath">The path of the file to read.</param>
    /// <returns>A list of <see cref="Package" /> objects.</returns>
    public static async Task<List<Package>> FileToPackagesAsync(string filePath)
    {
        // Initialize the packages list with an initial capacity to avoid unnecessary memory reallocation.
        // As per constraint: "There might be up to 15 items you need to choose from". An exception will be thrown if there are more
        const int maxPackages = 15;
        var packages = new List<Package>(maxPackages);

        // Process each line of the file asynchronously and create a Package object from it.
        async ValueTask ProcessLine(string line)
        {
            try
            {
                var package = PackageFactory.CreatePackage(line);
                packages.Add(package);
            }
            catch (Exception ex)
            {
                throw new APIException($"Error creating package from line: '{line}'", ex);
            }
        }

        // Use the ProcessFileAsync method to read the lines from the file and create Package objects.
        await ProcessFileAsync(filePath, ProcessLine);

        return packages;
    }
}