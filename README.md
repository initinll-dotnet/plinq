# Parallel Linq (PLINQ)

## Dump

> External Package - `Dumpify`

- [x] `Dump`

```csharp
IEnumerable<int> collection = [1,2,3,4,5];

collection.Dump();
```

- [x] `AsParallel`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable
    .Range(1, 10) // generating a collection of 10 elements
    .AsParallel() // activating parallel processing
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var _ in collection);

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
    // adding the computed number to the list of numbers computed by the current thread
    threadMap.AddOrUpdate(
        key: Environment.CurrentManagedThreadId, 
        addValue: [n],
        updateValueFactory: (key, values) => [..values, n]);

    for (int i = 0; i < 100_000_000; i++)
    {
        n += i;
    }

    return n;
}
```

- [x] `WithDegreeOfParallelism`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable
    .Range(1, 10) // generating a collection of 10 elements
    .AsParallel() // activating parallel processing
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var _ in collection);

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
    // adding the computed number to the list of numbers computed by the current thread
    threadMap.AddOrUpdate(
        key: Environment.CurrentManagedThreadId, 
        addValue: [n],
        updateValueFactory: (key, values) => [..values, n]);

    for (int i = 0; i < 100_000_000; i++)
    {
        n += i;
    }

    return n;
}
```

- [x] `AsOrdered`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable
    .Range(1, 10) // generating a collection of 10 elements
    .AsParallel() // activating parallel processing
    .AsOrdered() // preserving the order of the elements
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `AsUnordered`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable
    .Range(1, 10) // generating a collection of 10 elements
    .AsParallel() // activating parallel processing
    .AsOrdered() // preserving the order of the elements
    .AsUnordered() // removing the order of the elements
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `ParallelEnumerable.Range`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = ParallelEnumerable.Range(1, 10) // same as Enumerable.Range(1, 10).AsParallel()
    .AsOrdered() // preserving the order of the elements
    .AsUnordered() // removing the order of the elements
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `ParallelEnumerable.Repeat`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = ParallelEnumerable.Repeat(1, 10) // same as Enumerable.Repeat(1, 10).AsParallel()
    .AsOrdered() // preserving the order of the elements
    .AsUnordered() // removing the order of the elements
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `ParallelEnumerable.Empty`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = ParallelEnumerable.Empty<int>() // creating an empty parallel collection, does not output anything
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // setting the degree of parallelism to 2 (2 threads)
    .Select(HeavyComputation); // applying a heavy computation to each element via parallel processing

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `AsSequential`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = ParallelEnumerable.Range(0, 10) 
    .AsSequential() // converting the parallel collection to a sequential collection, opposite of AsParallel(), does not run in parallel, single thread
    .Select(HeavyComputation); // applying the heavy computation to each element

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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

//////////////

using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable.Range(0, 10) 
    .AsParallel() // running the computation in parallel, multiple threads
    .WithDegreeOfParallelism(degreeOfParallelism: 4) // limiting the number of threads to 4
    .Select(HeavyComputation) // applying the heavy computation to each element in parallel
    .AsSequential() // converting the parallel collection to a sequential collection, opposite of AsParallel(), does not run in parallel, single thread
    .Select(n => n * 2); // applying a transformation to each element in the collection sequentially

// forcing heavy computation to be excuted in parallel (multi thread execution)
// then after switching back to sequential execution, the computation will be executed in a single thread

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `WithCancellation`

```csharp
using System.Collections.Concurrent;
using System.Diagnostics;
using Dumpify;

var stopwatch = Stopwatch.StartNew();

var cts = new CancellationTokenSource();

// [thread id] => [list of numbers computed by this thread]
// [1] => [1, 2, 3, 4, 5]
// [3] => [6, 7, 8, 9, 10]
ConcurrentDictionary<int, List<int>> threadMap = [];

// generating a collection of 10 elements and applying a heavy computation to each element
var collection = Enumerable.Range(0, 10) 
    .AsParallel() // running the computation in parallel, multiple threads
    .WithCancellation(cts.Token) // using a cancellation token to cancel the computation
    .WithDegreeOfParallelism(degreeOfParallelism: 4) // limiting the number of threads to 4
    .Select(HeavyComputation) // applying the heavy computation to each element in parallel
    .AsSequential() // converting the parallel collection to a sequential collection, opposite of AsParallel(), does not run in parallel, single thread
    .Select(n => n * 2); // applying a transformation to each element in the collection sequentially

// cancelling the computation
cts.Cancel();

// forcing heavy computation to be excuted in parallel (multi thread execution)
// then after switching back to sequential execution, the computation will be executed in a single thread

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    Console.WriteLine(item);
}

stopwatch.Stop();
stopwatch.ElapsedMilliseconds.Dump("Execution Time");

// displaying the thread map
threadMap.Dump("Thread Map");

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
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
```

- [x] `WithMergeOptions`

```csharp
using Dumpify;

var collection = MyRange(0, 10) 
    .AsParallel() 
    .WithDegreeOfParallelism(degreeOfParallelism: 2) // sets the number of threads to use
    .WithMergeOptions(ParallelMergeOptions.NotBuffered) // does not buffer the results and makes them available as soon as they are produced
    .Select(HeavyComputation);

// WithMergeOptions is used to specify how the results of the parallel computation are merged
// Available options are:
// - Default: uses the default buffering behavior
// - NotBuffered: makes the results available as soon as they are produced
// - AutoBuffered: buffers the results and makes them available in the order they are produced
// - FullyBuffered: buffers all the results and makes them available in the order they are produced

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    $"Consuming {item}".Dump();
}

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
    $"Processing {n}".Dump();

    int dummy = 0;
    for (int i = 0; i < 100_000_000; i++)
    {
        dummy += i;
    }

    return n;
}

static IEnumerable<int> MyRange(int start, int count)
{
    for (int i = start; i < start + count; i++)
    {
        yield return i;
    }
}
```

- [x] `WithExecutionMode`

```csharp
using Dumpify;

var collection = MyRange(0, 10) 
    .AsParallel() // this is just a hint to the runtime to parallelize the computation
    .WithExecutionMode(ParallelExecutionMode.ForceParallelism) // force parallelism
    .WithDegreeOfParallelism(degreeOfParallelism: 2)
    .Select(HeavyComputation);

// WithExecutionMode forces the computation to be parallelized
// available options are:
// - Default: the runtime will decide whether to parallelize or not
// - ForceParallelism: the runtime will parallelize the computation

// iterating over the collection to force the computation to be
foreach (var item in collection)
{
    $"Consuming {item}".Dump();
}

// a heavy computation that takes a while to complete
int HeavyComputation(int n)
{
    $"Processing {n}".Dump();

    int dummy = 0;
    for (int i = 0; i < 100_000_000; i++)
    {
        dummy += i;
    }

    return n;
}

static IEnumerable<int> MyRange(int start, int count)
{
    for (int i = start; i < start + count; i++)
    {
        yield return i;
    }
}
```

- [x] `ForAll`

```csharp
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
```

