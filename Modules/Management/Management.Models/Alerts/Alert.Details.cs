namespace Trackwane.Management.Models.Alerts
{
    public class AlertDetails
    {
        public bool IsArchived { get; set; }

        public string Name { get; set; }

        public int Threshold { get; set; }

        public string Type { get; set; }
    }
}
