using SchoolManagmen.Entites;

namespace SchoolManagmen.Data.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            // Table Name
            builder.ToTable("Teachers");

            // Primary Key
            builder.HasKey(t => t.TeacherId);

            // Property Configurations
            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(t => t.HireDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(t => t.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.IsActive)
                .IsRequired();

            builder.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(100);

            // Full Configuration for PhoneNumber with Egyptian format
            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15)
                .HasAnnotation("RegularExpression", @"^(?:\+20|0)?1[0-2]\d{8}$")
                .HasComment("Phone number should be in Egyptian format, e.g., +201XXXXXXXXX or 01XXXXXXXXX");

            // Email Configuration remains the same
            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .HasComment("Email must be in a valid format, e.g., example@domain.com");


            builder.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(255);

            // Index on Email for faster lookups
            builder.HasIndex(t => t.Email)
                .IsUnique();
        }
    }
}
