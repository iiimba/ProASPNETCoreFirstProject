using System.Collections.Generic;

namespace GOFPatternExamples.Visitor
{
    class ObjectStructure
    {
        private List<Element> _arrayList = new List<Element>();

        public void Add(Element element)
        {
            _arrayList.Add(element);
        }

        public void Remove(Element element)
        {
            _arrayList.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (var item in _arrayList)
            {
                item.Accept(visitor);
            }
        }
    }
}
