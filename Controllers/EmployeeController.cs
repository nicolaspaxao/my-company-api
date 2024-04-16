using AutoMapper;
using CompanyAPI.Dtos;
using CompanyAPI.Models;
using CompanyAPI.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Controllers; 

[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase {
    private readonly IMapper _mapper;
    private readonly IUnitOfWorks _uof;

    public EmployeeController ( IMapper mapper , IUnitOfWorks uof ) {
        _mapper = mapper;
        _uof = uof;
    }

    /// <summary>
    /// Busca a lista de colaboradores cadastrados
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>) ,200 )]
    [ProducesResponseType(typeof(HttpError) ,400 )]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get() {
        try {
            var employees = await _uof.employeeRepo.Get().ToListAsync();
            var employeesDto = _mapper.Map<List<EmployeeDto>>( employees );
            return employeesDto;
        } catch (Exception ex) {
            return StatusCode(400, new HttpError(400 , ex.Message));
        }
    }

    /// <summary>
    /// Retorna um colaborador pelo seu ID
    /// </summary>
    [ProducesResponseType(typeof(EmployeeDto) , 200)]
    [ProducesResponseType(typeof(HttpError) , 400)]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id ) {
        try {
            var employee = await _uof.employeeRepo.GetById((e)=> e.id == id);
            var employeeDto = _mapper.Map<Employee>(employee);
            if(employeeDto is null) {
                return BadRequest(new HttpError(400 , "Colaborador não encontrado na base."));
            } else {
                return Ok(employeeDto);
            }
        } catch (Exception ex) {
            return StatusCode(400 , new HttpError(400 , ex.Message));
        }
    }

    /// <summary>
    /// Cria um colaborador novo
    /// </summary>
    [ProducesResponseType(typeof(string) , 200)]
    [ProducesResponseType(typeof(HttpError) , 400)]
    [HttpPost]
    public async Task<ActionResult<string>> Post (EmployeeViewModel viewModel) {
        try {
            var employee = _mapper.Map<Employee>(viewModel);
            if(employee is null) {
                return BadRequest(new HttpError(400 , "Houve algum erro ao receber os dados"));
            }
            employee.createdAt = DateTime.Now;
            employee.bloquedAt = null;
            _uof.employeeRepo.Add(employee);
            await _uof.Commit();
            return Ok("Colaborador salvo com sucesso!");
        } catch (Exception ex) {
            return StatusCode(400 , new HttpError(400 , ex.Message));
        }
    }
}
