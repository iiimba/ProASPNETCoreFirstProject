using System;
using System.Collections.Generic;

namespace GOFPatternExamples.Composite
{
    abstract class Component
    {
        public string Name { get; init; }

        public decimal Price { get; init; }

        public int Depth { get; set; }

        public Component(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public abstract void Operation();

        public abstract decimal GetPrice();
    }

    class Product : Component
    {
        public Product(string name, decimal price)
            : base(name, price)
        {

        }

        public override void Operation()
        {
            Console.WriteLine($"Real product: {Name}, Depth: {Depth}");
        }

        public override decimal GetPrice()
        {
            return Price;
        }
    }

    class Box : Component
    {
        private List<Component> _composites = new List<Component>();

        public Box(string name, decimal price)
            : base(name, price)
        {

        }

        /// <summary>
        /// This method can be placed in the Composite class
        /// </summary>
        public void Add(Component composite)
        {
            composite.Depth = Depth + 1;
            _composites.Add(composite);
        }

        /// <summary>
        /// This method can be placed in the Composite class
        /// </summary>
        public void Remove(Component composite)
        {
            _composites.Remove(composite);
        }

        /// <summary>
        /// This method can be placed in the Composite class
        /// </summary>
        public Component Find(int index)
        {
            return _composites[index];
        }

        public override void Operation()
        {
            Console.WriteLine($"Box name: {Name}, Depth: {Depth}");
            foreach (var composite in _composites)
            {
                composite.Operation();
            }
        }

        public override decimal GetPrice()
        {
            var price = Price;
            foreach (var composite in _composites)
            {
                price += composite.GetPrice();
            }

            return price;
        }
    }
}
