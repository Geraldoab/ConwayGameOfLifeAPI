﻿using Game.Domain.Entities;
using Game.Infra.Data.Core;

namespace Game.Domain.Interfaces.Services
{
    public interface IGameService
    {
        Task<CustomResult> UploadAsync(Grid grid, CancellationToken cancellationToken);
        Task<CustomResult> GetNextStateAsync(CancellationToken cancellationToken);
        Task<CustomResult> SimulateAsync(int iterations, CancellationToken cancellationToken);
        Task<CustomResult> GetFinalStateAsync(int iterations, CancellationToken cancellationToken);
        Task<CustomResult> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CustomResult> RemoveByIdAsync(int id, CancellationToken cancellationToken);
    }
}
