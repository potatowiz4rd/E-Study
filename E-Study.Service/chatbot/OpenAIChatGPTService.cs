using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace E_Study.Service.chatbot
{
    public class OpenAIChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string Gpt3Endpoint = "https://api.openai.com/v1/chat/completions";

        public OpenAIChatGPTService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<string> GetChatResponse(string message)
        {
            var requestBody = new
            {
                prompt = message,
                max_tokens = 100 // Adjust this according to your requirements
            };

            var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(Gpt3Endpoint, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Process the response JSON and extract the generated text
                // The response will be in the 'choices' array under the 'text' property
                // You can use JSON serialization/deserialization or simple string manipulation to get the result.
                // For simplicity, we'll assume the JSON response contains the expected data.
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                string generatedText = data.choices[0].text;
                return generatedText;
            }

            // Handle the error if the API call fails
            // For production, you might want to handle various failure scenarios more robustly.
            return "Failed to get a response from ChatGPT API.";
        }
    }
}
