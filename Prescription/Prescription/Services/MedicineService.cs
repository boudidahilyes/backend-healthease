using Prescription.Repositories;

namespace Prescription.Services
{
    public class MedicineService
    {
        private readonly MedicineRepository _repo;

        public MedicineService(MedicineRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> DecrementRemainingAsync(int medicineId)
        {
            return await _repo.DecrementRemainingQuantityAsync(medicineId);
        }
    }
}