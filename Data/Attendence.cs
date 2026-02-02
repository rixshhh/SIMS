using System.ComponentModel.DataAnnotations;

public class Attendence
{
    [Key]
    public int AttendenceID { get; set; }

    public int StudentID { get; set;}
    public int SubjectID { get; set;}
    public DateOnly AttendanceDate { get; set; }
    public required string Status { get; set; }
    public int MarkedBy {get; set;}
    
}