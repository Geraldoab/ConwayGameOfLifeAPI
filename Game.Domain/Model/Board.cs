namespace Game.Domain.Model
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board() {}

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
