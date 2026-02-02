using System.ComponentModel.DataAnnotations;

public class Exams
{
    [Key]
    public int ExamID { get; set; }
    public int StudentID { get; set; }
    public string ExamType { get; set; }
    public DateOnly ExamDate { get; set; }
    public int MaxMarks { get; set; }
}