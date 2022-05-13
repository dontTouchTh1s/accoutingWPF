using System;
using System.ComponentModel.DataAnnotations;
using accounting.Models;

namespace accounting.DTOs
{
    public class AccountDTO
    {
        [Key] public Guid accountId { get; set; }
        public int Credit { get; set; }
        public DateTime CreateDate { get; set; }
        public string Owner { get; set; }
    }
}