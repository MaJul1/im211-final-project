using System;
using System.Drawing;
using System.Globalization;
using CommandLine;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using Populator.Database;

namespace Populator;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Option>(args)
            .WithParsed<Option>(o => TryPopulateDatabase(o));
    }

    private static void TryPopulateDatabase(Option option)
    {
        try
        {
            PopulateDatabase(option);
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            throw;
        }
    }

    private static void PopulateDatabase(Option option)
    {

        DeleteAllRecords(option.MySqlStringConnection);

        var csvModels = GetCsvModels(option.CsvPath);

        var courses = GetAllCourses(csvModels);

        var skills = GetAllSkills(csvModels);

        var departments = GetDepartments();

        var programs = GetPrograms();

        PopulateAllNecessaryRecords(courses, skills, departments, programs, option.MySqlStringConnection);

        PopulateStudents(csvModels, option.MySqlStringConnection);

        Console.WriteLine("Successful Population");
    }

    private static void PopulateStudents(List<CSVModel> csvModels, string path)
    {
        var context = new PlspeduviewContext(path);

        List<Student> result = [];

        foreach(var model in csvModels)
        {
            Student student = new() 
            {
                Barangay = model.Barangay,
                BirthDay = DateOnly.Parse(model.Birthday),
                Department = context.Departments.FirstOrDefault(s => s.Code == model.Department)!,
                Program = context.Programs.FirstOrDefault(s => s.Code == model.Program)!,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Municipality = model.Municipality,
                PhoneNumber = model.PhoneNumber,
                Province = model.Province,
                SchoolId = model.StudentId,
                Section = model.Section,
                Sex = int.Parse(model.Sex),
                Type = int.Parse(model.StudentType),
                YearLevel = int.Parse(model.YearLevel),
                DateAdded = DateTime.Now
            };

            string[] courses = model.Courses.Split(";");

            foreach(var course in courses)
            {
                string[] data = course.Split('%');
                student.Courses.Add(context.Courses.FirstOrDefault(s => s.CourseCode == data[0])!);
            }

            string[] skills = model.Skills.Split(";");

            foreach(var skill in skills)
            {
                student.Skills.Add(context.Skills.FirstOrDefault(s => s.Description == skill)!);
            }

            result.Add(student);
            
        }
        
        context.Students.AddRange(result);

        context.SaveChanges();

        Console.WriteLine("Successsful populating student.");
    }

    private static void PopulateAllNecessaryRecords(List<Course> courses, List<Skill> skills, List<Department> departments, List<SchoolProgram> programs, string path)
    {
        using (var context = new PlspeduviewContext(path))
        {
            context.Courses.AddRange(courses);
            context.Skills.AddRange(skills);
            context.Departments.AddRange(departments);
            context.Programs.AddRange(programs);

            context.SaveChanges();
        }

        Console.WriteLine("Course, skills, departments, programs saved in database.");
    }

    private static List<Skill> GetAllSkills(List<CSVModel> csvModels)
    {
        HashSet<string> uniqueSkills = [];

        foreach (var record in csvModels)
        {
            string[] skills = record.Skills.Split(';');
            foreach(var s in skills)
            {
                uniqueSkills.Add(s);
            }
        }

        List<Skill> result = [];
        foreach(var s in uniqueSkills)
        {
            var skill = new Skill()
            {
                Description = s,
                DateAdded = DateTime.Now
            };

            result.Add(skill);
        }

        Console.WriteLine("Successful skill conversion without error.");

        return result;
    }

    private static List<Course> GetAllCourses(List<CSVModel> csvModels)
    {
        HashSet<string> set = [];

        foreach(var record in csvModels)
        {
            string[] courses = record.Courses.Split(';');
            foreach (var course in courses)
            {
                set.Add(course);
            }
        }

        List<Course> result = [];
        HashSet<string> uniqueCodes = [];

        foreach(var s in set)
        {
            string[] courseData = s.Split('%');

            if (courseData.Length != 3)
            {
                throw new ArgumentNullException($"Invalid number of '%' signs detected at {s}. It should only be 2.");
            }

            if (!uniqueCodes.Add(courseData[0]))
            {
                throw new ArgumentNullException($"Duplicate or invalid data at {s}");
            }

            Course course = new();

            try 
            {
                course.CourseCode = courseData[0];
                course.CourseDescription = courseData[1];
                course.Units = int.Parse(courseData[2]);
                course.DateAdded = DateTime.Now;
            }
            catch
            {
                throw new ArgumentException($"Unable to convert to course at {s}"); 
            }

            result.Add(course);
        }

        Console.WriteLine("Successful course convertion without error.");

        return result;
    }

    private static void VerifyIfFilesExists(Option option)
    {
        if (File.Exists(option.CsvPath) == false)
        {
            throw new ArgumentException($"{option.CsvPath} does not exists.");
        }

        if (File.Exists(option.MySqlStringConnection) == false)
        {
            throw new ArgumentException($"{option.MySqlStringConnection} does not exists.");
        }
    }

    private static List<CSVModel> GetCsvModels(string csvPath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };

        List<CSVModel> records;

        using (var stream = new StreamReader(csvPath))
        using (var csv = new CsvReader(stream, config))
        {
            records = csv.GetRecords<CSVModel>().ToList();
        }

        return records;
    }

    private static List<Department> GetDepartments()
    {
        List<Department> departments = new()
        {
            new () { Code = "CCST", Description = "College of Computer Studies and Technology" },
            new () { Code = "CHK", Description = "College of Human Kinetics" },
            new () { Code = "COA", Description = "College of Accountancy" },
            new () { Code = "CAHS", Description = "College Of Allied Health Science" },
            new () { Code = "CAS", Description = "College of Arts and Science" },
            new () { Code = "CBA", Description = "College of Business Administration" },
            new () { Code = "COE", Description = "College of Engineering" },
            new () { Code = "CTHM", Description = "College of Tourism And Hospitality Management" },
            new () { Code = "CTEd", Description = "Bachelor of Teacher Education" }
        };
        return departments;
    }

    private static List<SchoolProgram> GetPrograms()
    {
        List<SchoolProgram> program = new()
        {
            new () { Code = "BSIT", Description = "Bachelor of Science in Information Technology" },
            new () { Code = "BSIS", Description = "Bachelor of Science in Information System" },
            new () { Code = "BSPED", Description = "Bachelor of Science in Physical Education" },
            new () { Code = "BSA", Description = "Bachelor of Science in Accountancy" },
            new () { Code = "BSAIS", Description = "Bachelor of Science in Accounting Information System" },
            new () { Code = "BSMA", Description = "Bachelor Of Science In Management Accounting" },
            new () { Code = "BSN", Description = "Bachelor of Science in Nursing" },
            new () { Code = "BAPS", Description = "Bachelor of Arts in Political Science" },
            new () { Code = "BAC", Description = "Bachelor of Arts in Communication" },
            new () { Code = "BPA", Description = "Bachelor of Public Administration" },
            new () { Code = "BSP", Description = "Bachelor of Science in Psychology" },
            new () { Code = "BSEc", Description = "Bachelor of Science in Economics" },
            new () { Code = "BSBA", Description = "Bachelor of Science in Business Administration" },
            new () { Code = "BSEnt", Description = "Bachelor of Science in Entrepreneurship" },
            new () { Code = "BSOA", Description = "Bachelor of Science Office Administration" },
            new () { Code = "BSCE", Description = "Bachelor of Science in Computer Engineering" },
            new () { Code = "BSHM", Description = "Bachelor of Science in Hospitality Management" },
            new () { Code = "BSTM", Description = "Bachelor of Science in Tourism Management" },
            new () { Code = "BSNEd", Description = "Bachelor of Special Need Education" },
            new () { Code = "BTTVTEd", Description = "Bachelor of Technical - Vocational Teacher Education" },
            new () { Code = "BECEd", Description = "Bachelor of Early Childhood Education" },
            new () { Code = "BEEd", Description = "Bachelor of Elementary Education" },
            new () { Code = "BPEd", Description = "Bachelor of Physical Education" },
            new () { Code = "BSEd", Description = "Bachelor of Secondary Education" }        
        };
        return program;
    }

    private static void DeleteAllRecords(string path)
    {
        Console.WriteLine("Resetting Database");
        
        using (var context = new PlspeduviewContext(path))
        {
            context.Courses.RemoveRange(context.Courses);
            context.Skills.RemoveRange(context.Skills);
            context.Departments.RemoveRange(context.Departments);
            context.Programs.RemoveRange(context.Programs);
            context.Students.RemoveRange(context.Students);

            context.SaveChanges();
        }
    }
}

