namespace Trackwane.Framework.Interfaces
{
    public interface IEngineHost
    {
        IEngineHostConfig Configuration { get; }

        IExecutionEngine ExecutionEngine { get; }

        void Start();

        void Stop();
    }
}