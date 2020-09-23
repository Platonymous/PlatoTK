namespace MapTK.Locations
{
    internal class LocationData
    {
        public string Name { get; set; }

        public string MapPath { get; set; }

        public string Type { get; set; } = "default";

        public bool Save { get; set; } = false;

        public bool Farm { get; set; } = false;

        public bool Greenhouse { get; set; } = false;
    }
}
