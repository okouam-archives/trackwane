using System.Linq;
using System.Net.NetworkInformation;

namespace Trackwane.Framework.Fixtures
{
    public static class Background
    {
        /* Public */

        public static int FindAvailablePort(int portNumber)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ipEndPoints = ipProperties.GetActiveTcpListeners().Where(x => x.Port >= portNumber).ToList();

            var current = portNumber;

            while (true)
            {
                var listener = ipEndPoints.FirstOrDefault(x => x.Port == current);

                if (listener != null)
                {
                    current++;
                }
                else
                {
                    return current;
                }
            }
        }
    }
}
