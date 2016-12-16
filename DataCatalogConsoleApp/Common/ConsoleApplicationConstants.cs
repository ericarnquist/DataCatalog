using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Common
{
    public static class ConsoleApplicationConstants
    {
        //Command key constants
        public const char IMPORT_DATA_COMMAND_EXE_KEY = 'I';
        public const char EXIT_CONSOLE_COMMAND_EXE_KEY = 'E';

        //App setting keys
        public const string ACTIVE_COMMANDS_APP_SETTING_KEY = "ActiveConsoleCommands";

        //Input parameter names
        public const string FILE_NAME_AND_PATH_INPUT_PARAMETER_NAME = "filePathAndName";
        public const string RECORD_DELIMITER_INPUT_PARAMETER_NAME = "recordDelimiter";
    }
}
