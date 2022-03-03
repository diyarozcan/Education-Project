using AutoMapper;
using CoreApp102.Core.Models;
using CoreApp102.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp102.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        IService<Person> _perService;
        IMapper _mapper;

        public PersonsController(IService<Person> perService, IMapper mapper)
        {
            _perService = perService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var per = await _perService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Person>>(per));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var per = await _perService.GetByIdAsync(id);
            return Ok(_mapper.Map<Person>(per));
        }
        [HttpPost]
        public async Task<IActionResult> Save(Person catDto)
        {
            var newPer = await _perService.AddAsync(_mapper.Map<Person>(catDto));
            return Created(string.Empty, _mapper.Map<Person>(newPer));
        }
        [HttpPut]
        public IActionResult Update(Person perDto)
        {
            var per = _perService.Update(_mapper.Map<Person>(perDto));

            return NoContent();
            //return Ok(_mapper.Map<CategoryDto>(cat));
        }
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var per = _perService.GetByIdAsync(id).Result;   //async olmayan metodun icinde async kullanmak icin result kullanilir.
            _perService.Remove(per);
            return NoContent();
        }
    }
}
