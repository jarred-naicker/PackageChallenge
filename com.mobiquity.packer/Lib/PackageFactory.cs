using System.Text.RegularExpressions;
using com.mobiquity.packer.Entities;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Utils;

namespace com.mobiquity.packer.Lib;

/// <summary>
///     Factory class that creates a new Package object from a string input following a specific format.
/// </summary>
public class PackageFactory
{
    /// <summary>
    ///     Creates a new Package object from a string input.
    /// </summary>
    /// <param name="input">String input representing a package and its items.</param>
    /// <returns>A new Package object.</returns>
    /// <exception cref="APIException">
    ///     Thrown if the package capacity or package items cannot be determined from the
    ///     input.
    /// </exception>
    public static Package CreatePackage(string input)
    {
        var package = new Package();

        // Extract integer before the ":" character - this is the Package Capacity
        var capacityMatch = Regex.Match(input, @"^\d+(?=\s*:)");

        if (!capacityMatch.Success)
            throw new APIException($"Unable to determine capacity for: {input}");

        package.Capacity = ParserUtils.ParseToInt(capacityMatch.Value);

        // Extract numbers enclosed in round brackets - this is the PackageItem data
        var packageItemMatch = Regex.Matches(input, @"\((\d+),(\d+\.\d+),€(\d+)\)");

        if (packageItemMatch.Count == 0 || packageItemMatch.Any(_ => !_.Success))
            throw new APIException($"Unable to determine package items for: {input}");

        foreach (Match match in packageItemMatch)
        {
            var groups = match.Groups;
            var index = ParserUtils.ParseToInt(groups[1].Value);
            var weight = ParserUtils.ParseToDecimal(groups[2].Value);
            var cost = ParserUtils.ParseToDecimal(groups[3].Value);
            package.Items.Add(new PackageItem(index, weight, cost));
        }

        return package;
    }
}