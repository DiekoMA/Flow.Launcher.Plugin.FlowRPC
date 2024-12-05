using DiscordRPC;

namespace Flow.Launcher.Plugin.FLowRPC.Models;

public class RPCProfile
{
    public string ClientID { get; set; }
    public string Title { get; set; }
    public RichPresence Presence { get; set; }
}