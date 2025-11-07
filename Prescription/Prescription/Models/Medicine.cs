using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prescription.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public int DosePerDay { get; set; }
        public int MgPerDose { get; set; }
        public int Remaining { get; set; }
        public int PrescriptionId { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public Prescription? Prescription { get; set; }
    }
}
