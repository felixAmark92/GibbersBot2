using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GibbersBot2;

public class ChatGtpClient
{
    private readonly HttpClient _httpClient;

    public ChatGtpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY"); 
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<string?> GetResponse(string message, string? user = null, bool system = false)
    {
        var request = new ChatGtpRequest();
        if (user is not null)
        {
            var userTips = new ChatGtpMessage($"the next message is sent by user: {user}");
            request.Messages.Add(userTips);
        }
        var newMessage = new ChatGtpMessage(message);
        if (system) newMessage.Role = "system";
        request.Messages.Add(newMessage);
        var response = await _httpClient.PostAsJsonAsync("chat/completions", request);

        if (!response.IsSuccessStatusCode) return string.Empty;
        
        var content = await response.Content.ReadFromJsonAsync<ChatGtpResponse>();

        return content?.Choices[0].Message.Content;
    }
}