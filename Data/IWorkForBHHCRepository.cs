using System.Collections.Generic;
using System.Threading.Tasks;
using WorkForBHHC.Data.Entities;
using WorkForBHHC.Models;

namespace WorkForBHHC.Data
{
    public interface IWorkForBHHCRepository
    {
        IEnumerable<Reason> GetAllReasons();
        Reason GetReasonById(int Id);
        bool SaveAll();
        void AddEntity(object model);
        IEnumerable<Reason> GetAllReasonsByUser(string username);
        Task PersistReasonAsync(ReasonViewModel model, string username);
    }
}