using AutoMapper;
using SchoolManagmen.Contracts.Teachers;
using SchoolManagmen.Entites;

namespace SchoolManagmen.MappingWithAutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherResponse>();
            CreateMap<TeacherRequest, Teacher>();
        }
    }
}
