using AutoMapper;
using Business.Repository.IRepository;
using DataAcess.Data;
using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRoom.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRoom.FindAsync(roomId);
            if (roomDetails != null)
            {
                _db.HotelRoom.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                             _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                            (_db.HotelRoom.Include(x => x.HotelRoomImages));
                //if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                //{
                //foreach (HotelRoomDTO hotelRoom in hotelRoomDTOs)
                //{
                //    hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateStr, checkOutDatestr);
                //}
                //}
                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRoom.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    //hotelRoom.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDatestr);
                }

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<bool> IsRoomBooked(int RoomId, string checkInDate, string checkOutDate)
        {
            throw new NotImplementedException();
        }

        //if unique returns null else returns the room obj
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRoom.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRoom.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id != roomId));

                    return hotelRoom;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<HotelRoomDTO> IsSameNameHotelAlreadyExist(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRoom.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRoom.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}