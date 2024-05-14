using dot_net_task_form_generation.Models;
using dot_net_task_form_generation.Models.Creds;
using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;
using dot_net_task_form_generation.Services.IServices;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace dot_net_task_form_generation.Services
{
    public class CandidateInfoService : ICandidateInfoService
    {
        /// <summary>
        /// Class Level Variable Declaration
        /// </summary>
        private readonly CosmosDbCreds _dbCreds;
        private readonly Database _database;
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly RestResponse _response;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public CandidateInfoService(IOptions<CosmosDbCreds> dbCreds)
        {
            _dbCreds = dbCreds.Value;
            _cosmosClient = new(_dbCreds.CosmosEndpointUri, _dbCreds.CosmosAuthKey);
            _database = _cosmosClient.GetDatabase(_dbCreds.DatabaseId);
            _container = _database.GetContainer(_dbCreds.ContainerId);
            _response = new RestResponse();
        }

        /// <summary>
        /// Get All Candidates
        /// </summary>
        /// <returns>Response</returns>
        public async Task<RestResponse> GetAllCandidatesInfoAsync()
        {
            var candidatesInfo = new List<CandidatesInfo>();
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.PartitionKey = 'CandidateInfo'");
                var iterator = _container.GetItemQueryIterator<CandidatesInfo>(query);
                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    candidatesInfo.AddRange(response);
                }
                _response.isSuccess = true;
                _response.Message = "Candidate Info Successfully Fetched.";
                _response.Data = candidatesInfo;
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
        /// <returns>RestResponse</returns>
        public async Task<RestResponse> CreateCandidateAsync(CandidatesInfoDto candidateDto)
        {
            try
            {
                var candidate = new CandidatesInfo
                {
                    id = Guid.NewGuid(),
                    FirstNameEnabled = candidateDto.FirstNameEnabled ? true : true,
                    LastNameEnabled = candidateDto.LastNameEnabled ? true : true,
                    EmailEnabled = candidateDto.EmailEnabled ? true : true,
                    CurrentResidenceEnabled = candidateDto.CurrentResidenceEnabled,
                    DOBEnabled = candidateDto.DOBEnabled,
                    GenderEnabled = candidateDto.GenderEnabled,
                    IDNumberEnabled = candidateDto.IDNumberEnabled,
                    NationalityEnabled = candidateDto.NationalityEnabled,
                    PhoneNumberEnabled = candidateDto.PhoneNumberEnabled,
                    PartitionKey = "CandidateInfo",
                    IsActive = true
                };

                await _container.CreateItemAsync(candidate);
                _response.isSuccess = true;
                _response.Message = "Successfully Candidate Personal Info Saved.";
                _response.Data = candidate;
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
        /// This method is for Candidate Apply
        /// </summary>
        /// <param name="candidateApplyDto">This DTO holds all Info i.e. Personal Info, Questions, Answers and etc</param>
        /// <returns>RestResponse</returns>
        public async Task<RestResponse> CandidateApplyAsync(CandidateApplyDto candidateApplyDto)
        {
            try
            {
                var candidateApply = new CandidateApply
                {
                    id = Guid.NewGuid(),
                    AnswerDtoList = candidateApplyDto.AnswerDtoList,
                    CandidatePersonalInfo = candidateApplyDto.CandidatePersonalInfo,
                    PartitionKey = "CandidateApply",
                    IsActive = true
                };
                await _container.CreateItemAsync(candidateApply);
                _response.isSuccess = true;
                _response.Message = "Successfully Applied.";
                _response.Data = candidateApplyDto;
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
