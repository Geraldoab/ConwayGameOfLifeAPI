using Game.Domain.Model;
using Game.Infra.Data.Core;

namespace Game.Domain.Interfaces.Services
{
    public interface IGameService
    {
        CustomResult Start(Board board);
    }
}
