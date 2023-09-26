namespace ScoreKeep.Business.Interfaces;

public interface IRoomManagerService
{
    Task<bool> AddRoomManagerAsync(RoomManager roomManager, String password);
    Task<bool> UpdateRoomManagerAsync(RoomManager roomManager);
}