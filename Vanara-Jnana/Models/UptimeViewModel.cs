//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//
//namespace Jnana.Models
//{
//    internal class UptimeViewModel
//    {
//    }
//}
//

using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Services;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Jnana.Models;

public class UptimeViewModel : ObservableObject
{
    private readonly Timer _timer;

    private TimeSpan _uptime;
    public TimeSpan Uptime
    {
        get => _uptime;
        set => SetProperty(ref _uptime, value);
    }

    public UptimeViewModel()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += (_, _) => Update();
        _timer.Start();

        Update();
    }

    private void Update()
    {
        Uptime = UptimeService.GetUptime();
    }
}
