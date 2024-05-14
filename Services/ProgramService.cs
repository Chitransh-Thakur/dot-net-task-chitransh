using dot_net_task_form_generation.Models;
using dot_net_task_form_generation.Models.Creds;
using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;
using dot_net_task_form_generation.Services.IServices;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace dot_net_task_form_generation.Services
{
    public class ProgramService : IProgramService
    {
        /// <summary>
        /// Class Level Variable Declaration
        /// </summary>
        private readonly CosmosDbCreds _dbCreds;
        private readonly Container _container;
        private readonly Database _database;
        private readonly CosmosClient _cosmosClient;
        private readonly RestResponse _response;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public ProgramService(IOptions<CosmosDbCreds> dbCreds)
        {
            _dbCreds = dbCreds.Value;
            _cosmosClient = new(_dbCreds.CosmosEndpointUri, _dbCreds.CosmosAuthKey);
            _database = _cosmosClient.GetDatabase(_dbCreds.DatabaseId);
            _container = _database.GetContainer(_dbCreds.ContainerId);
            _response = new RestResponse();
        }

        /// <summary>
        /// Get All Programs
        /// </summary>
        /// <returns></returns>
        public async Task<RestResponse> GetAllProgramsAsync()
        {
            var programs = new List<Programs>();
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.PartitionKey = 'Program'");
                var iterator = _container.GetItemQueryIterator<Programs>(query);
                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    programs.AddRange(response);
                }
                _response.isSuccess = true;
                _response.Message = "Program Info Successfully Fetched.";
                _response.Data = programs;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = "Something Went Wrong!!";
                _response.Data = ex;
            }
            return _response;
        }

        /// <summary>
        /// This method stores the program info
        /// </summary>
        /// <param name="programDto"></param>
        /// <returns></returns>
        public async Task<RestResponse> CreateProgramAsync(ProgramsDto programDto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(programDto.Title) && !string.IsNullOrWhiteSpace(programDto.Description))
                {
                    var program = new Programs
                    {
                        id = Guid.NewGuid(),
                        Title = programDto.Title,
                        Description = programDto.Description,
                        PartitionKey = "Program",
                        IsActive = true
                    };
                    await _container.CreateItemAsync(program);
                    _response.isSuccess = true;
                    _response.Message = "Successfully Program Info Saved.";
                    _response.Data = program;
                }
                else
                {
                    _response.isSuccess = false;
                    _response.Message = "NULL NOT ALLOWED";
                    _response.Data = "Please Fill Title and Description to describe about the program";
                }
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = "Something Went Wrong!!";
                _response.Data = ex;
            }
            return _response;
        }
    }
}
