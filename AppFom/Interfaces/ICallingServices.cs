using System;
using System.Threading.Tasks;

namespace AppFom.Interfaces
{
    public interface ICallingServices
    {
        Task<bool> CallingNumber(string number);
    }
}
