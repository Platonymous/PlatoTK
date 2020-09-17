using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTK.FestivalSpots
{
    internal class FestivalPlacementData
    {
        public string Festival { get; set; } = "summer11";
        public string Phase { get; set; } = "Set-Up";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Facing { get; set; } = 3;

    }

    internal class FestivalNPCData
    {
        public string NPC { get; set; } = "";
        public FestivalPlacementData[] Placements { get; set; } = new FestivalPlacementData[0];
    }
}
