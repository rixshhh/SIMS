using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

AppDbContext DbContext = new();

var allStudents = DbContext.Students.ToList(); // List<Students>

foreach (var student in allStudents)
{
    Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Gender: {student.Gender} , Email: {student.Email}, Address: {student.Address}");
}

Console.WriteLine("\nAll Subjects");
var allSubjects = DbContext.Subjects.ToList();
foreach (var subject in allSubjects)
{
    Console.WriteLine($"ID: {subject.SubjectID},  SubjectName: {subject.SubjectName} ({subject.SubjectCode})");
}


Console.WriteLine("\nSubjects from CS Branch ");
var filteredSubjects = DbContext.Subjects
    .Where(subject => subject.SubjectCode.Contains("CS"));

foreach (var sub in filteredSubjects)
{
    Console.WriteLine($"ID: {sub.SubjectID}, SubjectName: {sub.SubjectName} ({sub.SubjectCode})");
}


Console.WriteLine("\nFaculties and their Departments ");
var allFaculties = DbContext.Faculty.ToList();

foreach (var faculty in allFaculties)
{
    Console.WriteLine($"ID: {faculty.FacultyID}, Name: {faculty.FacultyName}, Department: {faculty.Department}");
}
