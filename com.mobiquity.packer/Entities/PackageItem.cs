namespace com.mobiquity.packer.Entities;

/// <summary>
///     Represents an item to be included in a package.
/// </summary>
public class PackageItem
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PackageItem" /> class.
    /// </summary>
    public PackageItem()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PackageItem" /> class with the given index, weight, and cost.
    /// </summary>
    /// <param name="index">The index of the item.</param>
    /// <param name="weight">The weight of the item.</param>
    /// <param name="cost">The cost of the item.</param>
    public PackageItem(int index, decimal weight, decimal cost)
    {
        Index = index;
        Weight = weight;
        Cost = cost;
    }

    /// <summary>
    ///     Gets or sets the index of the item.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    ///     Gets or sets the weight of the item.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    ///     Gets or sets the cost of the item.
    /// </summary>
    public decimal Cost { get; set; }
}