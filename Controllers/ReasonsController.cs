using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForBHHC.Data;
using WorkForBHHC.Data.Entities;
using WorkForBHHC.Models;

namespace WorkForBHHC.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ReasonsController : Controller
    {
        private readonly IWorkForBHHCRepository _repository;
        private readonly ILogger<ReasonsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        public ReasonsController(IWorkForBHHCRepository repository, ILogger<ReasonsController> logger, 
            IMapper mapper,
            UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var username = User.Identity.Name;
                var results = _repository.GetAllReasonsByUser(User.Identity.Name);
                return Ok(_mapper.Map<IEnumerable<Reason>, IEnumerable<ReasonViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get reasons: {ex}");
                return BadRequest("Failed to get reasons");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var reason = _repository.GetReasonById(id);
                if (reason != null) return Ok(_mapper.Map<Reason, ReasonViewModel>(reason));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get reasons: {ex}");
                return BadRequest("Failed to get reasons");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ReasonViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newReason = _mapper.Map<ReasonViewModel, Reason>(model);

                    newReason.CreatedOn = DateTime.Now;

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newReason.User = currentUser;

                    _repository.AddEntity(model);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/reasons/{newReason.Id}", _mapper.Map<Reason, ReasonViewModel>(newReason));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save the reason: {ex}");
            }
            return BadRequest("Failed to save the reason");
        }
    }
}
