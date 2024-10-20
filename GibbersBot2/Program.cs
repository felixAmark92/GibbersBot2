// See https://aka.ms/new-console-template for more information


using Discord;
using Discord.WebSocket;
using GibbersBot2;

var client = new DiscordSocketClient();
var token = Environment.GetEnvironmentVariable("GIBBERS_TOKEN");
var chatGtpClient = new ChatGtpClient();

client.Log += Log;
client.MessageReceived += HandleMessage;


await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

var dailyEvent = new DailyEvent(13, 00, SendDailyMessage);


await Task.Delay(-1);


 Task Log(LogMessage log)
{
    Console.WriteLine(log.ToString());
 
    return Task.CompletedTask;
}
async Task SendDailyMessage()
{
    var channel =  client.GetChannel(352182763216044034) as SocketTextChannel;
    if (channel is null) throw new NullReferenceException();
    Console.WriteLine(channel.Name);
    try
    {
        Console.WriteLine(client.ConnectionState);
        var message = $"{DateTime.Now:HH.mm} this message was sent";
        await channel.SendMessageAsync(message);
        Console.WriteLine("Message was sent");
        
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
}
async Task HandleMessage(SocketMessage message)
{
    if(message.Author.IsBot) return;

    if (message is not SocketUserMessage userMessage) return;

    var mentionedUsers = userMessage.MentionedUsers;
    if (mentionedUsers.All(user => user.Id != client.CurrentUser.Id)) return;
    
    await message.Channel.TriggerTypingAsync();
    var responseMessage = await chatGtpClient.GetResponse(message.Content, message.Author.Username);
    if (responseMessage is null)
    {
        await message.Channel.SendMessageAsync("Ops! Seems like something went wrong there. please try again!");
        return;
    }
    await message.Channel.SendMessageAsync(responseMessage);
}
