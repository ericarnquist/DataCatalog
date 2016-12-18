using DataCatalogConsoleApp.Commands;
using DataCatalogConsoleApp.Common;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataCatalogConsoleApp.Tests
{
    [TestFixture]
    public class TestCommands
    {
        [Test]
        public void TestGetInputsExitCommand()
        {
            ICommand actual = CommandFactory.Create('E');
            IDictionary<string, IInputParameter> inputs = actual.Inputs;
            Assert.IsNotNull(inputs);
            Assert.AreEqual(inputs.Count, 0);
        }

        [Test]
        public void TestGetInputsImportCommand()
        {
            ICommand actual = CommandFactory.Create('I');
            IDictionary<string, IInputParameter> inputs = actual.Inputs;
            Assert.IsNotNull(inputs);
            Assert.AreEqual(inputs.Count, 2);
            Assert.IsTrue(inputs.ContainsKey(ConsoleApplicationConstants.FILE_NAME_AND_PATH_INPUT_PARAMETER_NAME));
            Assert.IsTrue(inputs.ContainsKey(ConsoleApplicationConstants.RECORD_DELIMITER_INPUT_PARAMETER_NAME));
        }

        [Test]
        public void TestAddInputToCommand()
        {
            ICommand actualCommand = CommandFactory.Create('I');
            IInputParameter actualParameter = new InputParameter("My Test Input");
            actualCommand.AddInput("testInput", new InputParameter("My Test Input"));

            Assert.IsTrue(actualCommand.Inputs.ContainsKey("testInput"));
            Assert.AreEqual(actualCommand.Inputs["testInput"].Prompt, actualParameter.Prompt);
        }
    }
}
