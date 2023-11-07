using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using VerticalTabsControlExample_4_Control.View;

namespace VerticalTabsControlExample_4_Control.ViewModel;

public partial class MainViewModel : ObservableBase, IViewLoadable
{
    [ObservableProperty] ObservableCollection<TabModel> _tabModels;

    private int tabSampleIdx = 0;
    public MainViewModel()
    {
        TabModels = new ();
    }
    FrameworkElement _view;
    public void OnLoaded(IViewable view)
    {
        _view = view.View;
    }

    [RelayCommand]
    private void AddA()
    {
        _view.Dispatcher.Invoke (() =>
        {
            this.TabModels.Add (new TabModel ()
            {
                ItemName = $"Item{tabSampleIdx}",
                ItemContent = new ContentA()
            });
        });
    }
    [RelayCommand]
    private void AddB()
    {
        _view.Dispatcher.Invoke (() =>
        {
            this.TabModels.Add (new TabModel ()
            {
                ItemName = $"Item{tabSampleIdx}",
                ItemContent = new ContentB ()
            });
        });
    }
    [RelayCommand]
    private void Delete(TabModel model)
    {
        this.TabModels.Remove (model);
    }
}
