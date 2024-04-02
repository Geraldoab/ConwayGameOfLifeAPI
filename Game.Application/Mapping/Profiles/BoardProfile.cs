using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Model;

namespace Game.Application.Mapping.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<BoardStatePostRequest, BoardState>()
                .ForMember(d => d.Grid.Width, s => s.MapFrom(m => m.Board.GridRequest.Width))
                .ForMember(d => d.Grid.Height, s => s.MapFrom(m => m.Board.GridRequest.Height))
                .ForMember(d => d.Grid.Cells, s => s.MapFrom(m => m.Board.GridRequest.Cells));
        }
    }
}
