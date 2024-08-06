using Newtonsoft.Json;
using PIHelperSh.AI.Core.Interfaces;
using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.Core.Models.ImageGenerator;
using PIHelperSh.AI.Kandinsky.Enums;
using PIHelperSh.AI.Kandinsky.Models.Internal;
using PIHelperSh.Core.Extentions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PIHelperSh.AI.Kandinsky
{
    public class KandinskyAIImageGenerator : ICloudAIImageGenerator
    {
        private readonly KandinskyAIContext _context;

        private List<KandinskyModelDTO>? _models;

        private readonly KandinskyStyles _style;
        private RestClient RestClient { get; set; }

        internal KandinskyAIImageGenerator(KandinskyAIContext cloudAIContext, KandinskyStyles style)
        {
            _context = cloudAIContext;

            var tmp = _context.GetProxy(false);
            if (tmp != null)
                RestClient = new RestClient(new RestClientOptions()
                {
                    Proxy = tmp.CreateProxy()
                });
            else
                RestClient = new RestClient();
            _style = style;
        }

        public async Task<AIImageGeneratorResponce> GetImage(AIImageGeneratorResponce image)
        {
            KandinskyImageResponceDTO? dto = null;
            bool flag = false;
           
            if (!image.Id.IsNullOrEmpty() && image.Id != Guid.Empty.ToString())
            {
                while (dto == null || dto.status != "DONE")
                {
                    if (flag) 
                    {
                        await Task.Delay(500);
                    }
                    else
                        flag = true;

                    var request = CreateBaseRequest($"text2image/status/{image.Id}");

                    var responce = await RestClient.GetAsync(request);

                    dto = JsonConvert.DeserializeObject<KandinskyImageResponceDTO>(responce.Content);
                }

                return new()
                {
                    Id = dto.uuid.ToString(),
                    Image = (dto.censored.HasValue && !dto.censored.Value)? Base64ToImage(dto.images[0]) : null,
                };
            }
            return new();
        }

        public async Task<AIImageGeneratorResponce> GetImage(AIPromtForImage promt)
        {
            var res = await RequestGeneration(promt);
            return await GetImage(res);
        }

        public async Task<AIImageGeneratorResponce> RequestGeneration(AIPromtForImage promt)
        {
            var model = await GetModel();
            if(model != null)
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, $"{_context.ApiUrl}/text2image/run");
                request.Headers.Add("X-Key", $"Key {_context.ApiKey}");
                request.Headers.Add("X-Secret", $"Secret {_context.SecretKey}");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(GenerateModel(promt),Encoding.UTF8, new MediaTypeHeaderValue("application/json")), "params");
                content.Add(new StringContent(model.id.ToString()), "model_id");
                request.Content = content;

                var response = await client.SendAsync(request);

                var str = await response.Content.ReadAsStringAsync();

                var dto =  JsonConvert.DeserializeObject<KandinskyImageResponceDTO>(str);

                return new()
                {
                    Id = dto?.uuid.ToString() ?? "",
                };
            }

            return new AIImageGeneratorResponce();
        }

        private Stream Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            return new MemoryStream(imageBytes, 0, imageBytes.Length);
        }

        private string GenerateModel(AIPromtForImage promt)
        {
            return JsonConvert.SerializeObject( new KandinskyGenerateRequestDTO()
            {
                type = KandinskyRequestType.Generate.GetValue<string>(),
                num_images = 1,
                style = _style.GetValue<string>(),
                height = promt.Height,
                width = promt.Width,
                negativePromptUnclip = promt.WithoutText,
                generateParams = new()
                {
                    {"query",promt.Text }
                },
            });
        }

        private async Task<KandinskyModelDTO?> GetModel()
        {
            if (_models == null)
                _models = await GetModels();

            return _models?.FirstOrDefault();
        }

        private async Task<List<KandinskyModelDTO>?> GetModels()
        {
            var request = CreateBaseRequest("models");

            var responce = await RestClient.GetAsync(request);

            return responce.Content != null ? JsonConvert.DeserializeObject<List<KandinskyModelDTO>>(responce.Content) : null;
        }

        private RestRequest CreateBaseRequest(string url)
        {
            var request = new RestRequest($"{_context.ApiUrl}/{url}");
            request.AddHeader("X-Key", $"Key {_context.ApiKey}");
            request.AddHeader("X-Secret", $"Secret {_context.SecretKey}");
            return request;
        }

        public void Dispose()
        {
            RestClient.Dispose();
        }
    }
}
