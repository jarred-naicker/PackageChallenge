using com.mobiquity.packer.Exceptions;

namespace com.mobiquity.packer.tests.Exceptions;

[TestFixture]
public class APIExceptionTests
{
    [Test]
    public void TestConstructor()
    {
        // Arrange
        const string message = "Test exception message";
        var innerException = new Exception("Inner exception message");

        // Act
        var apiException = new APIException(message, innerException);

        // Assert
        Assert.AreEqual(message, apiException.Message);
        Assert.AreEqual(innerException, apiException.InnerException);
    }

    [Test]
    public void TestConstructorWithNullInnerException()
    {
        // Arrange
        const string message = "Test exception message";

        // Act
        var apiException = new APIException(message, null);

        // Assert
        Assert.AreEqual(message, apiException.Message);
        Assert.IsNull(apiException.InnerException);
    }
}