using System;
using System.Collections.Generic;

namespace Trackwane.Framework.Interfaces
{
    public interface IEngineHostConfig
    {
        Uri ListenUri { get; }

        IEnumerable<Type> Events { get; }

        IEnumerable<Type> Handlers { get; }

        IEnumerable<Type> Listeners { get; }

        IEnumerable<Type> Commands { get; }
    }
}