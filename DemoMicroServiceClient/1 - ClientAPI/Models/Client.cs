using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? _accountNumber { get; set; }
        public string? _agencyNumber  { get; set; }
        public string? _userName { get; set; }
        public string? _accountType { get; set; }
        public string? _idNumber { get; set; }
        public bool _isActive { get; set; }
    }
}
