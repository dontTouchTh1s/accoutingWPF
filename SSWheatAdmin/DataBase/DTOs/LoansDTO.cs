using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSWheatAdmin.DataBase.DTOs
{
    public class LoansDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public ushort Id { get; set; }

        public ulong Amount { get; set; }
        public byte InstallmentsCount { get; set; }
        public string LendDate { get; set; } = null!;
        public ushort AccountId { get; set; }
        public string? PersonalAccountNumber { get; set; }
        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }
        public List<LoanInstallmentsDTO>? Installments { get; set; }
    }
}