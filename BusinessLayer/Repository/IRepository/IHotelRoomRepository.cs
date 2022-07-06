using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms();
        public Task<int> DeleteHotelRoom(int roomId);
        public Task<HotelRoomDTO> IsSameNameHotelAlreadyExist(string name);
        public Task<bool> IsRoomBooked(int RoomId, string checkInDate, string checkOutDate);
        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
    }
}