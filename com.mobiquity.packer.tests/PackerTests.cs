namespace com.mobiquity.packer.tests;

public class PackerTests
{
    [Test]
    public void Pack_WithValidFile_ReturnsExpectedResult()
    {
        // Arrange
        var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "testfile");
        File.WriteAllText(filePath,
            "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)\n" +
            "8 : (1,15.3,€34)\n" +
            "75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)\n" +
            "56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)");

        // Act
        var packer = new Packer();
        var result = packer.pack(filePath);

        // Assert
        Assert.AreEqual("4\n-\n2,7\n8,9", result);
    }
}