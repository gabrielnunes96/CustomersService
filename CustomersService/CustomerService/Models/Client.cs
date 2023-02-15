using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomerService.Models
{
    public class Client : IValidatableObject
    {
        private static readonly Regex ValidateCPF = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        private static readonly Regex ValidateCNPJ = new Regex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The user name is required.")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "User name must only contain letters and spaces.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The account type (PF or PJ) is required.")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "The ID number is required.")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "The agency number is required.")]
        [MaxLength(10)]
        [RegularExpression("^\\d+$", ErrorMessage = "Agency must only contain numbers.")]
        public string AgencyNumber { get; set; }

        [Required(ErrorMessage = "The account number is required.")]
        [MaxLength(10)]
        [RegularExpression("^\\d+$", ErrorMessage = "Account must only contain numbers.")]
        public string AccountNumber { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AccountType.Equals("PF", StringComparison.OrdinalIgnoreCase) &&
                !AccountType.Equals("PJ", StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("Account type must be 'PF' for individual or 'PJ' for company.");
            }

            if (AccountType.Equals("PF", StringComparison.OrdinalIgnoreCase) && !ValidateCPF.IsMatch(IdNumber))
            {
                yield return new ValidationResult("Invalid CPF. Remember to use '.' and '-' to separate the fields of the CPF.");
            }
            else if (AccountType.Equals("PJ", StringComparison.OrdinalIgnoreCase) && !ValidateCNPJ.IsMatch(IdNumber))
            {
                yield return new ValidationResult("Invalid CNPJ. Remember to use '.', '/', and '-' to separate the fields of the CNPJ.");
            }
        }
    }
}
