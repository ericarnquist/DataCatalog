using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Standard contract to define all commands to be implemented
    /// and used by the Data Catalog console application
    /// </summary>
    public interface ICommand
    {
        // Property methods
        string Name { get; set; }
        string Description { get; set; }
        char ExecutionCommand { get; set; }
        bool ExitApp { get; set; }
        IDictionary<string, IInputParameter> Inputs { get; }

        // Behavior methods
        void AddInput(string inputName, IInputParameter input);
        Task<bool> Execute();
        Task<bool> PrintOutput();
        void PrintError(Exception excp);
        void GetInputParameters();
    }
}
