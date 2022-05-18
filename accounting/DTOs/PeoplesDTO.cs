using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace accounting.DTOs
{
    public class PeoplesDTO
    {
        [Key] [StringLength(10)] public string NationalId { get; set; }
        [MaxLength(15)] [MinLength(3)] public string Name { get; set; }
        [MaxLength(20)] [MinLength(3)] public string LastName { get; set; }
        [MaxLength(15)] [MinLength(3)] public string FatherName { get; set; }
        [StringLength(16)] public string PersonalAccountNumber { get; set; }
        public List<AccountDTO>? Accounts { get; set; }
    }
}