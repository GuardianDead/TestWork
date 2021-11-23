using System.Collections.Generic;

namespace TestWorkLibrary.Models
{
    public class Supplies
    {
        public string Spawn { get; set; }
        public List<Supply> Items { get; set; }
        public List<string> Includes { get; set; }

        public Supplies(string spawn, List<Supply> items, List<string> includes)
        {
            Spawn = spawn;
            Items = items;
            Includes = includes;
        }
    }
}
