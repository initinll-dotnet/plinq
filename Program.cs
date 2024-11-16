using System.Collections.Concurrent;
using Dumpify;

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

Enumerable.Range(0, 10) 
    .AsParallel() // this is just a hint to the runtime to parallelize the computation
    .WithExecutionMode(ParallelExecutionMode.ForceParallelism) // force parallelism
    .WithDegreeOfParallelism(degreeOfParallelism: 2)
    .Select(HeavyComputation)
    .ForAll(item => 
    {
        $"Processing {item} on {Environment.CurrentManagedThreadId}".Dump();
    }); // processing in the current thread and not on main thread

// basically above perform actions on the current thread on the cores during parallel processing

// this performs on main thread after the parallel processing
// foreach (var item in collection)
// {
// }

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
    $"Processing {n}".Dump();

    // adding the computed number to the list of numbers computed by the current thread
    threadMap.AddOrUpdate(
        key: Environment.CurrentManagedThreadId, 
        addValue: [n],
        updateValueFactory: (key, values) => [..values, n]);

    int dummy = 0;
    for (int i = 0; i < 100_000_000; i++)
    {
        dummy += i;
    }

    return n;
}