using Xunit;

// The command tests replace Console.Out/Console.Error to capture build output, and the analysis
// tests are CPU-bound over the whole bindings tree. Running collections one at a time keeps the
// captured output per test and the timing predictable.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
