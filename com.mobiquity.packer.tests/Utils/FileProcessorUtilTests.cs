using com.mobiquity.packer.Entities;
using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Utils;

namespace com.mobiquity.packer.tests.Utils;

[TestFixture]
public class FileProcessorUtilTests
{
    [SetUp]
    public void TestInitialize()
    {
        // Create a test file with some content
        File.WriteAllText(TestFilePath, TestFileContents);
    }

    [TearDown]
    public void TestCleanup()
    {
        // Delete the test file
        File.Delete(TestFilePath);
    }

    private const string
        TestFilePath = "test"; // Intentionally omitting file extension as per given sample resouse files

    private const string TestFileContents =
        "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)\n8 : (1,15.3,€34)";

    [Test]
    public async Task TestProcessFileAsync()
    {
        // Arrange
        var linesProcessed = 0;

        // Act
        await FileProcessorUtil.ProcessFileAsync(TestFilePath, async line =>
        {
            linesProcessed++;
            await Task.Delay(10);
        });

        // Assert
        Assert.AreEqual(2, linesProcessed);
    }

    [Test]
    public Task TestProcessFileAsync_FileNotFound()
    {
        // Arrange
        const string filePath = "non-existent-file";

        // Act & Assert
        Assert.ThrowsAsync<APIException>(async () =>
            await FileProcessorUtil.ProcessFileAsync(filePath, async line => { }));
        return Task.CompletedTask;
    }

    [Test]
    public Task TestProcessFileAsync_LineProcessorThrowsException()
    {
        // Arrange
        const string exceptionMessage = "Line processor failed";
        Func<string, ValueTask> lineProcessor = async line =>
        {
            await Task.Delay(10);
            throw new Exception(exceptionMessage);
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<APIException>(async () =>
            await FileProcessorUtil.ProcessFileAsync(TestFilePath, lineProcessor));
        Assert.AreEqual(exceptionMessage, ex.InnerException?.Message);
        return Task.CompletedTask;
    }

    [Test]
    public async Task FileToPackages_ReturnsCorrectPackages()
    {
        // Arrange & Act
        var packages = await FileProcessorUtil.FileToPackagesAsync(TestFilePath);

        // Assert
        Assert.AreEqual(2, packages.Count);

        var expectedPackage1 = new Package(81, new List<PackageItem>
        {
            new(1, 53.38M, 45),
            new(2, 88.62M, 98),
            new(3, 78.48M, 3),
            new(4, 72.30M, 76),
            new(5, 30.18M, 9),
            new(6, 46.34M, 48)
        });

        var expectedPackage2 = new Package(8, new List<PackageItem>
        {
            new(1, 15.3M, 34)
        });

        var package1 = packages[0];

        Assert.AreEqual(expectedPackage1.Capacity, package1.Capacity);
        Assert.AreEqual(expectedPackage1.Items.Count, package1.Items.Count);
        for (var i = 0; i < expectedPackage1.Items.Count; i++)
        {
            Assert.AreEqual(expectedPackage1.Items[i].Index, package1.Items[i].Index);
            Assert.AreEqual(expectedPackage1.Items[i].Weight, package1.Items[i].Weight);
            Assert.AreEqual(expectedPackage1.Items[i].Cost, package1.Items[i].Cost);
        }

        var package2 = packages[1];

        Assert.AreEqual(expectedPackage2.Capacity, package2.Capacity);
        Assert.AreEqual(expectedPackage2.Items.Count, package2.Items.Count);
        for (var i = 0; i < expectedPackage2.Items.Count; i++)
        {
            Assert.AreEqual(expectedPackage2.Items[i].Index, package2.Items[i].Index);
            Assert.AreEqual(expectedPackage2.Items[i].Weight, package2.Items[i].Weight);
            Assert.AreEqual(expectedPackage2.Items[i].Cost, package2.Items[i].Cost);
        }
    }
}