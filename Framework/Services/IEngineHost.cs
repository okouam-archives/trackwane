using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Interfaces
{
    public interface IEngineHost
    {
        IModuleConfig Configuration { get; }

        IExecutionEngine ExecutionEngine { get; }

        void Start();

        void Stop();
    }
}