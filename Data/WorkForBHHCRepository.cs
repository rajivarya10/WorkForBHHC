using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForBHHC.Data.Entities;
using WorkForBHHC.Models;

namespace WorkForBHHC.Data
{
    public class WorkForBHHCRepository : IWorkForBHHCRepository
    {
        private readonly WorkForBHHCContext _ctx;
        private readonly ILogger<WorkForBHHCRepository> _logger;
        private readonly UserManager<StoreUser> _userManager;
        public WorkForBHHCRepository(WorkForBHHCContext ctx, ILogger<WorkForBHHCRepository> logger, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _logger = logger;
            _userManager = userManager;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        /// <summary>
        /// Returns all the reasons
        /// </summary>
        /// <returns>List of Reasons</returns>
        public IEnumerable<Reason> GetAllReasons()
        {
            try
            {
                _logger.LogInformation("GetAllReasons was called");
                return _ctx.Reasons.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get all reasons: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Returns all reasons for the logged in user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List of reasons</returns>
        public IEnumerable<Reason> GetAllReasonsByUser(string username)
        {
            return _ctx.Reasons
                    .Where(r => r.User.UserName == username)
                    .ToList();
        }

        public Reason GetReasonById(int Id)
        {
            return _ctx.Reasons
                    .Where(r => r.Id == Id)
                    .FirstOrDefault();
        }

        public async Task PersistReasonAsync(ReasonViewModel model, string username)
        {
            using var transaction = await _ctx.Database.BeginTransactionAsync();
            try
            {
                var currentUser = await _userManager.FindByNameAsync(username);
                var reason = new Reason
                {
                    Description = model.Description,
                    CreatedOn = DateTime.Now,
                    User = currentUser
                };
                await _ctx.Reasons.AddAsync(reason);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
            await transaction.CommitAsync();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
