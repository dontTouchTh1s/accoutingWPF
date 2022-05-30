using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DataBase.DTOs
{
    public class TransactionsDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public ushort Id { get; set; }

        public long Amount { get; set; }
        public string Date { get; set; }

        public ushort? AccountId { get; set; }

        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }

        public string? PersonalAccountNumber { get; set; }
    }
}