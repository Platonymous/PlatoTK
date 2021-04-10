using Microsoft.Xna.Framework;
using System;

namespace PlatoTK.APIs
{
    public interface IArcadeApi
    {
        Vector2 ReserveMachineSpot();

        event EventHandler OnRoomSetup;
    }
}
