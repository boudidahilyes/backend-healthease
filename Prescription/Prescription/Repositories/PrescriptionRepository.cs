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
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var prescription = await _db.Prescriptions
                .Include(p => p.Medicines)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
                return false;

            _db.Medicines.RemoveRange(prescription.Medicines);
            _db.Prescriptions.Remove(prescription);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DecrementRemainingQuantityAsync(int medicineId)
        {
            var medicine = await _db.Medicines
                .FirstOrDefaultAsync(m => m.Id == medicineId && m.Remaining > 0);

            if (medicine == null)
                return false;

            medicine.Remaining -= 1;
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
