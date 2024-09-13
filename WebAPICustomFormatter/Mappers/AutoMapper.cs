using AutoMapper;
using WebAPICustomFormatter.DTOs;
using WebAPICustomFormatter.Entities;

namespace WebAPICustomFormatter.Mappers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Person, PersonDTO>().ReverseMap();
    }
}
