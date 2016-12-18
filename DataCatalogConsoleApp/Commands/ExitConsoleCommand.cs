using DataCatalogConsoleApp.Commands;
using DataCatalogConsoleApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Exit command which writes out to the user and provides
    /// a response back so the console application can quit.
    /// </summary>
    public class ExitConsoleCommand : AbstractCommand
    {
        /// <summary>
        /// Default constructor setting the Name, Description, and ExecutionCommand.
        /// </summary>
        public ExitConsoleCommand()
        {
            Name = "Exit Data Catalog Console";
            Description = "This command exits the console application";
            ExecutionCommand = ConsoleApplicationConstants.EXIT_CONSOLE_COMMAND_EXE_KEY;
        }

        /// <summary>
        /// Sets the ExitApp to true and prints any output needed for the
        /// end user.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Execute()
        {
            ExitApp = true;
            await PrintOutput();
            return ExitApp;
        }

        /// <summary>
        /// Print standard output just before exiting the application.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> PrintOutput()
        {
            Console.Clear();
            Console.WriteLine("Exiting application...");
            return true;
        }
    }
}
