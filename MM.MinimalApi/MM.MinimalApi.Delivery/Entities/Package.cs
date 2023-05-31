using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MM.MinimalAPI.Delivery.Entities
{
    public class Package
    {
        public Package() { }

        public Package(int packageId, string code)
        {
            PackageId = packageId;
            Code = code;
        }

        [Key]
        [Required]
        public int Id { get; set; } 

        public int PackageId { get; set; }
        public string Code { get; set; }

        [JsonIgnore]
        public List<Location> Locations { get; set; }
    }
}
