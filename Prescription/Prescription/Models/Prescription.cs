using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Prescription.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public List<Medicine>? Medicines { get; set; }
    }
}
