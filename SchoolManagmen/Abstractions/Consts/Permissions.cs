namespace SchoolManagmen.Abstractions.Consts
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions";

        public const string GetTeachers = "teachers:read";
        public const string AddTeachers = "teachers:add";
        public const string UpdateTeachers = "teachers:update";
        public const string DeleteTeachers = "teachers:delete";

        public const string GetSubjects = "subjects:read";
        public const string AddSubjects = "subjects:add";
        public const string UpdateSubjects = "subjects:update";
        public const string DeleteSubjects = "subjects:delete";

        public const string GetStudents = "students:read";
        public const string AddStudents = "students:add";
        public const string UpdateStudents = "students:update";
        public const string DeleteStudents = "students:delete";

        public const string GetGradeReports = "gradeReports:read";
        public const string AddGradeReports = "gradeReports:add";
        public const string UpdateGradeReports = "gradeReports:update";
        public const string DeleteGradeReports = "gradeReports:delete";

        public const string GetEnrollments = "enrollments:read";
        public const string AddEnrollments = "enrollments:add";
        public const string UpdateEnrollments = "enrollments:update";
        public const string DeleteEnrollments = "enrollments:delete";

        public const string GetClasses = "classes:read";
        public const string AddClasses = "classes:add";
        public const string UpdateClasses = "classes:update";
        public const string DeleteClasses = "classes:delete";

        public const string GetCourses = "courses:read";
        public const string AddCourses = "courses:add";
        public const string UpdateCourses = "courses:update";
        public const string DeleteCourses = "courses:delete";

        public const string GetAttendances = "attendance:read";
        public const string AddAttendances = "attendance:add";
        public const string UpdateAttendances = "attendance:update";
        public const string DeleteAttendances = "attendance:delete";


        public const string GetUsers = "users:read";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";

        public const string GetRoles = "roles:read";
        public const string AddRoles = "roles:add";
        public const string UpdateRoles = "roles:update";
        public static IList<string?> GetAllPermissions() =>
            typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
    }
}
