using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CustomerService.Models
{
    public class Client : IValidatableObject    
    {
        private readonly Regex onlyNumbers = new Regex("^\\d+$");
        private readonly Regex onlyLettersAndSpaces = new Regex("^[a-zA-Z ]+$");
        private readonly Regex validateCPF = new Regex("(^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$)");
        private readonly Regex validateCNPJ = new Regex("(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)");

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obratório.")]
        [MaxLength(30)]
        public string? _userName { get; set; }

        [Required(ErrorMessage = "O tipo de identificador ('PF' ou 'PJ') é obrigatório.")]
        public string? _accountType { get; set; }

        [Required (ErrorMessage = "O número do identificador é obrigatório")]
        public string? _idNumber { get; set; }

        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        [MaxLength(10)]
        public string? _agencyNumber { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [MaxLength(10)]
        public string? _accountNumber { get; set; }

        public bool _isActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!onlyLettersAndSpaces.IsMatch(_userName))
                yield return new ValidationResult("Nome de usuário deve conter apenas letras e espaços em branco");

            if (!_accountType.Equals("PF") && (!_accountType.Equals("PJ")) && (!_accountType.Equals("pf")) && (!_accountType.Equals("pj")))
                yield return new ValidationResult("Tipo da conta deve ser 'PF' para pessoa física ou 'PJ' para pessoa jurídica.");

            if (_accountType.Equals("PF") && (!validateCPF.IsMatch(_idNumber)))
            {
                    yield return new ValidationResult("CPF inválido.");
            } else
            {
                if ((_accountType.Equals("PJ")) && (!validateCNPJ.IsMatch(_idNumber)))
                        yield return new ValidationResult("CNPJ inválido.");
            }

            if (!onlyNumbers.IsMatch(_agencyNumber))     
                yield return new ValidationResult("Número da agência deve conter apenas números.");         

            if (!onlyNumbers.IsMatch(_accountNumber))
                yield return new ValidationResult("Número da conta deve conter apenas números.");

        }
    }
}
