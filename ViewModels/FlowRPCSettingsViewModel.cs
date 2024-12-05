using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiscordRPC;
using Flow.Launcher.Plugin.FLowRPC.Models;

namespace Flow.Launcher.Plugin.FLowRPC.ViewModels;

public class FlowRPCSettingsViewModel : ObservableObject
{
    private ObservableCollection<RPCProfile> _profiles;

    public ObservableCollection<RPCProfile> Profiles
    {
        get => _settings.Profiles;
        set
        {
            _profiles = value;
            SetProperty(ref _profiles, value);
            OnPropertyChanged();
        }
    }
    private RPCProfile _currentProfile;

    public RPCProfile CurrentProfile
    {
        get => _currentProfile;
        set
        {
            SetProperty(ref _currentProfile, value);
            OnPropertyChanged();
        }
    }
    
    private PluginInitContext _context;
    private Settings _settings;
    public IRelayCommand CreateNewProfileCommand { get; }
    public IRelayCommand RemoveCurrentProfileCommand { get; }

    public FlowRPCSettingsViewModel(PluginInitContext context, Settings settings)
    {
        this._context = context;
        this._settings = settings;
        CreateNewProfileCommand = new RelayCommand(CreateNewProfile);
        RemoveCurrentProfileCommand = new RelayCommand(RemoveCurrentProfile);
    }

    private void CreateNewProfile()
    {
        if (_settings.Profiles != null)
        {
            var newProfile = new RPCProfile()
            {
                ClientID = string.Empty,
                Title = "New Profile",
                Presence = new RichPresence()
                {
                    State = "Flow RPC",
                    Details = "Created and Running from flow launcher",
                }
            };
            _settings.Profiles.Add(newProfile);
            CurrentProfile = newProfile;
            _context.API.SavePluginSettings();
        }

        //_settings.Profiles = new ObservableCollection<RPCProfile>();
        //_context.API.SavePluginSettings();
    }

    private void RemoveCurrentProfile()
    {
        CurrentProfile = null;
        _settings.Profiles.Remove(CurrentProfile);
        _context.API.SavePluginSettings();
    }
}