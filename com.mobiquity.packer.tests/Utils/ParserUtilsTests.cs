using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Utils;

namespace com.mobiquity.packer.tests.Utils;

[TestFixture]
public class ParserUtilsTests
{
    [Test]
    public void ParseToDecimal_ShouldReturnDecimalValue_WhenInputIsValid()
    {
        // Arrange
        const string input = "123.45";

        // Act
        var result = ParserUtils.ParseToDecimal(input);

        // Assert
        Assert.AreEqual(123.45m, result);
    }

    [Test]
    public void ParseToDecimal_ShouldThrowAPIException_WhenInputIsInvalid()
    {
        // Arrange
        const string input = "invalid";

        // Act and Assert
        Assert.Throws<APIException>(() => ParserUtils.ParseToDecimal(input));
    }

    [Test]
    public void ParseToInt_ShouldReturnIntValue_WhenInputIsValid()
    {
        // Arrange
        const string input = "42";

        // Act
        var result = ParserUtils.ParseToInt(input);

        // Assert
        Assert.AreEqual(42, result);
    }

    [Test]
    public void ParseToInt_ShouldThrowAPIException_WhenInputIsInvalid()
    {
        // Arrange
        const string input = "invalid";

        // Act and Assert
        Assert.Throws<APIException>(() => ParserUtils.ParseToInt(input));
    }
}