using Microsoft.EntityFrameworkCore;
using Prescription.Data;

namespace Prescription.Repositories
{
    public class PrescriptionRepository
    {
        private readonly AppDbContext _db;

        public PrescriptionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Prescription.Models.Prescription> AddAsync(Prescription.Models.Prescription prescription)
        {
            _db.Prescriptions.Add(prescription);
            await _db.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription.Models.Prescription?> GetByIdAsync(int id)
        {
            return await _db.Prescriptions
                .Include(p => p.Medicines)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Prescription.Models.Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await _db.Prescriptions
                .Include(p => p.Medicines)
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<Prescription.Models.Prescription>> GetByDoctorIdAsync(int doctorId)
        {
            return await _db.Prescriptions
                .Include(p => p.Medicines)
                .Where(p => p.DoctorId == doctorId)
                .ToListAsync();
        }
    }

}
