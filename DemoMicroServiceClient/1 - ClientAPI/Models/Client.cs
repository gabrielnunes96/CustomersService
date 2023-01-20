using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientAPI.Models
{
    public class Client
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage ="O nome do usuário é obratório.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage ="Números e caracteres especiais não são permitidos no nome.")]
        public string? _userName { get; set; }

        [Required(ErrorMessage ="O tipo de conta é obrigatório")]
        public string? _accountType { get; set; }

        [Required]
        [RegularExpression(@" (^\d{3}\.\d{3}\.\d{ 3}\-\d{ 2}$)| (^\d{ 2}\.\d{ 3}\.\d{ 3}\/\d{ 4}\-\d{ 2}$)", ErrorMessage = "Entre com um CPF/CNPJ válido.")]
        public string? _idNumber { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Para este campo, utilize apenas números")]
        public string? _accountNumber { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Para este campo, utilize apenas números")]
        public string? _agencyNumber  { get; set; }

        public bool _isActive { get; set; }
    }
}
