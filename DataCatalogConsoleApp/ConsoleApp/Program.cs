using DataCatalogConsoleApp.Commands;
using DataCatalogConsoleApp.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.ConsoleApp
{
    /// <summary>
    /// Main program class to launch the console application for the Data Catalog application
    /// </summary>
    public class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            //General start up work for the application
            AppStart();
            ICommand commandToExecute = null;
            bool exitApp = false;

            try
            {
                //Get the active commands once and reuse them for each execution
                IList<ICommand> commands = GetActiveCommands();
                Task<bool> commandResult = null;

                //Continue going until the user selects exits or closes the window
                while (!exitApp)
                {
                    //Generate the menu and prompt user for selection
                    int itemSelected = -1;
                    //if (commandResult == null || commandResult.IsCompleted)
                    //{
                        itemSelected = GetConsoleMenu(commands);
                    //}

                    //Get the command for the item selected and execute it
                    commandToExecute = commands[itemSelected];
                    commandResult = commandToExecute.Execute();

                    exitApp = commandResult.Result;
                }
            }
            catch (Exception excp)
            {
                if(commandToExecute != null)
                {
                    commandToExecute.PrintError(excp);
                    logger.Log(LogLevel.Error, "Error occurred while executing the command");
                }
                else
                {
                    Console.WriteLine("A fatal error occurred in the application, please contact your system administrator.");
                    logger.Log(LogLevel.Fatal, "The console app experienced an unrecoverable error");
                }
            }
        }

        /// <summary>
        /// This method will get run once at startup of the console and print basic
        /// introductory information about the app and setup any data that is needed for use
        /// later on.
        /// </summary>
        static void AppStart()
        {
            //Write the header of the application
            logger.Log(LogLevel.Info, "Starting Data Catalog console application");
            Console.WriteLine("********************************************************");
            Console.WriteLine("****************** WELCOME TO THE **********************");
            Console.WriteLine("************* DATA CATALOG APPLICATION *****************");
            Console.WriteLine("********************************************************");
        }

        /// <summary>
        /// Get a list of the active commands available for the console application.
        /// </summary>
        /// <returns>List of ICommand objects</returns>
        static IList<ICommand> GetActiveCommands()
        {
            //Get an array of active commands from the configuration file
            var activeCommandsConfigValue = ConfigurationManager.AppSettings[ConsoleApplicationConstants.ACTIVE_COMMANDS_APP_SETTING_KEY];

            if(string.IsNullOrEmpty(activeCommandsConfigValue))
            {
                logger.Log(LogLevel.Error, "No active commands available, check to make sure you have the " 
                    + ConsoleApplicationConstants.ACTIVE_COMMANDS_APP_SETTING_KEY 
                    + " setup within your app.config file correctly");
                throw new ApplicationException("No app configuration setup for active commands");
            }

            var arrActiveCommandKeys = activeCommandsConfigValue.Split(',');

            //Iterate through each command key and use the factory to create it
            IList<ICommand> arrActiveCommands = new List<ICommand>();
            foreach(var key in arrActiveCommandKeys)
            {
                if(key.Length > 1)
                {
                    //All keys must be 1 character if not an exception will be thrown
                    logger.Log(LogLevel.Error, "Unable to process app setting command " 
                        + key 
                        + " due to character length greater than 1");
                    throw new ApplicationException("Active command key " + key
                        + " is invalid with more than one key, app settings need to be updated");
                }   
                else
                {
                    //Otherwise create the command object and add it to the array
                    arrActiveCommands.Add(CommandFactory.Create(key[0]));
                } 
            }

            return arrActiveCommands;
        }

        /// <summary>
        /// This method builds the console menu based on a list of available
        /// commands that are passed in. The menu allows the user to use the cursor
        /// to move up and down through menu items and make a selection.
        /// </summary>
        /// <param name="menuItems">List of ICommands to be included in the menu</param>
        /// <returns>The index of the item selected</returns>
        static int GetConsoleMenu(IList<ICommand> menuItems)
        {
            bool menuItemSelected = false;
            int top = Console.CursorTop;
            int bottom = 0;
            int selectedMenuItemIndex = 0;
            ConsoleKeyInfo keyPressed;

            while (!menuItemSelected)
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (i == selectedMenuItemIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i].ExecutionCommand + ". " + menuItems[i].Name);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i].ExecutionCommand + ". " + menuItems[i].Name);
                    }
                }

                keyPressed = Console.ReadKey(true);

                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedMenuItemIndex > 0)
                        {
                            selectedMenuItemIndex--;
                        }
                        else
                        {
                            selectedMenuItemIndex = (menuItems.Count - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedMenuItemIndex < (menuItems.Count - 1))
                        {
                            selectedMenuItemIndex++;
                        }
                        else
                        {
                            selectedMenuItemIndex = 0;
                        }
                        break;

                    case ConsoleKey.Enter:
                        menuItemSelected = true;
                        break;

                    default:
                        //Do nothing
                        break;
                }

                //Move the cursor to the top
                Console.SetCursorPosition(0, top);
            }

            //set the cursor after the menu so it can continue
            Console.SetCursorPosition(0, bottom);
            Console.CursorVisible = true;

            return selectedMenuItemIndex;
        }
    }
}
