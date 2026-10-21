using Xunit;

// Several tests change the process current directory (`Directory.SetCurrentDirectory`) or exercise
// full builds in scratch directories; running collections sequentially keeps process state isolated.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
