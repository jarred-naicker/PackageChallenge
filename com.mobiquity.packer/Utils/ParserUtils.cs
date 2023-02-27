using System.Globalization;
using com.mobiquity.packer.Exceptions;

namespace com.mobiquity.packer.Utils;

public static class ParserUtils
{
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    /// <summary>
    ///     Parses a string to decimal, using the InvariantCulture.
    /// </summary>
    /// <param name="input">String to parse.</param>
    /// <returns>The decimal value if successful, otherwise it throws an APIException</returns>
    public static decimal ParseToDecimal(string input)
    {
        if (decimal.TryParse(input, NumberStyles.Any, InvariantCulture, out var result))
            return result;
        throw new APIException($"Unable to convert string to decimal: {input}");
    }

    /// <summary>
    ///     Parses a string to integer, using the InvariantCulture.
    /// </summary>
    /// <param name="input">String to parse.</param>
    /// <returns>The integer value if successful, otherwise it throws an APIException</returns>
    public static int ParseToInt(string input)
    {
        if (int.TryParse(input, NumberStyles.Any, InvariantCulture, out var result))
            return result;
        throw new APIException($"Unable to convert string to integer: {input}");
    }
}