using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.PInvoke;

namespace Jnana.Helpers;

public class GuruMeditationException : Exception
{
    public GuruMeditationException()
        : base("Guru Meditation") { }

    public GuruMeditationException(string message)
        : base(message) { }

    public GuruMeditationException(string message, Exception inner)
        : base(message, inner) { }

    public GuruMeditationException(HRESULT hRESULT)
        : base("Guru Meditation") { }
}
