using System.Text;

namespace GeekCliServices.Services
{
    public interface ICommandOutputSink
    {
        void Write(string message);
    }

    public static class CommandOutput
    {
        private static readonly AsyncLocal<ICommandOutputSink?> CurrentSink = new();
        private static readonly ICommandOutputSink DefaultSink = new StderrCommandOutputSink();

        public static void Info(string? message) => Write(message);

        public static void Error(string? message) => Write(message);

        public static IDisposable Push(ICommandOutputSink sink)
        {
            var previous = CurrentSink.Value;
            CurrentSink.Value = sink;

            return new RestoreScope(previous);
        }

        private static void Write(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            (CurrentSink.Value ?? DefaultSink).Write(message);
        }

        private sealed class RestoreScope : IDisposable
        {
            private readonly ICommandOutputSink? _previous;

            public RestoreScope(ICommandOutputSink? previous)
            {
                _previous = previous;
            }

            public void Dispose()
            {
                CurrentSink.Value = _previous;
            }
        }
    }

    public sealed class BufferingCommandOutputSink : ICommandOutputSink
    {
        private readonly StringBuilder _builder = new();

        public void Write(string message)
        {
            _builder.AppendLine(message);
        }

        public override string ToString()
        {
            return _builder.ToString().TrimEnd();
        }
    }

    internal sealed class StderrCommandOutputSink : ICommandOutputSink
    {
        public void Write(string message)
        {
            Console.Error.WriteLine(message);
        }
    }
}
