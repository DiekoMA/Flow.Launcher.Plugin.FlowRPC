using System.Windows.Controls;
using Flow.Launcher.Plugin.FLowRPC.ViewModels;

namespace Flow.Launcher.Plugin.FLowRPC.Views;

public partial class FlowRPCSettings : UserControl
{
    public FlowRPCSettings(FlowRPCSettingsViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}