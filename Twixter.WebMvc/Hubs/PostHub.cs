using Microsoft.AspNetCore.SignalR;

namespace Twixter.WebMvc.Hubs;

public class PostHub:Hub
{
    public async Task SendNewPost(string userName, string content)
    {
        await Clients.All.SendAsync("ReceiveNewPost", userName, content);
    }
}