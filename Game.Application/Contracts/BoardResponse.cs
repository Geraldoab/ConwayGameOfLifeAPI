using Game.Application.Contracts.Core;

namespace Game.Application.Contracts
{
    public class BoardResponse
    {
        public required GridResponse Grid { get; set; }
    }
}
