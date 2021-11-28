namespace CleanArchitecture.Core.Entities
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}