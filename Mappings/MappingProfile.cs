using AutoMapper;
using CompanyAPI.Dtos;
using CompanyAPI.Models;

namespace CompanyAPI.Mappings {
    public class MappingProfile :Profile{

        public MappingProfile()
        {
            CreateMap<Employee , EmployeeDto>().ReverseMap();
            CreateMap<Employee , EmployeeViewModel>().ReverseMap();
        }
    }
}
