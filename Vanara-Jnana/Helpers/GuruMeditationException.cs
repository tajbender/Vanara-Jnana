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
    public GuruMeditationException(HRESULT hRESULT, string message)
        : base(message) { }
    public GuruMeditationException(HRESULT hRESULT, string message, Exception inner)
        : base(message, inner) { }

    public GuruMeditationException(COMException comException)
        : base("Guru Meditation") { }
    public GuruMeditationException(COMException comException, string message)
        : base(message) { }
    public GuruMeditationException(COMException comException, string message, Exception inner)
        : base(message, inner) { }

}
