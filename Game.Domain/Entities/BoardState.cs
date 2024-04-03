namespace Game.Domain.Entities
{
    public sealed class BoardState
    {
        public int Id { get; set; }

        public Grid Grid { get; set; } = null!;
    }
}
