using System;
using System.Reflection.Metadata;
using CommandLine;
using Microsoft.Extensions.Options;

namespace Database.Options;

[Verb("query", HelpText = "Use to perform querying to database.")]
public class QueryOptions
{
    [Value(0, Required = true, HelpText = "The entity to perform querying.")]
    public string Entity {get; set;} = null!;

    [Option('f', "filter", HelpText = "Use to filter results.")]
    public IEnumerable<string>? Filter {get; set;}

    [Option('s', "sort", HelpText = "Use to sort results.")]
    public IEnumerable<string>? Sort {get; set;}

    [Option('g', "groupby", HelpText = "Use to group results.")]
    public IEnumerable<string>? GroupBy {get; set;}

}
