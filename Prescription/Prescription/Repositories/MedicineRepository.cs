using Microsoft.EntityFrameworkCore;
using Prescription.Data;

namespace Prescription.Repositories
{
    public class MedicineRepository
    {
        private readonly AppDbContext _db;

        public MedicineRepository(AppDbContext db)
        {
            _db = db;
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