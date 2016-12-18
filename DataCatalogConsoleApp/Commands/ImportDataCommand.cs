using DataCatalogCommon.Validators;
using DataCatalogConsoleApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using DataCatalogApi.Common;
using DataCatalogApi.Models;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DataCatalogCommon.Domain.Enums;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// The import data command takes input from the user for importing
    /// data into the application via flat files. The input is prompted for
    /// and validated and then each record is read, validated, and pushed to
    /// the database catalog using the REST API.
    /// </summary>
    public class ImportDataCommand : AbstractCommand
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ImportDataCommand()
        {
            // Add base information to the command
            Name = "Data Catalog Import";
            Description = "Imports person data into the catalog for access later.";
            ExecutionCommand = ConsoleApplicationConstants.IMPORT_DATA_COMMAND_EXE_KEY;

            // Add the file path and name parameter
            IInputParameter filePathAndNameParam = new InputParameter("Enter the file path and name to import (i.e. C:\\DataCatalog\\myData.txt):");
            IValidator filePathValidation = new FilePathValidator();
            filePathAndNameParam.AddValidation(filePathValidation);
            AddInput(ConsoleApplicationConstants.FILE_NAME_AND_PATH_INPUT_PARAMETER_NAME, filePathAndNameParam);

            // Add the record delimeter parameter
            IInputParameter recordDelimParam = new InputParameter("Enter the record delimiter: pipe, comma, or space are valid:");
            IValidator recordDelimValidator = new MatchValidator("|, ");
            recordDelimParam.AddValidation(recordDelimValidator);
            AddInput(ConsoleApplicationConstants.RECORD_DELIMITER_INPUT_PARAMETER_NAME, recordDelimParam);
        }

        /// <summary>
        /// Main logic method for the command which writes to the backend database
        /// via the REST API. This method will perform a POST call for each record and will
        /// not stop if an individual record fails.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Execute()
        {
            string file;
            string delimiter;
            string[] arrFields;
            int lineIndex = 1;

            try
            {
                // Setup the rest service client
                restClient = GetDataCatalogHttpClient(DataCatalogApiConstants.DATA_CATALOG_API_PERSON_ENDPOINT);

                // Setup the Console
                Console.Clear();
                Console.WriteLine(ExecutionCommand + " - " + Name);
                Console.WriteLine(Description);
                
                // Request input from the user
                GetInputParameters();

                // Get the inputs needed to process the data
                file = Inputs[ConsoleApplicationConstants.FILE_NAME_AND_PATH_INPUT_PARAMETER_NAME].Input;
                delimiter = Inputs[ConsoleApplicationConstants.RECORD_DELIMITER_INPUT_PARAMETER_NAME].Input;

                // Read each record in the file
                foreach (string record in File.ReadLines(file))
                {
                    // Splice the fields into an array using the delimiter
                    arrFields = record.Split(delimiter[0]);

                    // Create the model for the record
                    PersonModel model = new PersonModel(arrFields[0], //First Name 
                                                        arrFields[1], //Last Name
                                                        arrFields[2], //Gender
                                                        arrFields[3], //Favorite Color
                                                        arrFields[4]); // Birth Date

                    // Serialize the model and attempt a post
                    string serializedContent = serializer.Serialize(model);
                    HttpContent httpContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponse = await restClient.PostAsync(restClient.BaseAddress.ToString(), httpContent);

                    // Output errors to the console so action can be taken by the user
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine(string.Format("Unable to process record at line {0}", lineIndex));
                    }

                    // Increment the line index so we can provide it to the user in case of an exception
                    lineIndex++;
                }

                // Print the results of the data that has been loaded
                await PrintOutput();
            }
            catch(ApplicationException aExcp)
            {
                // Handle an Application Exception being thrown while a Person is created in case of invalid data
                Console.WriteLine("The file could not be processed due to an invalid record at line " + lineIndex);
                string detailedAppMessage = string.Format("The record could not be parsed due to: {0}", aExcp.Message);
                Console.WriteLine(detailedAppMessage);
                logger.Error(detailedAppMessage);
            }
            catch(FileNotFoundException fExcp)
            {
                // Handle a exception if the file cannot be accessed
                Console.WriteLine("The file could not be found and processed, please select a different file for import");
                string detailedExcpMessage = string.Format("The file could not be processed due to: {0}", fExcp.Message);
                logger.Error(detailedExcpMessage);
            }
            catch(Exception excp)
            {
                // Handle a general exception
                Console.WriteLine("The file could not be processed due to unhandled exception, please check with your system administrator");
                string detailedExcpMessage = string.Format("The file could not be processed due to: {0}", excp.Message);
                logger.Error(detailedExcpMessage);
            }

            return ExitApp;
        }

        /// <summary>
        /// Prints the output of all people entered in the system
        /// using three different sorting requirements by calling out to the REST
        /// api through a GET method.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> PrintOutput()
        {
            // Print the list of people that have been added based on the ordering requirements
            restClient = GetDataCatalogHttpClient(DataCatalogApiConstants.DATA_CATALOG_API_PERSON_ENDPOINT);
            HttpResponseMessage response;
            List<KeyValuePair<string, string>> restCallsToMake = new List<KeyValuePair<string, string>>();
            restCallsToMake.Add(new KeyValuePair<string, string>("Gender", "Gender,LastName"));
            restCallsToMake.Add(new KeyValuePair<string, string>("Birth Date", "BirthDate"));
            restCallsToMake.Add(new KeyValuePair<string, string>("Last Name", "-LastName"));

            foreach (KeyValuePair<string, string> callToMake in restCallsToMake)
            {
                response = await restClient.GetAsync(string.Format("{0}{1}{2}",
                                                     restClient.BaseAddress.ToString(),
                                                     "?sort=",callToMake.Value));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    var people = JsonConvert.DeserializeObject<List<PersonModel>>(responseData);
                    printPeople(people, callToMake.Key);
                }
            }

            return true;
        }

        /// <summary>
        /// Internal method to this class which prints the people out with
        /// information pertaining to how the result set is ordered.
        /// </summary>
        /// <param name="people"></param>
        /// <param name="order"></param>
        private void printPeople(List<PersonModel> people, string order)
        {
            // Iterate through the list of people and print the properties
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(string.Format("People Ordered By {0}", order));
            Console.WriteLine("First Name, Last Name, Gender, Favorite Color, Birth Date");
            Console.WriteLine("_________________________________________________________");
            foreach (var person in people)
            {
                DateTime dt = DateTime.Parse(person.BirthDate);
                string gend = Enum.GetName(typeof(Gender), Convert.ToInt32(person.Gender));
                Console.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}",
                                        person.FirstName,
                                        person.LastName,
                                        gend,
                                        person.FavoriteColor,
                                        dt.ToString("M/d/yyyy")));
            }
            Console.WriteLine("__________________________________________________________");
        }
    }
}
