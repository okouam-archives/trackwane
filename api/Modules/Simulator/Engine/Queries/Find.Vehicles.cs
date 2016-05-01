using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine.Queries
{
    public class FindVehicles
    {
        private readonly IProvideReadings readingProvider;
        private readonly GetRoutes getRoutesQuery;

        public FindVehicles(IProvideReadings readingProvider, GetRoutes getRoutesQuery)
        {
            this.readingProvider = readingProvider;
            this.getRoutesQuery = getRoutesQuery;
        }

        public IEnumerable<int> Execute()
        {
            var batches = getRoutesQuery.Execute().Batch(10);

            return from batch in batches
                   from position in readingProvider.Retrieve("rt", batch.Select(x => x.ToString()).ToArray())
                   orderby position.Id
                   select position.Id;
        }
    }
}
