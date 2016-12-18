using DataCatalogConsoleApp.Common;
using System;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Concrete factory used for generating concrete 
    /// implementation classes of the ICommand interface.
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Instantiates an ICommand based on the command
        /// key passed into the call. If the command is not available this method
        /// will throw an ApplicationException which should be handled.
        /// </summary>
        /// <param name="commandKey">Single character key of the command to create</param>
        /// <returns></returns>
        public static ICommand Create(char commandKey)
        {
            ICommand commandToCreate;

            switch (commandKey)
            {
                case (ConsoleApplicationConstants.IMPORT_DATA_COMMAND_EXE_KEY):
                    commandToCreate = new ImportDataCommand();
                    break;
                case (ConsoleApplicationConstants.EXIT_CONSOLE_COMMAND_EXE_KEY):
                    commandToCreate = new ExitConsoleCommand();
                    break;
                default:
                    throw new ApplicationException("Command is not available for application");
            }

            return commandToCreate;
        }
    }
}
