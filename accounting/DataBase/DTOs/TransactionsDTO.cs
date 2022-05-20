using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accounting.DataBase.DTOs
{
    public class TransactionsDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public string Date { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")] public AccountDTO Account { get; set; }

        public string? PersonalAccountNumber { get; set; }
    }
}