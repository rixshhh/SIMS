using System.ComponentModel.DataAnnotations;

public class Faculty
{
    [Key]
    public int FacultyID { get; set; }

    public required string FacultyName { get; set; }
    public required string Department { get; set; }
}