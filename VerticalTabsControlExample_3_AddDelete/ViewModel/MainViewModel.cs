using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace VerticalTabsControlExample_3_AddDelete.ViewModel;

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
        this.Add ();
        this.Add ();
        this.Add ();
    }

    [RelayCommand]
    private void Add()
    {
        _view.Dispatcher.Invoke (() =>
        {
            this.TabModels.Add (new TabModel ()
            {
                ItemName = $"Item{tabSampleIdx}",
                ItemContent = $"Item{tabSampleIdx++}"
            });
        });
    }
    [RelayCommand]
    private void Delete(TabModel model)
    {
        this.TabModels.Remove (model);
    }
}
