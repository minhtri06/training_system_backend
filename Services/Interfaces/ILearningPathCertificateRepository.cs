using backend.Dto.LearningPathCertificate;

namespace backend.Services.Interfaces
{
    public interface ILearningPathCertificateRepository
    {
        bool CheckIdExist(int traineeId, int learningPathId);
        LearningPathCertificateDto GetById(int traineeId, int learningPathId);
        ICollection<LearningPathCertificateDto> GetAll();
        LearningPathCertificateDto Create(NewLearningPathCertificateDto newLearningPathCertificateDto);
        LearningPathCertificateDto DeleteById(int traineeId, int learningPathId);
        LearningPathCertificateDto Update(
            int traineeId, int learningPathId,
            UpdateLearningPathCertificateDto updateLearningPathCertificateDto
        );
    }
}
