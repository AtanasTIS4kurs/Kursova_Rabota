using GameStore.BL.Interfaces;
using GameStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<BusinessController> _logger;

        public CompaniesController(ICompanyService companyService, ILogger<BusinessController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet("GetALL")]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAll();
            if (companies.Count == 0)
            {
                _logger.LogError("No companies found in database");
                return NotFound(new { message = "No companies found." });
            }
            _logger.LogInformation("Getting all companies from database");
            return Ok(companies);
        }
        [HttpGet("GetbyId")]
        public async Task<ActionResult<Company>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string.");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string." });
            }
            var company = await _companyService.GetById(id);
            if (company == null)
            {
                _logger.LogError("Company with id:{id} not found", id);
                return NotFound(new { message = "Company not found." });
            }
            _logger.LogInformation("Getting company from database with id:{id}", id);
            return Ok(company);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Company company)
        {
            if (string.IsNullOrEmpty(company.Id) || !System.Text.RegularExpressions.Regex.IsMatch(company.Id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string.");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string." });
            }
            var existingGompany = await _companyService.GetById(company.Id);
            if (existingGompany != null)
            {
                _logger.LogError("Company creation failed. Company with id:{company.id} is already in use.", company.Id);
                return Conflict(new { message = "A company with this id already exists. Please use a unique id." });
            }
            if (company.Employees <1)
            {
                _logger.LogError("Company cannot have fewer than 1 employees");
                return Conflict(new { message = "Company cannot have fewer than 1 employees" });
            }
            await _companyService.Create(company);
            _logger.LogInformation("Creating company with id:{company.Id} in database", company.Id);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("Update")]
        public IActionResult Update(string id, Company company)
        {
            if (id != company.Id)
            {
                _logger.LogError("Company with id:{@id} not found", id);
                return BadRequest();
            }
            _companyService.Update(company);
            _logger.LogInformation("Company with id:{@company.Id} is updated", company.Id);
            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || !System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string.");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string." });
            }
            var company = _companyService.GetById(id);
            if (company == null)
            {
                _logger.LogWarning("Company with ID: {Id} not found.", id);
                return NotFound(new { message = "Company not found." });
            }
            _companyService.Delete(id);
            _logger.LogInformation("Deleting company with id:{@id}", id);
            return NoContent();
        }
    }


}
