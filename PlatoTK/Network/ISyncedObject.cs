namespace PlatoTK.Network
{

    public interface ISyncedObject
    {
        ISyncedData Data { get; set; }

        Netcode.NetString GetDataLink(object linkedObject);
    }
}
