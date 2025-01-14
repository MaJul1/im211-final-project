using System;
using CommandLine;

namespace Populator;

public class Option
{
    [Option('d', "databasePath", Required = true, HelpText = "The .db file path.")]
    public string DatabasePath {get; set;} = null!;

    [Option('c', "csvPath", Required = true, HelpText = "The .csv file path.")]
    public string CsvPath {get; set;} = null!;
}
