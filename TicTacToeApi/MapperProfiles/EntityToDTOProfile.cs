using AutoMapper;
using TicTacToeApi.Models;
using TicTacToeApi.Models.DTO;

namespace TicTacToeApi.MapperProfiles
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<Game, GameInfoDTO>()
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => new TableDTO
                {
                    Size = src.Table.Size,
                    UnusedPoints = src.Table.UnusedPoints,
                    Points = src.Table.Points.Select(p => new PointDTO { X = p.X, Y = p.Y, Value = p.Value }).ToList()
                }));
            CreateMap<Table, TableDTO>();
            CreateMap<Point, PointDTO>();
        }
    }
}
