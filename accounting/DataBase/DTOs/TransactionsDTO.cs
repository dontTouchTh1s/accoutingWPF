using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DataBase.DTOs
{
    public class TransactionsDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public ushort Id { get; set; }

        public long Amount { get; init; }
        public string Date { get; init; }

        public ushort AccountId { get; init; }

        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }

        public string? PersonalAccountNumber { get; init; }
    }
}