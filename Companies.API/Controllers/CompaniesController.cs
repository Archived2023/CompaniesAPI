using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using Companies.API.Dtos.CompaniesDtos;
using AutoMapper;
using Companies.API.Repositorys;
using Companies.API.Services;
using Companies.API.Exceptions;
using Companies.API.Responses;

namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ResponseController
    {
        private readonly IServiceManager serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet(Name = "RouteName")]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
        {
            var response = await serviceManager.CompanyService.GetAsync(includeEmployees);
            return  Ok(((OkResponse<IEnumerable<CompanyDto>>)response).Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var response = await serviceManager.CompanyService.GetAsync(id);
            if(!response.Success)
                HandleErrors(response);
            
            return Ok(((OkResponse<CompanyDto>)response).Result);
        }



        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, CompanyForUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(); //ToDo create Filter
            await serviceManager.CompanyService.UpdateAsync(id, dto);
            return NoContent();
        }

        ////// POST: api/Companies
        ////// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Company>> PostCompany(CompanyForCreationDto dto)
        //{
        //    var company = mapper.Map<Company>(dto);

        //    await unitOfWork.CompanyRepository.AddAsync(company);
        //    // _context.Companies.Add(company);
        //    await unitOfWork.CompleteAsync();

        //    var companyToReturn = mapper.Map<CompanyDto>(company);

        //    return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyToReturn);
        //}

        ////// DELETE: api/Companies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(Guid id)
        //{
        //    var company = await unitOfWork.CompanyRepository.GetAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    unitOfWork.CompanyRepository.Remove(company);
        //    // _context.Companies.Remove(company);
        //    await unitOfWork.CompleteAsync();

        //    return NoContent();
        //}

       
    }
}
