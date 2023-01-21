using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ClientAPI.Models
{
    public class Client : IValidatableObject    
    {
        private readonly Regex onlyNumbers = new Regex("^\\d+$");
        private readonly Regex onlyLettersAndSpaces = new Regex("^[a-zA-Z ]+$");
        private readonly Regex validateCPF = new Regex("(^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$)");
        private readonly Regex valideteCNPJ = new Regex("(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)");

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obratório.")]
        [MaxLength(30)]
        [Display(Name = "Nome do usuário")]
        public string? _userName { get; set; }

        [Required(ErrorMessage = "O tipo de identificador ('PF' ou 'PJ') é obrigatório.")]
        [Display(Name = "Tipo de Conta ('PF' ou 'PJ').")]
        public string? _accountType { get; set; }

        [Required (ErrorMessage = "O número do identificador é obrigatório")]
        [Display(Name = "CPF/CNPJ")]
        public string? _idNumber { get; set; }

        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        [Display(Name = "Agência")]
        [MaxLength(10)]
        public string? _agencyNumber { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [Display(Name = "Conta")]
        [MaxLength(10)]
        public string? _accountNumber { get; set; }

        public bool _isActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!onlyLettersAndSpaces.IsMatch(_userName))
                yield return new ValidationResult("Nome de usuário deve conter apenas letras e espaços em branco");

            if (!(_accountType.Equals("PF")) || !(_accountType.Equals("PJ")))
                yield return new ValidationResult("Tipo da conta deve ser 'PF' para pessoa física ou 'PJ' para pessoa jurídica.");

            if (_accountType == "PF")
                if (!validateCPF.IsMatch(_idNumber))
                    yield return new ValidationResult("CPF inválido.");

            if (_accountType == "PJ")
                if (!valideteCNPJ.IsMatch(_idNumber))
                    yield return new ValidationResult("CNPJ inválido.");

            if (!onlyNumbers.IsMatch(_agencyNumber))     
                yield return new ValidationResult("Número da agência deve conter apenas números.");         

            if (!onlyNumbers.IsMatch(_accountNumber))
                yield return new ValidationResult("Número da conta deve conter apenas números.");

        }
    }
}
