namespace Models
{
    public interface ISharedObject
    {
        public string Id { get; set; }
        public Position Position { get; set; }
    }
}