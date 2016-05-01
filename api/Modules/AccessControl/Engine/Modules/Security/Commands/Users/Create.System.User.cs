using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.AccessControl.Commands.Users
{
    public class CreateSystemUser : Command
    {
        public CreateSystemUser(Guid id) : base(id)
        {
        }
    }
}
