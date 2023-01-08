using backend.Dto.LearningPath;
using backend.Dto.LearningPathCertificate;
using backend.Dto.Trainee;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Repositories
{
    public class LearningPathCertificateRepository
        : ILearningPathCertificateRepository
    {
        private readonly AppDbContext _context;

        public LearningPathCertificateRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int traineeId, int learningPathId)
        {
            return _context.LearningPathCertificates.Any(
                lpc =>
                    lpc.TraineeId == traineeId
                    && lpc.LearningPathId == learningPathId
            );
        }

        public LearningPathCertificateDto GetById(
            int traineeId,
            int learningPathId
        )
        {
            var learningPathCertificate =
                _context.LearningPathCertificates.Single(
                    lpc =>
                        lpc.TraineeId == traineeId
                        && lpc.LearningPathId == learningPathId
                );

            return Utils.DtoConversion.ConvertLearningPathCertificate(
                learningPathCertificate
            );
        }

        public ICollection<LearningPathCertificateDto> GetAll()
        {
            var learningPathCertificateDtos = _context.LearningPathCertificates
                .Select(
                    lpc =>
                        Utils.DtoConversion.ConvertLearningPathCertificate(lpc)
                )
                .ToList();

            return learningPathCertificateDtos;
        }

        public ICollection<CertificatedTraineeDto> GetAllCertTraineesByLPathId(
            int learningPathId
        )
        {
            return _context.LearningPathCertificates
                .Where(lpc => lpc.LearningPathId == learningPathId)
                .Include(lpc => lpc.Trainee)
                .Select(
                    lpc =>
                        Utils.DtoConversion.ConvertLPathCertToCertTrainee(lpc)
                )
                .ToList();
        }

        public ICollection<LearningPathDto> GetAllLearningPathsByTraineeId(
            int traineeId
        )
        {
            return _context.LearningPathCertificates
                .Where(lpc => lpc.TraineeId == traineeId)
                .Select(lpc => lpc.LearningPath)
                .Select(lp => Utils.DtoConversion.ConvertLearningPath(lp))
                .ToList();
        }

        public LearningPathCertificateDto Create(
            NewLearningPathCertificateDto newLearningPathCertificateDto
        )
        {
            var learningPathCertificate = new LearningPathCertificate()
            {
                TraineeId = newLearningPathCertificateDto.TraineeId,
                LearningPathId = newLearningPathCertificateDto.LearningPathId,
                StartDate = newLearningPathCertificateDto.StartDate,
                Duration = newLearningPathCertificateDto.Duration
            };

            _context.LearningPathCertificates.Add(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(
                learningPathCertificate
            );
        }

        public LearningPathCertificateDto DeleteById(
            int traineeId,
            int learningPathId
        )
        {
            var learningPathCertificate =
                _context.LearningPathCertificates.Single(
                    lpc =>
                        lpc.TraineeId == traineeId
                        && lpc.LearningPathId == learningPathId
                );

            _context.LearningPathCertificates.Remove(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(
                learningPathCertificate
            );
        }

        public LearningPathCertificateDto Update(
            int traineeId,
            int learningPathId,
            UpdateLearningPathCertificateDto updateLearningPathCertificateDto
        )
        {
            var learningPathCertificate =
                _context.LearningPathCertificates.Single(
                    lpc =>
                        lpc.TraineeId == traineeId
                        && lpc.LearningPathId == learningPathId
                );

            Utils.EntityMapping.MapLearningPathCertificateFromDto(
                ref learningPathCertificate,
                updateLearningPathCertificateDto
            );

            _context.LearningPathCertificates.Update(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(
                learningPathCertificate
            );
        }
    }
}
