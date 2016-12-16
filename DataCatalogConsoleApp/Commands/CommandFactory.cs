using DataCatalogConsoleApp.Common;
using System;

namespace DataCatalogConsoleApp.Commands
{
    public static class CommandFactory
    {
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
