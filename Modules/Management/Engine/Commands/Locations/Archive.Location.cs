﻿using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Locations
{
    public class ArchiveLocation: UserCommand
    {
        public string LocationId { get; private set; }

        public ArchiveLocation(string requesterId, string organizationId, string locationId) : base(requesterId)
        {
            LocationId = locationId;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; private set; }
    }
}
