using NUnit.Framework;
using System;
using DataCatalogConsoleApp.Commands;

namespace DataCatalogConsoleApp.Tests
{
    [TestFixture]
    public class TestCommandFactory
    {
        [Test]
        public void TestCreateImportCommand()
        {
            ICommand expected = new ImportDataCommand();
            ICommand actual = CommandFactory.Create('I');

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.ExecutionCommand, actual.ExecutionCommand);
            Assert.AreEqual(expected.ExitApp, actual.ExitApp);
            Assert.IsInstanceOf(typeof(ImportDataCommand), actual);
        }

        [Test]
        public void TestCreateExitCommand()
        {
            ICommand expected = new ExitConsoleCommand();
            ICommand actual = CommandFactory.Create('E');

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.ExecutionCommand, actual.ExecutionCommand);
            Assert.AreEqual(expected.ExitApp, actual.ExitApp);
            Assert.IsInstanceOf(typeof(ExitConsoleCommand), actual);
        }

        [Test]
        public void TestEmptyCommand()
        {
            Assert.Throws<ApplicationException>(() => CommandFactory.Create(' '));
        }

        [Test]
        public void TestInvalidCommand()
        {
            Assert.Throws<ApplicationException>(() => CommandFactory.Create('X'));
        }
    }
}
