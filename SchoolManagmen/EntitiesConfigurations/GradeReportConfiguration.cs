using SchoolManagmen.Entites;

namespace SchoolManagmen.Configurations
{
    public class GradeReportConfiguration : IEntityTypeConfiguration<GradeReport>
    {
        public void Configure(EntityTypeBuilder<GradeReport> builder)
        {
            // Add unique index for preventing duplicate reports for the same student in the same course
            builder.HasIndex(gr => new { gr.StudentId, gr.CourseId })
                .IsUnique();

            // Define the table name
            builder.ToTable("GradeReports");

            // Define the primary key
            builder.HasKey(gr => gr.GradeReportId);

            // Define the relationship with Student
            builder.HasOne(gr => gr.Student)
                .WithMany(s => s.GradeReports)  // A student can have many grade reports
                .HasForeignKey(gr => gr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when student is deleted

            // Define the relationship with Course
            builder.HasOne(gr => gr.Course)
                .WithMany(c => c.GradeReports)  // A course can have many grade reports
                .HasForeignKey(gr => gr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when course is deleted

            // Grade field configuration
            builder.Property(gr => gr.Grade)
                .IsRequired()
                .HasColumnType("decimal(5,2)");  // Decimal with 2 decimal places

            // Comments field configuration
            builder.Property(gr => gr.Comments)
                .HasMaxLength(500);  // Optional, max length of 500 characters


        }
    }
}
