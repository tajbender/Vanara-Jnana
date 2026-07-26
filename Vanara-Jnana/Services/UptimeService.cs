using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using Vanara.InteropServices;
using Vanara.PInvoke;

namespace Jnana.Services;

public static class UptimeService
{
    public static TimeSpan GetUptime()
    {
        ulong ms = Kernel32.GetTickCount64();
        return TimeSpan.FromMilliseconds(ms);
    }
}
