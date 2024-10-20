namespace GibbersBot2;

public class ChatGtpMessage(string content)
{
    public string Role { get; set; } = "user";
    public string Content { get; set; } = content;
}