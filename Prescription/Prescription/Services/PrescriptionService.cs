using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository _repo;

        public PrescriptionService(PrescriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Prescription.Models.Prescription> CreateAsync(Prescription.Models.Prescription prescription)
        {
            prescription.CreatedAt = DateTime.Now;

            if (prescription.Medicines == null || prescription.Medicines.Count == 0)
                throw new ArgumentException("A prescription must contain at least one medicine.");

            foreach (var m in prescription.Medicines)
            {
                m.Prescription = prescription;
            }

            return await _repo.AddAsync(prescription);
        }

        public async Task<Prescription.Models.Prescription?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Prescription.Models.Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await _repo.GetByPatientIdAsync(patientId);
        }
        public async Task<List<Prescription.Models.Prescription>> GetByDoctorIdAsync(int doctorId)
        {
            return await _repo.GetByDoctorIdAsync(doctorId);
        }
    }
}
