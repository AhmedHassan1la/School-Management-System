using SchoolManagmen.Entites;

namespace SchoolManagmen.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasIndex(c => new { c.CourseName, c.TeacherId })
                .IsUnique();

            builder.ToTable("Courses");

            // Primary Key
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Credits)
                .IsRequired();

            // Foreign Key for TeacherId
            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}