namespace MapTK.SpouseRooms
{
    internal class SpouseRoomTokenY : SpouseRoomTokenX
    {
        protected override string[] GetResult(int x, int y)
        {
            return new[] { y.ToString() };
        }
    }
}
