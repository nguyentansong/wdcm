using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDCM_Api.Entities;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            Guid UserId = Guid.NewGuid();
            user.Id = UserId;
            await _userRepository.Create(user);
            return Ok("ok");
        }
    }
}
