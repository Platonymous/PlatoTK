namespace MapTK.SpouseRooms
{
    internal class SpouseRoomPlacement
    {
        public string Name { get; }

        public int Spot { get; } = -1;

        public SpouseRoomPlacement(string name, int index)
        {
            Name = name;
            Spot = index;
        }
    }
}
