using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

AppDbContext DbContext = new();

while (true)
{
    Console.WriteLine("\n1. Fetch all students");
    Console.WriteLine("2. Fetch all Subjects");
    Console.WriteLine("3. Fetch Courses and Subjects");
    Console.WriteLine("4. Fetech Faculties and their Departments");
    Console.WriteLine("5. Students with their Enrolled Courses");
    Console.WriteLine("6. Attendance Grouped By Status (Present / Absent) and Marked By faculty.");
    Console.WriteLine("0. Exit");

    Console.Write("Choice: ");

    var choice = Console.ReadLine();

    if (choice == "0")
        break;

    switch (choice)
    {
        case "1":
            Console.WriteLine("\nAll Students Details\n");

            var allStudents = DbContext.Students
                .Select(s => new
                {
                    s.StudentID,
                    s.FirstName,
                    s.LastName,
                    s.Gender,
                    s.Email,
                    s.Address
                })
                .ToList();

            Console.WriteLine("--------------------------------------------------");

            foreach (var s in allStudents)
            {
                Console.WriteLine($"Student ID : {s.StudentID}");
                Console.WriteLine($"Name       : {s.FirstName} {s.LastName}");
                Console.WriteLine($"Gender     : {s.Gender}");
                Console.WriteLine($"Email      : {s.Email}");
                Console.WriteLine($"Address    : {s.Address}");
                Console.WriteLine("--------------------------------------------------");
            }
            break;

        case "2":
            Console.WriteLine("\nAll Subjects Details\n");

            var allSubjects = DbContext.Subjects
                .Select(s => new
                {
                    s.SubjectID,
                    s.SubjectName,
                    s.SubjectCode
                })
                .ToList();

            Console.WriteLine("--------------------------------------------------");

            foreach (var subject in allSubjects)
            {
                Console.WriteLine($"Subject ID   : {subject.SubjectID}");
                Console.WriteLine($"Subject Name : {subject.SubjectName}");
                Console.WriteLine($"Subject Code : {subject.SubjectCode}");
                Console.WriteLine("--------------------------------------------------");
            }
            break;

        case "3":
            Console.WriteLine("\nAll Courses and their Subjects\n");

            var CourseAndSubjects =
                from c in DbContext.Courses
                join s in DbContext.Subjects
                    on c.CourseID equals s.CourseID
                orderby c.CourseName
                select new
                {
                    c.CourseName,
                    c.CourseCode,
                    c.TotalFees,
                    s.SubjectName
                };

            string currentCourse = "";

            foreach (var cas in CourseAndSubjects)
            {
                if (currentCourse != cas.CourseName)
                {
                    currentCourse = cas.CourseName;

                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Course Name : {cas.CourseName}");
                    Console.WriteLine($"Course Code : {cas.CourseCode}");
                    Console.WriteLine($"Total Fees  : {cas.TotalFees}");
                    Console.WriteLine("Subjects    :");
                }

                Console.WriteLine($"   - {cas.SubjectName}");
            }

            Console.WriteLine("--------------------------------------------------");
            break;

        case "4":
            Console.WriteLine("\nAll Faculties and their Departments\n");

            var allFaculty = DbContext.Faculty
                .ToList();

            Console.WriteLine("--------------------------------------------------");

            foreach (var faculty in allFaculty)
            {
                Console.WriteLine($"Faculty ID   : {faculty.FacultyID}");
                Console.WriteLine($"Faculty Name : {faculty.FacultyName}");
                Console.WriteLine($"Department   : {faculty.Department}");
                Console.WriteLine("--------------------------------------------------");
            }
            break;

        case "5":
            Console.WriteLine("\nStudents with their Enrolled Courses\n");

            var allEnrollCourses =
                (from s in DbContext.Students
                 join e in DbContext.Enrollments
                     on s.StudentID equals e.StudentID
                 join c in DbContext.Courses
                     on e.CourseID equals c.CourseID
                 join sub in DbContext.Subjects
                     on e.CourseID equals sub.CourseID
                 orderby s.FirstName, c.CourseName
                 select new
                 {
                     StudentName = s.FirstName + " " + s.LastName,
                     c.CourseName,
                     sub.SubjectName,
                     sub.SubjectCode
                 }).ToList();

            var groupedData = allEnrollCourses
                .GroupBy(x => x.StudentName)
                .OrderBy(g => g.Key);

            foreach (var studentGroup in groupedData)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine($"Student Name : {studentGroup.Key}");
                Console.WriteLine("--------------------------------------------------");

                var courseGroups = studentGroup
                    .GroupBy(x => x.CourseName);

                foreach (var course in courseGroups)
                {
                    Console.WriteLine($"Course Name  : {course.Key}");
                    Console.WriteLine("Subjects     :");

                    foreach (var subject in course
                            .Select(s => new { s.SubjectName, s.SubjectCode })
                            .Distinct())
                    {
                        Console.WriteLine($"   - {subject.SubjectName} ({subject.SubjectCode})");
                    }

                    Console.WriteLine();
                }
            }
            Console.WriteLine("==================================================");
            break;

        case "6":
            Console.WriteLine("\nAttendance Grouped By Status (Present / Absent)\n");

            var attendanceData =
                (from a in DbContext.Attendence
                 join s in DbContext.Students
                     on a.StudentID equals s.StudentID
                 join sub in DbContext.Subjects
                     on a.SubjectID equals sub.SubjectID
                 join f in DbContext.Faculty
                     on a.MarkedBy equals f.FacultyID
                 select new
                 {
                     StudentName = s.FirstName + " " + s.LastName,
                     sub.SubjectName,
                     a.AttendanceDate,
                     a.Status,
                     MarkedBy = f.FacultyName
                 })
                .ToList();

            var groupedBySubjectName = attendanceData
                .GroupBy(x => x.SubjectName)
                .OrderBy(g => g.Key);

            foreach (var subject in groupedBySubjectName)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine($"Subject : {subject.Key}");
                var dateGroups = subject
                    .GroupBy(x => x.AttendanceDate)
                    .OrderBy(g => g.Key);
                Console.WriteLine("--------------------------------------------------");

                foreach (var date in dateGroups)
                {
                    Console.WriteLine($"Date     : {date.Key:dd-MM-yyyy}");

                    var markedByGroups = date
                    .GroupBy(x => x.MarkedBy);

                    foreach (var faculty in markedByGroups)
                    {
                        Console.WriteLine($"Marked By: {faculty.Key}");

                        var presentStudents = faculty
                            .Where(x => x.Status == "Present")
                            .Select(x => x.StudentName)
                            .Distinct();

                        var absentStudents = faculty
                            .Where(x => x.Status == "Absent")
                            .Select(x => x.StudentName)
                            .Distinct();

                        Console.WriteLine("Present Students:");
                        if (presentStudents.Any())
                            foreach (var student in presentStudents)
                                Console.WriteLine($"   - {student}");
                        else
                            Console.WriteLine("   None");

                        Console.WriteLine("Absent Students:");
                        if (absentStudents.Any())
                            foreach (var student in absentStudents)
                                Console.WriteLine($"   - {student}");
                        else
                            Console.WriteLine("   None");

                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine("==================================================");
            break;

        case "7":
        
        break;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }

}

