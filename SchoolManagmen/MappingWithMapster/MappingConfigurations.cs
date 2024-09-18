
using SchoolManagmen.Contracts.Classes;
using SchoolManagmen.Contracts.Courses;
using SchoolManagmen.Contracts.Enrollments;
using SchoolManagmen.Contracts.GradeReports;
using SchoolManagmen.Contracts.Subjects;
using SchoolManagmen.Contracts.Teachers;
using SchoolManagmen.Contracts.User;
using SchoolManagmen.Entites;

namespace SchoolManagmen.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<TeacherRequest, Teacher>.NewConfig();
                  

        TypeAdapterConfig<Teacher, TeacherResponse>.NewConfig()

          .Map(dest => dest.ClassName, src => src.Classes.Any() ? src.Classes.First().ClassName : "no class")
            .Map(dest => dest.CourseNames, src => src.Courses.Select(c => c.CourseName).ToList());


        // Mapping from SubjectRequest to Subject
        TypeAdapterConfig<SubjectRequest, Subject>.NewConfig();
          

        // Mapping from Subject to SubjectResponse
        TypeAdapterConfig<Subject, SubjectResponse>.NewConfig()
          
            .Map(dest => dest.TeacherName,
                 src => $"{src.Teacher.FirstName} {src.Teacher.LastName}");


        TypeAdapterConfig<Enrollment, EnrollmentResponse>.NewConfig()
                   .Map(dest => dest.StudentName, src => $"{src.Student.FirstName} {src.Student.LastName}")
                   .Map(dest => dest.CourseName, src => src.Course.CourseName); 
        
        
        TypeAdapterConfig<GradeReport, GradeReportResponse>.NewConfig()
                   .Map(dest => dest.StudentName, src => $"{src.Student.FirstName} {src.Student.LastName}")
                   .Map(dest => dest.CourseName, src => src.Course.CourseName);
    }
}



//TypeAdapterConfig<TeacherRequest, Teacher>.NewConfig()
//           .TwoWays();
//TypeAdapterConfig<CourseRequest, Course>.NewConfig()
//          .TwoWays();




//TypeAdapterConfig<Class, ClassResponse>.NewConfig()
//           .Map(dest => dest.TeacherName, src => $"{src.Teacher.FirstName} {src.Teacher.LastName}");

//TypeAdapterConfig<Contracts.Authentication.RegisterRequest, ApplicationUser>.NewConfig()
//           .Map(dest => dest.UserName, src => src.Email);

//config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
//     .Map(dest => dest, src => src.user)
//     .Map(dest => dest.Roles, src => src.roles);

//config.NewConfig<CreateUserRequest, ApplicationUser>()
//     .Map(dest => dest.UserName, src => src.Email)
//     .Map(dest => dest.EmailConfirmed, src => true);

//config.NewConfig<UpdateUserRequest, ApplicationUser>()
//            .Map(dest => dest.UserName, src => src.Email)
//            .Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());