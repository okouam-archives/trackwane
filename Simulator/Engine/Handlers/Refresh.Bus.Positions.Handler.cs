using System.Linq;
using log4net;
using paramore.brighter.commandprocessor;
using Trackwane.Simulator.Engine.Commands;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine.Handlers
{
    public class RefreshBusPositionsHandler : RequestHandler<RefreshBusPositions>
    {
        private readonly ITrackwaneApi api;
        private readonly FindVehiclePositions query;

        public RefreshBusPositionsHandler(ITrackwaneApi api, FindVehiclePositions query) : base(null)
        {
            this.api = api;
            this.query = query;
        }

        public override RefreshBusPositions Handle(RefreshBusPositions command)
        {
            log.Debug("Retrieving bus positions from the CTA API");

            var events = query.Execute(1008, 1010, 1011, 1012, 1014, 1015, 1016, 1017, 1023, 1024).ToList();

            log.Debug($"{events.Count} bus positions were retrieved");

            var acknowledgements = api.SaveRawData(events);

            log.Debug($"{acknowledgements.Count()} GPS events were saved to the Trackwane API");

            return base.Handle(command);
        }

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
