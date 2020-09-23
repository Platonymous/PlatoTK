using Microsoft.Xna.Framework;

namespace MapTK.MapExtras
{
    internal class MapMergeData
    {
        public string Source { get; set; }

        public string Target { get; set; }

        public Rectangle FromArea { get; set; }

        public Rectangle ToArea { get; set; }

        public bool RemoveEmpty { get; set; } = false;

        public bool PatchMapProperties { get; set; } = false;
    }
}
