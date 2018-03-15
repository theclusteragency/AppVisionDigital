using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFom.Models;

namespace AppFom.Interfaces
{
    public interface IOperationServices
    {

        Task<OperResult<User>> CheckLogin<T>(T generic);

        Task<OperResult<List<Event>>> GetEvents();

        Task<OperResult<EventDetail>> GetEventDetail(int idevento);

        Task<OperResult<List<Event>>> GetOperEvents();

        Task<OperResult<bool>> UpdateUser(User user);

        Task<OperResult<int>> AddComment<T>(T generic);

        Task<OperResult<int>> AddPhoto<T>(T generic);

        Task<OperResult<int>> UpdStatusEvent<T>(T generic);

        Task<OperResult<int>> UpdStatusActivity<T>(T generic);

        Task<OperResult<List<User>>> getAllUsers();

        Task<OperResult<List<Comentario>>> getChatComments<T>(T generic);

        Task<OperResult<int>> AddChatComment<T>(T generic);
    }
}
