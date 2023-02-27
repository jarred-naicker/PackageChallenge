# Packaging Challenge for Mobiquity implemented by Jarred Naicker

## Solution details:

This solution contains a class library written in c# which contains all the logic for solving the packaging challenge. There is also a test project which contains unit tests to cover the solution. There is also a small API project that references the class library which I used to test the implementation. Code has been commented explaining the function and outcome of various classes and methods.

Class library implemented with .NET/C# called Packer. This class library will read file content of a given file path. Each line of the content will then be processed using a Factory Pattern and output a list of **PackageItem**.

As the assignment states, the pack method will receive a file path. Included in the class library is a file processor util which will read the file content for the given file path and using a Factory Pattern, construct **Package** instances per line, with each **Package** containing a **Capacity** and list of **PackageItem**. The Package object contains 2 methods which calculate the items that can be packaged as per the assignment. There are:

- ***PackItems***

  Uses Dynamic Programming to calculate the items to pack.

- ***PackItemsAlternative***

  Uses a recursive approach to calculate the items to pack.

## Performance Considerations

The packager problem appears to be a variation of the 0/1 knapsack problem. 

Careful consideration was taken in determining the optimal solution. I wanted to implement a solution that is both time and memory efficient.

I wanted to avoid implementing a solution where the execution increases exponentially when the number of package items and weight capacity grows.

So given my initial concerns about time and memory, I had to consider I wanted to represent my solution in Big O notation.

### What is Big O notation?

As per wikipedia, Big O notation is used to classify algorithms according to how their run time or space requirements grow as the input size grows.

Here I will use dynamic programming to solve the problem, the problem can be expressed in Big O notation as O(n*W), where n is the number of package items from out input and W the max weight capacity that our package can hold.

### What is Dynamic Programming?

Dynamic programming is optimization over recursion. See I could iterate over each package item and execute an expression to determine if it is the item I am looking for, I would also need to keep track of the item to "currently" salifies the requirements. This is a Top Down Approach which results in exponential performance as the more items given the longer it will take. This can be expressed in Big O notation as O(n^2). Instead I opted for Dynamic programming which is Bottom Up Approach and produces polynomial performance. This can be expressed in Big O notation as O(n*W). By using a strategy called memoization, storing results to subproblems I don't have to recompute them when needed later.

With that being said my preferred solution was implemented using Dynamic Programming, however I have included an alternative which uses recursion in the code so please take a look. The recursive solution may be slower but it is definitely easier to read, understand and maintain going forward.