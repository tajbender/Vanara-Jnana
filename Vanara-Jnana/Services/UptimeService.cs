using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;

namespace Jnana.Services;

public static class UptimeService
{
    [DllImport("kernel32.dll")]
    private static extern ulong GetTickCount64();

    public static TimeSpan GetUptime()
    {
        ulong ms = GetTickCount64();
        return TimeSpan.FromMilliseconds(ms);
    }
}
