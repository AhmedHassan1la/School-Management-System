using SchoolManagmen.Entites;

namespace SchoolManagmen.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            // Define the table name
            builder.ToTable("Attendance");

            // Define the primary key
            builder.HasKey(a => a.AttendanceId);

            // Define the relationship with Student
            builder.HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the Date field
            builder.Property(a => a.Date)
                .IsRequired()
                .HasColumnType("date"); // Use 'date' type in SQL Server

            // Configure the Status field
            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20); // Set a max length of 20 characters for status

            // Add unique index to prevent duplicate entries for the same student on the same date
            builder.HasIndex(a => new { a.StudentId, a.Date })
                .IsUnique();
        }
    }
}
