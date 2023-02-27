using com.mobiquity.packer.Entities;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Lib;

namespace com.mobiquity.packer.tests.Lib;

public class PackageFactoryTests
{
    [Test]
    public void CreatePackage_WhenGivenValidInput_ReturnsPackageWithCorrectCapacityAndItems()
    {
        // Arrange
        const string input = "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)";
        var expectedPackage = new Package(81, new List<PackageItem>
        {
            new(1, 53.38m, 45m),
            new(2, 88.62m, 98m),
            new(3, 78.48m, 3m),
            new(4, 72.30m, 76m),
            new(5, 30.18m, 9m),
            new(6, 46.34m, 48m)
        });

        // Act
        var resultPackage = PackageFactory.CreatePackage(input);

        // Assert
        Assert.AreEqual(expectedPackage.Capacity, resultPackage.Capacity);
        Assert.AreEqual(expectedPackage.Items.Count, resultPackage.Items.Count);
        for (var i = 0; i < expectedPackage.Items.Count; i++)
        {
            Assert.AreEqual(expectedPackage.Items[i].Index, resultPackage.Items[i].Index);
            Assert.AreEqual(expectedPackage.Items[i].Weight, resultPackage.Items[i].Weight);
            Assert.AreEqual(expectedPackage.Items[i].Cost, resultPackage.Items[i].Cost);
        }
    }

    [Test]
    public void CreatePackage_WhenGivenInputWithoutCapacity_ThrowsAPIException()
    {
        // Arrange
        const string input = "(1,53.38,€45) (2,88.62,€98)";

        // Act & Assert
        Assert.Throws<APIException>(() => PackageFactory.CreatePackage(input));
    }

    [Test]
    public void CreatePackage_WhenGivenInputWithoutItems_ThrowsAPIException()
    {
        // Arrange
        const string input = "10 :";

        // Act & Assert
        Assert.Throws<APIException>(() => PackageFactory.CreatePackage(input));
    }
}