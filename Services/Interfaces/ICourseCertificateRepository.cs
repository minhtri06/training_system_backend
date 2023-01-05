using backend.Dto.CourseCertificate;

namespace backend.Services.Interfaces
{
    public interface ICourseCertificateRepository
    {
        ICollection<CourseCertificateDto> GetAll();
        CourseCertificateDto? GetById(int traineeId, int courseId);
        CourseCertificateDto Create(
            NewCourseCertificateDto newCourseCertificateDto
        );
        CourseCertificateDto? DeleteById(int traineeId, int courseId);
        CourseCertificateDto? Update(
            int traineeId,
            int courseId,
            UpdateCourseCertificateDto updateCourseCertificateDto
        );
    }
}
