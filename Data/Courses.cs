using System.ComponentModel.DataAnnotations;

public class Courses
{
    [Key]
    public int CourseID { get; set; }

    public required string CourseName { get; set; }
    public required string CourseCode { get; set; }
    public required string DurationMonths { get; set; }
    public required decimal TotalFees { get; set; }

}