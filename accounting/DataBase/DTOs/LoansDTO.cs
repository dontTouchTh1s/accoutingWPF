using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DataBase.DTOs
{
    public class LoansDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public int InstallmentsCount { get; set; }
        public string LendDate { get; set; } = null!;
        public int AccountId { get; set; }
        public string? PersonalAccountNumber { get; set; }
        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }
        public List<LoanInstallmentsDTO>? Installments { get; set; }
    }
}