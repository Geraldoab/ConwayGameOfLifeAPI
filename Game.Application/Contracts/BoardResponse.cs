using Game.Application.Contracts.Core;
using System.Diagnostics.CodeAnalysis;

namespace Game.Application.Contracts
{
    public class BoardResponse
    {
        public BoardResponse()
        {  
        }

        [SetsRequiredMembers]
        public BoardResponse(GridResponse grid)
        {
            Grid = grid;
        }

        public required GridResponse Grid { get; set; }
    }
}
