using System;
using System.ComponentModel.DataAnnotations;

namespace accounting.DTOs
{
    public class PeopleDTO
    {
        [Key][StringLength(10)] public string NationalId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PersonalAccountNumber { get; set; }
    }
}