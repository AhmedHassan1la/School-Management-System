﻿namespace SchoolManagmen.Entites;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int StudentId { get; set; }

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
