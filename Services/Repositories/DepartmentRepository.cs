using backend.Models;
using backend.Dto.Department;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int departmentId)
        {
            return _context.Departments.Any(d => d.Id == departmentId);
        }

        public ICollection<DepartmentDto> GetAll()
        {
            return _context.Departments
                .Select(d => Utils.DtoConversion.ConvertDepartment(d))
                .ToList();
        }

        public DepartmentDto GetById(int departmentId)
        {
            var department = _context.Departments.Single(
                c => c.Id == departmentId
            );

            return Utils.DtoConversion.ConvertDepartment(department);
        }

        public DepartmentDto Create(NewDepartmentDto newDepartmentDto)
        {
            var newDepartment = new Department()
            {
                Name = newDepartmentDto.Name
            };

            _context.Departments.Add(newDepartment);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertDepartment(newDepartment);
        }

        public DepartmentDto DeleteById(int departmentId)
        {
            var department = _context.Departments.Single(
                c => c.Id == departmentId
            );

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertDepartment(department);
        }

        public DepartmentDto Update(
            int departmentId,
            UpdateDepartmentDto updateDepartmentDto
        )
        {
            var department = _context.Departments.Single(
                c => c.Id == departmentId
            );

            department.Name = updateDepartmentDto.Name;

            _context.Departments.Update(department);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertDepartment(department);
        }
    }
}
