using backend.Dto.LearningPath;
using backend.Dto.LearningPathCertificate;
using backend.Dto.Trainee;

namespace backend.Services.Interfaces
{
    public interface ILearningPathCertificateRepository
    {
        bool CheckIdExist(int traineeId, int learningPathId);
        LearningPathCertificateDto GetById(int traineeId, int learningPathId);
        ICollection<LearningPathCertificateDto> GetAll();
        ICollection<CertificatedTraineeDto> GetAllCertTraineesByLPathId(
            int learningPathId
        );
        ICollection<LearningPathDto> GetAllLearningPathsByTraineeId(
            int traineeId
        );
        LearningPathCertificateDto Create(
            NewLearningPathCertificateDto newLearningPathCertificateDto
        );
        LearningPathCertificateDto DeleteById(
            int traineeId,
            int learningPathId
        );
        LearningPathCertificateDto Update(
            int traineeId,
            int learningPathId,
            UpdateLearningPathCertificateDto updateLearningPathCertificateDto
        );
    }
}
