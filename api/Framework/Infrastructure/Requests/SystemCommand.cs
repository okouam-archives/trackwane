using System;
using Request = Trackwane.Framework.Common.Request;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class SystemCommand : Request
    {
   
        protected SystemCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
