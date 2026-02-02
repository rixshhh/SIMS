using System.ComponentModel.DataAnnotations;

public class Results
{
    [Key]
    public int ResultID { get; set; }

    public int StudentID { get; set; }
    public int ExamID { get; set; }
    public double MarksObtained { get; set; }
    public required string Grade { get; set; }
}