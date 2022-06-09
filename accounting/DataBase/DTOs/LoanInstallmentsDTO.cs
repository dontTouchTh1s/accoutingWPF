using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DataBase.DTOs
{
    public class LoanInstallmentsDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public ushort Id { get; set; }

        public ushort LoanId { get; init; }
        [ForeignKey("LoanId")] public LoansDTO Loan { get; set; }
        public ulong Amount { get; init; }
        public string Date { get; init; }
    }
}