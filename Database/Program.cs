using CommandLine;
using ConsoleTables;
using Database.Models;
using Database.Options;
using Database.Services;
using Database.Services.Command;

class Program
{
    static void Main(string[] args)
    {
        // Parser.Default.ParseArguments<QueryOptions>(args)
        //     .MapResult(
        //         (QueryOptions opts) => HandleQueryOptions(opts),
        //         errs => 1
        //     );

        List<Student> students = [
            new Student()
            {
                FirstName = "Mark"
            },
            new Student
            {
                FirstName = "Jose"
            }
        ];

        QueryOptions option = new()
        {
            GroupBy = ["firstname"]
        };

        QueryServices.ExecuteQuery(students.AsQueryable(), option);
    }
    private static int HandleQueryOptions(QueryOptions opts)
    {
        Console.WriteLine(opts.Entity);
        return 0;
    }
}