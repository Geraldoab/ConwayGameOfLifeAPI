using AutoMapper;
using Game.Application.Contracts;
using Game.Application.Contracts.Core;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Interfaces.Services;
using Game.Domain.Entities;
using Game.Domain.Resources;
using Game.Infra.Data.Core;
using System.Net;

namespace Game.Application.Services
{
    public class GameService : IGameService
    {
        private const int MAX_NUMBER_OF_ITERATIONS = 100;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return CustomResult.Fail($"{Messages.INVALID_BOARD_ID} {id}");

            var result = await _gameRepository.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return CustomResult.Fail($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public async Task<CustomResult> GetFinalStateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 1)
                return CustomResult.Fail(Messages.INVALID_ITERATIONS);

            if (iterations > MAX_NUMBER_OF_ITERATIONS)
                return CustomResult.Fail($"{Messages.MAX_NUMBER_OF_ITERATIONS_WAS_REACHED} {MAX_NUMBER_OF_ITERATIONS}");

            var result = await _gameRepository.GetFinalStateAsync(iterations, cancellationToken);
            if (result == null)
                return CustomResult.Fail(Messages.INVALID_FINAL_STATE);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public async Task<CustomResult> GetNextStateAsync(CancellationToken cancellationToken)
        {
            var result = await _gameRepository.GetNextStateAsync(cancellationToken);
            if (result == null)
                return CustomResult.Fail(Messages.INVALID_NEXT_STATE);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public async Task<CustomResult> RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return CustomResult.Fail($"{Messages.INVALID_BOARD_ID} {id}");

            var result = await _gameRepository.RemoveByIdAsync(id, cancellationToken);
            if (result == null)
                return CustomResult.Fail($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<BoardResponse>(result));
        }

        public async Task<CustomResult> SimulateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 1)
                return CustomResult.Fail(Messages.INVALID_ITERATIONS);

            if (iterations > MAX_NUMBER_OF_ITERATIONS)
                return CustomResult.Fail($"{Messages.MAX_NUMBER_OF_ITERATIONS_WAS_REACHED} {MAX_NUMBER_OF_ITERATIONS}");

            var result = await _gameRepository.SimulateAsync(iterations, cancellationToken);
            if (result == null)
                return CustomResult.Fail($"{Messages.CURRRENT_BOARD_STATE_NOT_FOUND}", HttpStatusCode.NotFound);

            return CustomResult.Ok(_mapper.Map<GridResponse>(result));
        }

        public async Task<CustomResult> UploadAsync(Grid grid, CancellationToken cancellationToken)
        {
            if (grid == null || !grid.Validate().IsValid)
                return CustomResult.Fail(Messages.INVALID_BOARD_GRID);

            var result = await _gameRepository.UploadAsync(grid, cancellationToken);
            return CustomResult.Ok(result, HttpStatusCode.Created);
        }
    }
}
