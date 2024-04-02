namespace Game.Domain.Model
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Cells { get; set; }
    }
}
