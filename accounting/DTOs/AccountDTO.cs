using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DTOs
{
    public class AccountDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AccountId { get; set; }

        public int Credit { get; set; }
        public string CreateDate { get; set; }
        public string OwnerNationalId { get; set; }
        [ForeignKey("OwnerNationalId")] public PeoplesDTO Owner { get; set; }
        public List<LoansDTO>? Loans { get; set; }
        public List<TransactionsDTO>? Transactions { get; set; }
    }
}