using CommandLine;
using Database.Options;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<QueryOptions>(args)
            .MapResult(
                (QueryOptions opts) => HandleQueryOptions(opts),
                errs => 1
            );
    }
    private static int HandleQueryOptions(QueryOptions opts)
    {
        Console.WriteLine(opts.Entity);
        return 0;
    }
}