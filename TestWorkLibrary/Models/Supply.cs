using System.Collections.Generic;

namespace TestWorkLibrary.Models
{
    public class Supply
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<string> Addons { get; set; }
        public double Condition { get; set; }
        public bool HasFillCondition { get; set; }
        public double Probability { get; set; }
        public bool HasFillProbability { get; set; }

        public Supply(string name, List<string> addons, bool hasFillCondition = false,
            bool hasFillProbability = false, double probability = 1.0, double condition = 1.0,
            int count = 1)
        {
            Name = name;
            Addons = addons;
            Count = count;
            HasFillCondition = hasFillCondition;
            HasFillProbability = hasFillProbability;
            Condition = condition;
            Probability = probability;
        }
    }
}
