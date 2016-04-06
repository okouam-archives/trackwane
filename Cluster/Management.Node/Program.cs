using Trackwane.Framework.Cluster;

namespace Trackwane.Management.Service
{
    internal static class Program
    {
        private static void Main()
        {
            EntryPoint<Node>.Launch("Management_Engine");
        }
    }
}
