using DataCatalogConsoleApp.Commands;
using DataCatalogConsoleApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Commands
{
    public class ExitConsoleCommand : AbstractCommand
    {
        public ExitConsoleCommand()
        {
            Name = "Exit Data Catalog Console";
            Description = "This command exits the console application";
            ExecutionCommand = ConsoleApplicationConstants.EXIT_CONSOLE_COMMAND_EXE_KEY;
        }

        public override async Task<bool> Execute()
        {
            ExitApp = true;
            PrintOutput();
            return ExitApp;
        }

        public override async Task<bool> PrintOutput()
        {
            Console.Clear();
            Console.WriteLine("Exiting application...");
            return true;
        }
    }
}
