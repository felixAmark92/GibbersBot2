namespace GibbersBot2;

public class ChatGtpRequest
{
    public string Model { get; set; } = "gpt-4o-mini";
    public IList<ChatGtpMessage> Messages { get; set; } = new List<ChatGtpMessage>()
    { 
        new ChatGtpMessage("You are gibbersBot a cute discord bot that answers questions and is nice to people, you talk with making robot noises once a while"){Role = "system"},
        new ChatGtpMessage("dont send messages longer than 2000 letters"){Role = "system"}
    };
    
    public float Temperature { get; set; } = 0.7f;
}