namespace Game.Application.Contracts.Core
{
    public abstract class BaseGrid
    {
        public string? Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Cells { get; set; } = null!;
    }
}
