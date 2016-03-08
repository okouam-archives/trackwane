using System;
using System.Collections.Generic;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class EngineHostConfig : IEngineHostConfig
    {
        public EngineHostConfig()
        {
            
        }

        public EngineHostConfig(IEnumerable<Type> commands, IEnumerable<Type> events, IEnumerable<Type> handlers, IEnumerable<Type> listeners, Uri listenUri)
        {
            Commands = commands;
            Events = events;
            Handlers = handlers;
            Listeners = listeners;
            ListenUri = listenUri;
        }

        public IEnumerable<Type> Events { get; set; }
        
        public IEnumerable<Type> Listeners { get; set; } 

        public IEnumerable<Type> Commands { get; set; }

        public IEnumerable<Type> Handlers { get; set; }

        public Uri ListenUri { get; set; }
    }
}