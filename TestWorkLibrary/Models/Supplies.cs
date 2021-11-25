using System.Collections.Generic;

namespace TestWorkLibrary.Models
{
    public class Supplies
    {
        public List<Supply> Items { get; set; }
        public List<string> Includes { get; set; }

        public Supplies(List<Supply> items, List<string> includes)
        {
            Items = items;
            Includes = includes;
        }
    }
}
