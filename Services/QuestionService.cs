using dot_net_task_form_generation.Models;
using dot_net_task_form_generation.Models.Creds;
using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;
using dot_net_task_form_generation.Services.IServices;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

#pragma warning disable

namespace dot_net_task_form_generation.Services
{
    public class QuestionService : IQuestionService
    {
        /// <summary>
        /// Class Level Variable Declaration
        /// </summary>
        private readonly IMemoryCache _memoryCache;
        private readonly RestResponse _response;
        private readonly CosmosDbCreds _dbCreds;
        private readonly Database _database;
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryCache"></param>
        public QuestionService(IMemoryCache memoryCache, IOptions<CosmosDbCreds> dbCreds)
        {
            _memoryCache = memoryCache;
            _dbCreds = dbCreds.Value;
            _cosmosClient = new(_dbCreds.CosmosEndpointUri, _dbCreds.CosmosAuthKey);
            _database = _cosmosClient.GetDatabase(_dbCreds.DatabaseId);
            _container = _database.GetContainer(_dbCreds.ContainerId);
            _response = new RestResponse();
        }

        /// <summary>
        /// Get All Questions
        /// </summary>
        /// <returns></returns>
        public async Task<RestResponse> GetAllQuestionsAsync()
        {

            var questions = new List<Questions>();
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.PartitionKey = 'Question'");
                var iterator = _container.GetItemQueryIterator<Questions>(query);
                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    questions.AddRange(response);
                }
                _response.isSuccess = true;
                _response.Message = "Questions Successfully Fetched.";
                _response.Data = questions;
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
        /// This Method Adds Question to Cache
        /// </summary>
        /// <param name="questionsDto"></param>
        /// <returns></returns>
        public async Task<RestResponse> AddQuestionToCache(QuestionsDto questionsDto)
        {
            try
            {
                if (questionsDto != null && !string.IsNullOrWhiteSpace(questionsDto.QuestionText))
                {
                    var cacheKey = "Questions";
                    var choices = questionsDto.EnableOtherOption ? questionsDto.Choices.Concat(new[] { "Other" }).ToList() : questionsDto.Choices;
                    if (!_memoryCache.TryGetValue(cacheKey, out List<Questions> questions))
                    {
                        questions = new List<Questions>();
                    }
                    Questions question = new()
                    {
                        id = Guid.NewGuid(),
                        QuestionText = questionsDto.QuestionText,
                        Type = questionsDto.Type,
                        Choices = choices,
                        EnableOtherOption = questionsDto.EnableOtherOption,
                        PartitionKey = "Question",
                        IsActive = true
                    };
                    questions.Add(question);
                    _memoryCache.Set(cacheKey, questions, TimeSpan.FromMinutes(30));
                    _response.isSuccess = true;
                    _response.Message = "Question Successfully Added to Cache";
                    _response.Data = questions;
                }
                else
                {
                    _response.isSuccess = false;
                    _response.Message = "NULL NOT ALLOWED";
                    _response.Data = "Please Fill the Question";
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

        /// <summary>
        /// Stores questions to db
        /// </summary>
        /// <returns></returns>
        public async Task<RestResponse> CreateQuestionAsync()
        {
            try
            {
                // Get questions from memory cache
                var cacheKey = "Questions";
                if (_memoryCache.TryGetValue(cacheKey, out List<Questions> questions))
                {
                    if (questions != null)
                    {
                        foreach (var question in questions)
                        {
                            await _container.CreateItemAsync(question);
                        }
                        _response.isSuccess = true;
                        _response.Message = "Questions Successfully Saved";
                        _response.Data = questions;
                    }
                    else
                    {
                        _response.isSuccess = true;
                        _response.Message = "Cache does not exist or is empty";
                        _response.Data = null;
                    }
                }
                else
                {
                    _response.isSuccess = true;
                    _response.Message = "Cache does not exist or is empty";
                    _response.Data = null;
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

        /// <summary>
        /// Get Cache Data for Questions
        /// </summary>
        /// <returns></returns>
        public List<Questions> GetQuestionsFromCache()
        {
            var cacheKey = "Questions";

            if (_memoryCache.TryGetValue(cacheKey, out List<Questions> questions))
            {
                return questions;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Delete the question from cache using index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<RestResponse> DeleteCacheDataByIndex(int index)
        {
            try
            {
                var cacheKey = "Questions";
                if (_memoryCache.TryGetValue(cacheKey, out List<Questions> questions))
                {
                    if (index >= 0 && index < questions.Count)
                    {
                        questions.RemoveAt(index);
                        _memoryCache.Set(cacheKey, questions);
                        _response.isSuccess = true;
                        _response.Message = $"Data at index {index} successfully removed from cache";
                        _response.Data = questions;
                    }
                    else
                    {
                        _response.isSuccess = false;
                        _response.Message = $"Invalid index {index}. Index must be within the range of 0 to {questions.Count - 1}";
                        _response.Data = null;
                    }
                }
                else
                {
                    _response.isSuccess = false;
                    _response.Message = "Cache does not exist or is empty";
                    _response.Data = null;
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

        /// <summary>
        /// Update the question in cache using index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="updatedQuestion"></param>
        /// <returns></returns>
        public async Task<RestResponse> UpdateQuestionByIndex(int index, QuestionsDto updatedQuestion)
        {
            try
            {
                var cacheKey = "Questions";
                if (_memoryCache.TryGetValue(cacheKey, out List<Questions> questions))
                {
                    if (index >= 0 && index < questions.Count)
                    {
                        questions[index].QuestionText = updatedQuestion.QuestionText;
                        questions[index].Type = updatedQuestion.Type;
                        questions[index].Choices = updatedQuestion.Choices;
                        questions[index].EnableOtherOption = updatedQuestion.EnableOtherOption;

                        _memoryCache.Set(cacheKey, questions);

                        _response.isSuccess = true;
                        _response.Message = $"Question at index {index} successfully updated in cache";
                        _response.Data = questions;
                    }
                    else
                    {
                        _response.isSuccess = false;
                        _response.Message = $"Invalid index {index}. Index must be within the range of 0 to {questions.Count - 1}";
                        _response.Data = null;
                    }
                }
                else
                {
                    _response.isSuccess = false;
                    _response.Message = "Cache does not exist or is empty";
                    _response.Data = null;
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
