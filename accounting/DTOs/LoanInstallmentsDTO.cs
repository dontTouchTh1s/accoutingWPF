using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DTOs
{
    public class LoanInstallmentsDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int LoanId { get; set; }
        [ForeignKey("LoanId")] public LoansDTO Loan { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
    }
}