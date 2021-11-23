using System.Collections.Generic;

namespace TestWorkLibrary.Models
{
    public class Supply
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<string> Addons { get; set; }
        public double Condition { get; set; }
        public double Probability { get; set; }

        public Supply(string name, List<string> addons, int count = 1, double condition = 0.0, double probability = 0.0)
        {
            Name = name;
            Addons = addons;
            Count = count;
            Condition = condition;
            Probability = probability;
        }
    }
}
