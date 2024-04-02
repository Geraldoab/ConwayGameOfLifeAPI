namespace Game.Application.Contracts.Core
{
    public class GridRequest
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Cells { get; set; }
    }
}
