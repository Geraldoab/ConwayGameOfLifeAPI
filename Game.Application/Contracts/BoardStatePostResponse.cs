namespace Game.Application.Contracts
{
    public class BoardStatePostResponse
    {
        public int Id { get; set; }
        public int[,] Board { get; set; } = new int[,] { { 0, 0 }, {  1, 0 }, { 2, 0 }, { 3, 0 }, };
    }
}
