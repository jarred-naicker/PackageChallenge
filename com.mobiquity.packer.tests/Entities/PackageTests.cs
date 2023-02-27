using com.mobiquity.packer.Entities;

namespace com.mobiquity.packer.tests.Entities;

[TestFixture]
public class PackageTests
{
    [Test]
    public void TestConstructor()
    {
        var capacity = 10.5m;
        var items = new List<PackageItem>();
        var package = new Package(capacity, items);

        Assert.AreEqual(capacity, package.Capacity);
        Assert.AreEqual(items, package.Items);
        Assert.AreEqual(0, package.PackagedIndexes.Count);
    }

    [Test]
    public void TestGetPackagedIndexesResult()
    {
        var package = new Package();
        package.PackagedIndexes = new List<int> { 1, 2, 3 };

        Assert.AreEqual("1,2,3", package.GetPackagedIndexesResult());
    }

    [Test]
    public void TestSetCapacity()
    {
        var package = new Package();
        var capacity = 15.5m;
        package.Capacity = capacity;

        Assert.AreEqual(capacity, package.Capacity);
    }

    [Test]
    public void TestSetItems()
    {
        var package = new Package();
        var items = new List<PackageItem>();
        var item = new PackageItem(1, 2.5m, 5);
        items.Add(item);
        package.Items = items;

        Assert.AreEqual(items, package.Items);
    }
}