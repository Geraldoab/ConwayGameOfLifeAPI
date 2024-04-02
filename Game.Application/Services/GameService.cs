using AutoMapper;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Interfaces.Services;
using Game.Domain.Model;
using Game.Infra.Data.Core;

namespace Game.Application.Services
{
    internal class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public GameService(IMapper mapper, IGameRepository gameRepository)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public CustomResult GetById(int id)
        {
            var result = _gameRepository.GetById(id);

            return CustomResult.Ok(result);
        }

        public CustomResult GetFinalState(int iterations)
        {
            var result = _gameRepository.GetFinalState(iterations);

            return CustomResult.Ok(result);
        }

        public CustomResult GetNextState()
        {
            var result = _gameRepository.GetNextState();
            return CustomResult.Ok(result);
        }

        public CustomResult RemoveById(int id)
        {
            var result = _gameRepository.RemoveById(id);
            return CustomResult.Ok(result);
        }

        public CustomResult Simulate(int iterations)
        {
            var result = _gameRepository.Simulate(iterations);
            return CustomResult.Ok(result);
        }

        public CustomResult Upload(Grid grid)
        {
            var result = _gameRepository.Upload(grid);
            return CustomResult.Ok(result);
        }
    }
}
