﻿#region Licence
/* The MIT License (MIT)
Copyright © 2016 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.monitoring.Events;

namespace paramore.brighter.commandprocessor.monitoring.Handlers
{
    public class RequestHandlerAsyncMonitorHandler<T> : RequestHandlerAsync<T> where T : class, IRequest
    {
        private readonly IAmAControlBusSender _controlBusSender;
        private bool _isMonitoringEnabled;
        private string _handlerName;
        private string _instanceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandlerAsyncMonitorHandler{T}"/> class.
        /// </summary>
        /// <param name="controlBusSender">The control bus command processor, to post over</param>
        public RequestHandlerAsyncMonitorHandler(IAmAControlBusSender controlBusSender)
            : this(controlBusSender, LogProvider.GetCurrentClassLogger())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandlerAsyncMonitorHandler{T}"/> class.
        /// Use this instance if you need to inject a logger, for example for testing
        /// </summary>
        /// <param name="controlBusSender">The control bus command processor, to post over</param>
        /// <param name="logger">The logger</param>
        public RequestHandlerAsyncMonitorHandler(IAmAControlBusSender controlBusSender, ILog logger) : base(logger)
        {
            _controlBusSender = controlBusSender;
        }

        /// <summary>
        /// Initializes from attribute parameters.
        /// </summary>
        /// <param name="initializerList">The initializer list.</param>
        public override void InitializeFromAttributeParams(params object[] initializerList)
        {
            _isMonitoringEnabled = (bool)initializerList[0];
            _handlerName = (string)initializerList[1];
            _instanceName = (string)initializerList[2];
        }

        /// <summary>
        /// Awaitably handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="ct">Allow the caller to cancel the operation (optional)</param>
        /// <returns><see cref="Task"/>.</returns>
        public override async Task<T> HandleAsync(T command, CancellationToken? ct)
        {
            if (!_isMonitoringEnabled) return await base.HandleAsync(command, ct).ConfigureAwait(ContinueOnCapturedContext);
            try
            {
                // Todo FHo: await PostAsync to maximise benefits?

                if (ct.HasValue && !ct.Value.IsCancellationRequested)
                {
                    _controlBusSender.Post(
                        new MonitorEvent(
                            _instanceName,
                            MonitorEventType.EnterHandler,
                            _handlerName,
                            JsonConvert.SerializeObject(command),
                            Clock.Now().GetValueOrDefault()));
                }

                await base.HandleAsync(command, ct).ConfigureAwait(ContinueOnCapturedContext);


                if (ct.HasValue && !ct.Value.IsCancellationRequested)
                {
                    _controlBusSender.Post(
                        new MonitorEvent(
                            _instanceName,
                            MonitorEventType.ExitHandler,
                            _handlerName,
                            JsonConvert.SerializeObject(command),
                            Clock.Now().GetValueOrDefault()));
                }

                return command;
            }
            catch (Exception e)
            {
                if (ct.HasValue && !ct.Value.IsCancellationRequested)
                {
                    _controlBusSender.Post(
                        new MonitorEvent(
                            _instanceName,
                            MonitorEventType.ExceptionThrown,
                            _handlerName,
                            JsonConvert.SerializeObject(command),
                            Clock.Now().GetValueOrDefault(),
                            e));
                }
                throw;
            }
        }
    }
}
