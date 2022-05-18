using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DTOs
{
    public class LoansDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public int InstallmentsCount { get; set; }
        public int ReceiveDate { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }

        public List<LoanInstallmentsDTO>? Installments { get; set; }
    }
}