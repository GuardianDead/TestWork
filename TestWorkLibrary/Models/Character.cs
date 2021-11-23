namespace TestWorkLibrary.Models
{
    public class Character
    {
        public string Id { get; set; }
        public Supplies Supplies { get; set; }

        public Character(string id, Supplies supplies)
        {
            Id = id;
            Supplies = supplies;
        }
    }
}
