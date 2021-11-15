using System.Collections.Generic;

namespace GOFPatternExamples.Flyweight
{
    class FlyweightFactory
    {
        private readonly Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

        public Flyweight GetFlyweight(string key, object initValue)
        {
            if (!flyweights.ContainsKey(key))
            {
                flyweights.Add(key, new ConcreteFlyweight(initValue));
            }

            return flyweights[key];
        }
    }
}
