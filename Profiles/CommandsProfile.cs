using AutoMapper;
using DTOs;
using Models;

namespace Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile(){
            //source -> target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}