using EntityLayer;
namespace EntityLayer.Entities
{
    public class FullRoomProp
    {
        public Room Room { get; set; }
        public RoomFeatures RoomFeatures { get; set; }
        public RoomImages RoomImages { get; set; }

        public FullRoomProp()
        {
            Room = new Room();
            RoomFeatures = new RoomFeatures();
            RoomImages = new RoomImages();
        }
    }
}
