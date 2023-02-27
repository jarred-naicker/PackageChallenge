namespace com.mobiquity.packer.Entities;

/// <summary>
///     Represents a package that can hold a list of package items.
/// </summary>
public class Package
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Package" /> class.
    /// </summary>
    public Package()
    {
        Items = new List<PackageItem>();
        PackagedIndexes = new List<int>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Package" /> class with a given capacity and list of items.
    /// </summary>
    /// <param name="capacity">The maximum weight capacity of the package.</param>
    /// <param name="items">The list of items to be packed.</param>
    public Package(decimal capacity, List<PackageItem> items)
    {
        Capacity = capacity;
        Items = items;
        PackagedIndexes = new List<int>();
    }

    /// <summary>
    ///     Gets or sets the maximum weight capacity of the package.
    /// </summary>
    public decimal Capacity { get; set; }

    /// <summary>
    ///     Gets or sets the list of items to be packed.
    /// </summary>
    public List<PackageItem> Items { get; set; }

    /// <summary>
    ///     Gets or sets the list of indexes of the items that are packed in the package.
    /// </summary>
    public List<int> PackagedIndexes { get; set; }

    /// <summary>
    ///     Returns a string that represents the packed item indexes in the package.
    /// </summary>
    /// <returns>A comma-separated string of the indexes of the items that are packed in the package.</returns>
    public string GetPackagedIndexesResult()
    {
        if (PackagedIndexes.Count == 0) return "-";

        return string.Join(",", PackagedIndexes);
    }

    /// <summary>
    ///     Pack items into a package using dynamic programming.
    ///     Please Note: This method uses a Top Down Approach which uses recursion to obtain results, however it does so
    ///     in exponential performance as the more items given the longer it will take.
    ///     This can be expressed in Big O notation as O(n^2). Not using this but with the additional constraints being
    ///     the max weight of 100 and 15 items at most to choose from there is not much of a performance difference
    ///     when compared to Dynamic Programming approach. However this solution does not scale well if the constraints
    ///     were to change.
    /// </summary>
    public void PackItemsAlternative()
    {
        var itemsToPack = GetItemsToPack();

        // Sort the list of items by their cost per unit weight in descending order
        itemsToPack.Sort((i1, i2) => -i1.Cost.CompareTo(i2.Cost));

        // Iterate over the sorted list of items and add each item to the package if its weight does not exceed the remaining capacity
        for (var i = 0; i < itemsToPack.Count && Capacity > 0; i++)
            if (itemsToPack[i].Weight <= Capacity)
            {
                PackagedIndexes.Add(itemsToPack[i].Index);
                Capacity -= itemsToPack[i].Weight;
            }
    }

    /// <summary>
    ///     Pack items into a package using dynamic programming.
    ///     Please Note: This method uses dynamic programming to solve the problem, this a Bottom Up Approach that uses
    ///     memorization to solve the problem can be expressed in Big O notation as O(n*W), where n is the number of
    ///     package items from out input and W the max weight capacity that our package can hold. This scales well in
    ///     comparison to a Top Down Approach.
    /// </summary>
    public void PackItems()
    {
        var itemsToPack = GetItemsToPack();

        var n = itemsToPack.Count;

        // Create a 2D array to store the optimal solution for each sub-problem
        var dp = new decimal[n + 1, (int)Capacity + 1];

        // Initialize the first row and first column to zero
        for (var w = 0; w <= (int)Capacity; w++) dp[0, w] = 0;
        for (var i = 0; i <= n; i++) dp[i, 0] = 0;

        // Solve the sub-problems using dynamic programming
        for (var i = 1; i <= n; i++)
        for (var w = 1; w <= (int)Capacity; w++)
            if (itemsToPack[i - 1].Weight <= w)
                dp[i, w] = Math.Max(dp[i - 1, w],
                    dp[i - 1, w - (int)itemsToPack[i - 1].Weight] + itemsToPack[i - 1].Cost);
            else
                dp[i, w] = dp[i - 1, w];

        // Extract the optimal solution by backtracking from the last sub-problem
        var weight = (int)Capacity;
        for (var i = n; i > 0 && weight > 0; i--)
            if (dp[i, weight] != dp[i - 1, weight])
            {
                PackagedIndexes.Add(itemsToPack[i - 1].Index);
                weight -= (int)itemsToPack[i - 1].Weight;
            }

        PackagedIndexes.Sort();
    }

    /// <summary>
    ///     Exclude items that cannot be packed i.e. dont meet constraints. Then order by item Weight as we prefer
    ///     to send a package which weighs less in case there is more than, one package with the same price.
    /// </summary>
    /// <returns>A list of PackageItem which can be packaged</returns>
    private List<PackageItem> GetItemsToPack()
    {
        return Items
            .Where(item => item.Cost <= 100 || item.Weight <= 100)
            .OrderBy(item => item.Weight)
            .ToList();
    }
}