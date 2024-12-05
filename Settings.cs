using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flow.Launcher.Plugin.FLowRPC.Models;

namespace Flow.Launcher.Plugin.FLowRPC;

public class Settings
{
    public ObservableCollection<RPCProfile> Profiles { get; set; }
}