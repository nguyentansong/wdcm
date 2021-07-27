using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDCM_Api.Repository.Interfaces;

namespace WDCM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public readonly IMenuRepository _repository;
        public MenuController(IMenuRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetMenu()
        {
            var data = await _repository.GetMenu();
            return Ok(data);
        }

        [HttpGet("parent")]
        public async Task<ActionResult> GetMenuByParentId([FromQuery] int id)
        {
            var data = await _repository.GetMenuByParentId(id);
            return Ok(data);
        }

        [HttpGet("byid")]
        public async Task<ActionResult> GetMenuById([FromQuery] int id)
        {
            return Ok(await _repository.GetMenyById(id));
        }
    }
}
