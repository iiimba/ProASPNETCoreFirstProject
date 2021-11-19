using System.Collections.Generic;

namespace GOFPatternExamples.Strategy
{
    abstract class Strategy
    {
        public abstract void SortAlgorithm(List<object> list);
    }

    class QuickSort : Strategy
    {
        public override void SortAlgorithm(List<object> list)
        {
            list.Sort(); // Default is quick
        }
    }

    class ShellSort : Strategy
    {
        public override void SortAlgorithm(List<object> list)
        {
            list.Sort(); // Imagine that it is shell sort:)
        }
    }

    class MergeSort : Strategy
    {
        public override void SortAlgorithm(List<object> list)
        {
            list.Sort(); // Imagine that it is merge sort:)
        }
    }
}
