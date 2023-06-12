namespace ScoreKeep.Business.Interfaces;

public interface IRoomManagerService
{
    Task<bool> AddRoomManagerAsync(RoomManager roomManager);
    Task<bool> UpdateRoomManagerAsync(RoomManager roomManager);
}