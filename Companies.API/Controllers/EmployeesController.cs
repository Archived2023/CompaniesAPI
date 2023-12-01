using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using Companies.API.Dtos.EmployeesDtos;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace Companies.API.Controllers
{
    [Route("api/companies/{companyId:guid}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly APIContext db;
        private readonly IMapper mapper;

        public EmployeesController(APIContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;
        }

        // GET: api/Employees
        /// <summary>
        /// Get employees by CompanyId
        /// </summary>
        /// <param name="companyId">The Id of the company</param>
        /// <returns>An ActionResult of an IEnumerable of EmployeeDto</returns>
        /// <response code="200">Returned the employees from the requested company</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(Guid companyId)
        {
            var company = await db.Companies.Include(c => c.Employees).ThenInclude(e => e.Department).FirstOrDefaultAsync(c => c.Id == companyId);

            if(company is null) return NotFound();

            var employeeDtos = mapper.Map<IEnumerable<EmployeeDto>>(company.Employees);

            return Ok(employeeDtos);
        }


        [HttpGet("{employeeId:guid}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeesForCompany(Guid companyId, Guid employeeId)
        {
            var company = await db.Companies.FirstOrDefaultAsync(c => c.Id.Equals(companyId));

            if (company is null) return NotFound("Company not found");

            var employee = await db.Employees.Include(e => e.Department)
                                             .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee is null) return NotFound("Employee not found");

            var employeeDto = mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);

        }
        /// <summary>
        /// Updating a employee
        /// </summary>
        /// <param name="companyId">The Id of the company</param>
        /// <param name="employeeId">The Id of the employee to update</param>
        /// <param name="patchDoc"></param>
        /// <returns>An ActionResult</returns>
        /// <remarks>
        /// Sample request (this request updates the employees information):
        /// PATCH /employees/{id}
        /// [ \
        ///  { \
        ///  "op": "replace", \
        ///  "path": "/name", \
        ///  "value": "new name" } \
        ///  } \
        ///  { \
        ///  "op": "replace", \
        ///  "path": "/age", \
        ///  "value": 25 } \
        /// ]
        /// 
        /// </remarks>
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult> PatchEmployee(
            Guid companyId,
            Guid employeeId,
            JsonPatchDocument<EmployeesForUpdateDto> patchDoc)
        {
            var company = await db.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

            if (company is null) return NotFound("No comp found");

            var empToPatch = await db.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (empToPatch is null) return NotFound();
            if(company.Id == empToPatch.CompanyId) return BadRequest();

            var dto = mapper.Map<EmployeesForUpdateDto>(empToPatch);

            patchDoc.ApplyTo(dto, ModelState);

            await TryUpdateModelAsync(dto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            mapper.Map(dto, empToPatch);
            db.Update(empToPatch);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT: api/Employees/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Employees
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        //// DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(Guid id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EmployeeExists(Guid id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
