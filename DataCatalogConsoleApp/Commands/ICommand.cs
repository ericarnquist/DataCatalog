using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Standard contract defining all commands to be implemented
    /// and used by the console application.
    /// </summary>
    public interface ICommand
    {
        #region Properties
        /// <summary>
        /// Name of the command.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Description of what the command does, used for presentation
        /// to the end user through the console application.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Character that is used to generate the command and can be used
        /// to kick it off through the console application.
        /// </summary>
        char ExecutionCommand { get; set; }

        /// <summary>
        /// Boolean value to identify if the implementing command should exit
        /// the console application after completion.
        /// </summary>
        bool ExitApp { get; set; }

        /// <summary>
        /// Dictionary of input parameters with the key being the name of the
        /// parameter for efficient access.
        /// </summary>
        IDictionary<string, IInputParameter> Inputs { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new input to the command via the Dictionary.
        /// </summary>
        /// <param name="inputName">Key to store the parameter in the Dictionary</param>
        /// <param name="input">InputParameter to be stored as the value in the Dictionary</param>
        void AddInput(string inputName, IInputParameter input);

        /// <summary>
        /// Primary execution method which includes the core logic of the command which can be
        /// run asynchronously.
        /// </summary>
        /// <returns>true if successful</returns>
        Task<bool> Execute();

        /// <summary>
        /// Standard method for printing output after successful execution of the command.
        /// </summary>
        /// <returns>true if successful</returns>
        Task<bool> PrintOutput();

        /// <summary>
        /// Standard method for printing an error to the console upon failure during
        /// execution of the command.
        /// </summary>
        /// <param name="excp"></param>
        void PrintError(Exception excp);

        /// <summary>
        /// Runs through defined input parameters and requests input from the end user
        /// during execution of the command.
        /// </summary>
        void GetInputParameters();
        #endregion
    }
}
