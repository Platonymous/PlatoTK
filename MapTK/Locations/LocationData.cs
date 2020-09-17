namespace MapTK.Locations
{
    internal class LocationData
    {
        public string Name { get; set; }

        public string MapPath { get; set; }

        public string Type { get; set; } = "default";

        public string[] Options { get; } = new string[0];
    }
}
