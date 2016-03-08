using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Data.Domain;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.Data.Engine.Queries
{
    public class FindBySearchCriteria : Query<ResponsePage<SearchResult>>
    {
        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public ResponsePage<SearchResult> Execute(string hardwareId, DateTime? from, DateTime? to)
        {
            return Execute(repository =>
            {
                var query = repository.Query<SensorReading>();

                if (!string.IsNullOrEmpty(hardwareId))
                {
                    query = query.Where(x => x.HardwareId == hardwareId);
                }

                if (from.HasValue)
                {
                    query = query.Where(x => x.Timestamp > from.Value);
                }

                if (to.HasValue)
                {
                    query = query.Where(x => x.Timestamp > to.Value);
                }

                var results = query.Customize(x => x.WaitForNonStaleResults()).ToList().Select(x => new SearchResult
                {
                    BatteryLevel = x.BatteryLevel,
                    Coordinates = x.Coordinates,
                    Distance = x.Distance,
                    HardwareId = x.HardwareId,
                    Heading = x.Heading,
                    Orientation = x.Orientation,
                    Petrol = x.Petrol,
                    Speed = x.Speed,
                    Timestamp = x.Timestamp
                }).ToList();

                return new ResponsePage<SearchResult>
                {
                    Items = results,
                    Total = results.Count
                };
            });
        }  
    }
}
