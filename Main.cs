using System;
using System.Collections.Generic;
using System.Windows.Controls;
using DiscordRPC;
using Flow.Launcher.Plugin;
using Flow.Launcher.Plugin.FLowRPC.ViewModels;
using Flow.Launcher.Plugin.FLowRPC.Views;

namespace Flow.Launcher.Plugin.FLowRPC
{
    public class FLowRPC : IPlugin, ISettingProvider
    {
        private static FlowRPCSettingsViewModel _viewModel;
        private PluginInitContext _context;
        private Settings _settings;
        private DiscordRpcClient client;
        
        public void Init(PluginInitContext context)
        {
            _context = context;
            _settings = context.API.LoadSettingJsonStorage<Settings>();
            _viewModel = new FlowRPCSettingsViewModel(_context, _settings);
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();

            if (query.Search.Length < 2)
            {
                results.Add(new Result()
                {
                    Title = "Start",
                    SubTitle = "Select a profile to show off on discord.",
                });
                
                results.Add(new Result()
                {
                    Title = "Stop",
                    SubTitle = "Select a profile to show off on discord.",
                });
                
                results.Add(new Result()
                {
                    Title = "Pause",
                    SubTitle = "Select a profile to show off on discord.",
                });
            }

            switch (query.Search.ToLower())
            {
                case "start":
                    foreach (var profile in _settings.Profiles)
                    {
                        results.Add(new Result()
                        {
                            Title = profile.Title,
                            SubTitle = profile.ClientID,
                            AsyncAction = async c =>
                            {
                                try
                                {
                                    client = new DiscordRpcClient(profile.ClientID);
                                    client.Initialize();
                                    client.SetPresence(profile.Presence);
                                }
                                catch (Exception e)
                                {
                                    _context.API.ShowMsgError(e.Message);
                                }
                                return true;
                            }
                        });
                    }
                    break;
                case "stop":
                    results.Add(new Result()
                    {
                        Title = $"Current Running Presence {client.CurrentPresence.State}",
                        SubTitle = "Running",
                        AsyncAction = async c =>
                        {
                            client.ClearPresence();
                            return true;
                        }
                    });
                    break;
            }
            return results;
        }

        public Control CreateSettingPanel()
        {
            return new FlowRPCSettings(_viewModel);
        }
    }
}