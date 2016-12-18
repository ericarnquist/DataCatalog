using DataCatalogCommon.Validators;
using NUnit.Framework;

namespace DataCatalogCommon.Tests
{
    [TestFixture]
    public class TestValidators
    {
        [Test]
        public void TestMatchValidatorValid()
        {
            IValidator validator = new MatchValidator("|, ");
            validator.ObjectToValidate = "|";
            bool expected = true;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMatchValidatorInvalid()
        {
            IValidator validator = new MatchValidator("|, %*#()");
            validator.ObjectToValidate = "?";
            bool expected = false;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMatchValidatorObjectTypeInvalid()
        {
            IValidator validator = new MatchValidator("|, %*#()");
            validator.ObjectToValidate = 5000;
            bool expected = false;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual("The path provided was of an unsupported type please provide a different type of path", validator.Message);
        }

        [Test]
        public void TestMatchValidatorObjectTooLong()
        {
            IValidator validator = new MatchValidator("|, ");
            validator.ObjectToValidate = "||";
            bool expected = false;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFilePathValidatorValid()
        {
            string path = "C:\\Development\\DataCatalog\\ImportDataPipe.txt";
            IValidator validator = new FilePathValidator(path);
            bool expected = true;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFilePathValidatorPathInvalid()
        {
            string path = "C:\\ThisPath:IsInvalid\\ImportDataPipe.txt";
            IValidator validator = new FilePathValidator(path);
            bool expected = false;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFilePathValidatorPathTooLong()
        {
            string path = "C:\\ThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolongThispathisvalidbuttoolong\\ImportDataPipe.txt";
            IValidator validator = new FilePathValidator(path);
            bool expected = false;
            bool actual = validator.Validate();
            Assert.AreEqual(expected, actual);
        }
    }
}
