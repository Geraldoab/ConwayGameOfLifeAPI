using Game.Application.Contracts.Core;

namespace Game.Application.Contracts
{
    public class BoardRequest
    {
        public required GridRequest Grid { get; set; }
    }
}
