using System;
using System.Collections.Generic;

namespace GOFPatternExamples.Strategy
{
    class SomeArray
    {
        private List<object> _list = new List<object> { 5, 8, 7, 3, 1, 4 };

        public Strategy Strategy { get; set; }

        public void Sort()
        {
            if (Strategy == null)
            {
                _list.Sort();

                return;
            }

            Strategy.SortAlgorithm(_list);
        }

        public void ShowItems()
        {
            foreach (var item in _list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
