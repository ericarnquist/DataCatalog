using System;
using System.Collections.Generic;
using NLog;
using System.Net.Http;
using DataCatalogApi.Common;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace DataCatalogConsoleApp.Commands
{
    /// <summary>
    /// Abstract implementation of the ICommand interface includes
    /// base properties, logging facility and method implementations
    /// which are common across all concrete commands.
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        //Common properties of commands
        public string Description { get; set; }
        public char ExecutionCommand { get; set; }
        public string Name { get; set; }
        public bool ExitApp { get; set; } = false; //By default do not exit
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        protected HttpClient restClient;
        protected JavaScriptSerializer serializer = new JavaScriptSerializer();

        //Parameter inputs for use during execution
        private IDictionary<string, IInputParameter> _inputs;
        public IDictionary<string, IInputParameter> Inputs
        {
            get
            {
                if(_inputs == null)
                { 
                    _inputs = new Dictionary<string, IInputParameter>();
                }
                return _inputs;
            }
        }

        /// <summary>
        /// Default implementation for the PrintError method off of ICommand
        /// which can be called when an error occurs during execution of the
        /// command.
        /// </summary>
        /// <param name="excp">Exception caught during execution</param>
        public void PrintError(Exception excp)
        {
            Console.WriteLine(string.Format("An error occurred in {0} with the following message.", Name));
            Console.WriteLine(string.Format("{0} {1}", excp.Source, excp.Message));
        }

        /// <summary>
        /// Add an input parameter to the dictionary structure
        /// </summary>
        /// <param name="inputName">Name of the parameter used to store and retrieve</param>
        /// <param name="input">Instance of the IInputParameter implementation</param>
        public void AddInput(string inputName, IInputParameter input)
        {
            this.Inputs.Add(inputName, input);
        }

        /// <summary>
        /// Base implementation which iterates through each InputParameter
        /// attached to the command and requests the parameter from the end
        /// user through the console application. The method will continue
        /// to prompt the user until they have entered a valid input value.
        /// </summary>
        public void GetInputParameters()
        {
            // Step through each item in the Dictionary
            foreach(KeyValuePair<string, IInputParameter> parameter in Inputs)
            {
                // Set validation to false and continue until valid input is received
                bool parameterValid = false;
                while(!parameterValid)
                {
                    parameterValid = parameter.Value.GetParameter();
                }
            }
        }

        //Abstract methods for implementation by concrete classes
        public abstract Task<bool> Execute();
        public abstract Task<bool> PrintOutput();

        /// <summary>
        /// Gets an HttpClient in order to perform REST service calls
        /// to the Api.
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        protected HttpClient GetDataCatalogHttpClient(string endPoint)
        {
            // If the rest client has not been created yet or the end point has changed update it
            if (restClient == null || restClient.BaseAddress.ToString().IndexOf(endPoint) == -1)
            {
                restClient = new HttpClient();
                restClient.BaseAddress = new Uri(string.Format("{0}/{1}/{2}",
                                                                DataCatalogApiConstants.DATA_CATALOG_API_BASE_URL,
                                                                DataCatalogApiConstants.DATA_CATALOG_API_REQ_PREFIX,
                                                                endPoint));
                restClient.DefaultRequestHeaders.Accept.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            return restClient;

        }
    }
}
