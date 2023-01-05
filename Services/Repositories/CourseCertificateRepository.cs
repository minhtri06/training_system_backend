using backend.Models;
using backend.Dto.CourseCertificate;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class CourseCertificateRepository : ICourseCertificateRepository
    {
        private readonly AppDbContext _context;

        public CourseCertificateRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<CourseCertificateDto> GetAll()
        {
            return _context.CourseCertificates
                .Select(cc => Utils.DtoConversion.ConvertCourseCertificate(cc))
                .ToList();
        }

        public CourseCertificateDto? GetById(int traineeId, int courseId)
        {
            var courseCertificate = _context.CourseCertificates.SingleOrDefault(
                cc => cc.TraineeId == traineeId && cc.CourseId == courseId
            );

            if (courseCertificate != null)
            {
                return Utils.DtoConversion.ConvertCourseCertificate(
                    courseCertificate
                );
            }
            return null;
        }

        public CourseCertificateDto Create(
            NewCourseCertificateDto newCourseCertificateDto
        )
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == newCourseCertificateDto.TraineeId
            );
            var course = _context.Courses.SingleOrDefault(
                c => c.Id == newCourseCertificateDto.CourseId
            );

            var newCourseCertificate = new CourseCertificate()
            {
                Trainee = trainee,
                Course = course,
                StartDate = newCourseCertificateDto.StartDate,
                Duration = newCourseCertificateDto.Duration
            };

            _context.CourseCertificates.Add(newCourseCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourseCertificate(
                newCourseCertificate
            );
        }

        public CourseCertificateDto? DeleteById(int traineeId, int courseId)
        {
            var courseCertificate = _context.CourseCertificates.SingleOrDefault(
                cc => cc.TraineeId == traineeId && cc.CourseId == courseId
            );

            if (courseCertificate == null)
            {
                return null;
            }

            _context.CourseCertificates.Remove(courseCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourseCertificate(
                courseCertificate
            );
        }

        public CourseCertificateDto? Update(
            int traineeId,
            int courseId,
            UpdateCourseCertificateDto updateCourseCertificateDto
        )
        {
            var courseCertificate = _context.CourseCertificates.SingleOrDefault(
                cc => cc.TraineeId == traineeId && cc.CourseId == courseId
            );

            if (courseCertificate == null)
            {
                return null;
            }

            courseCertificate.StartDate = updateCourseCertificateDto.StartDate;
            courseCertificate.Duration = updateCourseCertificateDto.Duration;

            _context.CourseCertificates.Update(courseCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourseCertificate(
                courseCertificate
            );
        }
    }
}
