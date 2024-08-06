using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PIHelperSh.AI.Core.Interfaces;
using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.GroqCloud.Models.DTOs;
using PIHelperSh.AI.GroqCloud.Models.Enums;
using PIHelperSh.AI.GroqCloud.Models.InternelModels;
using RestSharp;

namespace PIHelperSh.AI.GroqCloud
{
    /// <summary>
    /// Класс чата переписки с нейросетью
    /// </summary>
    public class GroqAIChat : ICloudAIChat
    {
        private ICloudAIContext _cloudAIContext;
        private AIRequestInternal AIRequest { get; set; }
        private RestClient RestClient { get; set; }

        /// <summary>
        /// Create chat with defined model
        /// </summary>
        internal GroqAIChat(ICloudAIContext cloudAIContext, AIModel model)
        {
            _cloudAIContext = cloudAIContext;
            AIRequest = new();
            AIRequest.Model = model;
            var tmp = _cloudAIContext.GetProxy(false);
            if (tmp != null)
                RestClient = new RestClient(new RestClientOptions()
                {
                    Proxy = tmp.CreateProxy()
                });
            else
                RestClient = new RestClient();
        }

        /// <summary>
        /// System promt (default empty)
        /// </summary>
        public AIPromt? SystemPromt
        {
            get
            {
                if (AIRequest != null)
                {
                    return AIRequest.Messages.FirstOrDefault(x => x.Role == AIMessageRole.System);
                }
                return null;
            }

            set
            {
                if (AIRequest != null)
                {
                    var message = AIRequest.Messages.FirstOrDefault(x => x.Role == AIMessageRole.System);

                    if (message != null)
                    {
                        if (value == null)
                        {
                            AIRequest.Messages.Remove(message);
                            return;
                        }

                        message.Text = value.Text;
                    }
                    else if (value != null)
                    {
                        AIRequest.Messages.Add(new()
                        {
                            Role = AIMessageRole.System,
                            Text = value.Text
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Chat model type (default not defined)
        /// </summary>
        public AIModel? MyModel
        {
            get
            {
                if (AIRequest != null)
                {
                    return AIRequest.Model;
                }
                return null;
            }

            set
            {
                if (AIRequest != null && value != null)
                {
                    AIRequest.Model = value.Value;
                }
            }
        }

        private RestRequest GenerateRequest(string jsonBody)
        {
            var restRequestData = new RestRequest(_cloudAIContext.ApiUrl);
            restRequestData.AddHeader("Authorization", $"Bearer {_cloudAIContext.ApiKey}");
            restRequestData.AddHeader("Content-Type", "application/json");

            restRequestData.AddJsonBody(jsonBody);

            return restRequestData;
        }

        private AIPromt? AnalyseResponce(RestResponse responce)
        {
            if (responce.IsSuccessful && !string.IsNullOrEmpty(responce.Content))
            {
                var responceBody = JObject.Parse(responce.Content).GetValue("choices")?.ToObject<List<AIResponceBaodyDTO>>()?.FirstOrDefault();
                if (responceBody != null)
                {
                    var res = new AIMessageInternal()
                    {
                        Role = AIMessageRole.Assistant,
                        Text = responceBody.message.content
                    };
                    AIRequest.Messages.Add(res);
                    return res;
                }
            }
            return null;
        }

        public void Dispose()
        {
            RestClient.Dispose();
        }

        public AIPromt SendToChat(AIPromt message)
        {
            return SendToChatAsync(message).Result;
        }

        public async Task<AIPromt> SendToChatAsync(AIPromt message)
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    AIRequest.Messages.Add(new()
                    {
                        Role = AIMessageRole.User,
                        Text = message.Text
                    });

                    var restRequestData = GenerateRequest(JsonConvert.SerializeObject(AIRequest.GetRequestModel));

                    var responce = await RestClient.PostAsync(restRequestData);

                    return AnalyseResponce(responce);
                }
                catch (Exception ex)
                {
                    RestClient.Dispose();
                    var tmp = _cloudAIContext.GetProxy(true);
                    if (tmp != null)
                        RestClient = new RestClient(new RestClientOptions()
                        {
                            Proxy = tmp.CreateProxy(),
                        });
                    else
                        throw;
                }
            }
            return null;
        }

        public void ClearChatHistory(bool removeSystemPromt = false)
        {
            if (removeSystemPromt)
                AIRequest.Messages.Clear();
            else
            {
                AIRequest.Messages.RemoveAll(x => x.Role != AIMessageRole.System);
            }
        }

        public void ClearChatHistoryAsync(bool removeSystemPromt = false)
        {
            ClearChatHistory(removeSystemPromt);
        }

        public AIChatHistory SaveChatHistory()
        {
            return SaveChatHistoryAsync().Result;
        }

        public Task<AIChatHistory> SaveChatHistoryAsync()
        {
            var res = new AIChatHistory();

            res.SystemPromt = AIRequest.Messages.FirstOrDefault(x => x.Role == AIMessageRole.System) ?? new();

            res.History = AIRequest.Messages.Where(x => x.Role != AIMessageRole.System).Select(x => new AIChatRecord()
            {
                Role = x.Role.ToString(),
                Promt = new() 
                {
                    Text = x.Text
                } 
            });

            return Task.FromResult(res);
        }

        public void LoadChatHistory(AIChatHistory chatHistory)
        {
            LoadChatHistoryAsync(chatHistory).Wait();
        }

        public Task LoadChatHistoryAsync(AIChatHistory chatHistory)
        {
            var currentList = new List<AIMessageInternal>();

            if (!string.IsNullOrEmpty(chatHistory.SystemPromt.Text))
                currentList.Add(new()
                {
                    Role = AIMessageRole.System,
                    Text = chatHistory.SystemPromt.Text
                });

            currentList.AddRange(chatHistory.History.Select(x => new AIMessageInternal()
            {
                Text = x.Promt.Text,
                Role = Enum.Parse<AIMessageRole>(x.Role) // todo - переделать
            }));

            AIRequest.Messages = currentList;

            return Task.CompletedTask;  
        }
    }
}
