using com.mobiquity.packer.Entities;

namespace com.mobiquity.packer.tests.Entities;

[TestFixture]
public class PackageItemTests
{
    [TestCase(1, 1.5, 10.0)]
    [TestCase(2, 2.5, 20.0)]
    [TestCase(3, 3.5, 30.0)]
    public void TestConstructor(int index, decimal weight, decimal cost)
    {
        // Act
        var item = new PackageItem(index, weight, cost);

        // Assert
        Assert.AreEqual(index, item.Index);
        Assert.AreEqual(weight, item.Weight);
        Assert.AreEqual(cost, item.Cost);
    }

    [TestCase(1, 1.5, 10.0)]
    [TestCase(2, 2.5, 20.0)]
    [TestCase(3, 3.5, 30.0)]
    public void TestProperties(int index, decimal weight, decimal cost)
    {
        // Arrange
        var item = new PackageItem(0, 0, 0);

        // Act
        item.Index = index;
        item.Weight = weight;
        item.Cost = cost;

        // Assert
        Assert.AreEqual(index, item.Index);
        Assert.AreEqual(weight, item.Weight);
        Assert.AreEqual(cost, item.Cost);
    }
}