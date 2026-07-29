using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Vanara_Jnana.ViewModels;

public class PerfMonViewModel : ObservableObject
{
    public ObservableCollection<string> Categories { get; init; }
    public ObservableCollection<CounterInfo> Counters { get; } = new();

    private CounterInfo _selectedCounter;
    public CounterInfo SelectedCounter
    {
        get => _selectedCounter;
        set => SetProperty(ref _selectedCounter, value);
    }

    public IRelayCommand PinToStatusbarCommand { get; }

    private readonly DispatcherTimer _timer;

    public PerfMonViewModel()
    {
        PinToStatusbarCommand = new RelayCommand(PinToStatusbar);

        Categories = new ObservableCollection<string>(
            PerformanceCounterCategory.GetCategories()
                                      .Select(c => c.CategoryName)
                                      .OrderBy(name => name));

        // TODO: select default category and load counters for it

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += UpdateCounters;
        _timer.Start();
    }

    public void LoadCounters(string categoryName)
    {
        Counters.Clear();

        var cat = new PerformanceCounterCategory(categoryName);

        foreach (var counterName in cat.GetCounters())
        {
            Counters.Add(new CounterInfo
            {
                Category = categoryName,
                Name = counterName.CounterName,
                InstanceName = counterName.InstanceName,
                Description = counterName.CounterHelp,
                Counter = counterName
            });
        }
    }

    private void UpdateCounters(object? sender, object e)
    {
        foreach (var c in Counters)
        {
            try
            {
                c.CurrentValue = c.Counter.NextValue();
            }
            catch
            {
                c.CurrentValue = float.NaN;
            }
        }
    }

    private void PinToStatusbar()
    {
        if (SelectedCounter == null)
            return;

        // TODO: Add to StatusbarViewModel
    }
}

public class CounterInfo : ObservableObject
{
    public string Category { get; init; }
    public string Name { get; init; }
    public string InstanceName { get; init; }
    public string Description { get; init; }

    private float _currentValue;
    public float CurrentValue
    {
        get => _currentValue;
        set => SetProperty(ref _currentValue, value);
    }

    public PerformanceCounter Counter { get; init; }
}
