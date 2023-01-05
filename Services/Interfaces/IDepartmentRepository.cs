using backend.Dto.Department;

namespace backend.Services.Interfaces
{
    public interface IDepartmentRepository
    {
        bool CheckIdExist(int departmentId);
        ICollection<DepartmentDto> GetAll();
        DepartmentDto GetById(int departmentId);
        DepartmentDto Create(NewDepartmentDto newDepartmentDto);
        DepartmentDto DeleteById(int departmentId);
        DepartmentDto Update(
            int departmentId,
            UpdateDepartmentDto updateDepartmentDto
        );
    }
}
