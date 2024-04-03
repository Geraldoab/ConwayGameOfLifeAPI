using AutoMapper;
using Game.Application.Contracts;
using Game.Application.Contracts.Core;
using Game.Domain.Entities;

namespace Game.Application.Mapping.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<GridRequest, Grid>()
                .ForMember(d => d.Width, s => s.MapFrom(m => m.Width))
                .ForMember(d => d.Height, s => s.MapFrom(m => m.Height))
                .ForMember(d => d.Cells, s => s.MapFrom(m => m.Cells)).ReverseMap();

            CreateMap<Grid, GridResponse>()
                .ForMember(d => d.Id, s => s.MapFrom(m=> m.Id))
                .ForMember(d => d.Width, s => s.MapFrom(m => m.Width))
                .ForMember(d => d.Height, s => s.MapFrom(m => m.Height))
                .ForMember(d => d.Cells, s => s.MapFrom(m => m.Cells));

            CreateMap<BoardRequest, BoardState>()
                .ForMember(d => d.Grid, s => s.MapFrom(m => m.Grid)).ReverseMap();

            CreateMap<BoardState, BoardResponse>()
                .ForMember(d => d.Grid, s => s.MapFrom(m => m.Grid)).ReverseMap();
        }
    }
}
