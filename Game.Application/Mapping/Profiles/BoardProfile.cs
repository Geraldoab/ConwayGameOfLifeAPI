using AutoMapper;
using Game.Application.Contracts;
using Game.Domain.Model;

namespace Game.Application.Mapping.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<BoardStatePostRequest, Board>()
                .ForMember(d => d.Width, s => s.MapFrom(m => m.Board.Width))
                .ForMember(d => d.Height, s => s.MapFrom(m => m.Board.Height));
        }
    }
}
