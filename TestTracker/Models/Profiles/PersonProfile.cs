using AutoMapper;
using TestTracker.Models.DTO;

namespace TestTracker.Models.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile() {
            CreateMap<Person, ActionPersonDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<ActionPersonDTO, Person>();
        }
    }
}