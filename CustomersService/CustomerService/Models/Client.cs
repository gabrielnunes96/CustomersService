using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CustomerService.Models
{
    /// <summary>
    /// Entidade que representa a tabela Client
    /// </summary>
    public class Client : IValidatableObject    
    {
        private readonly Regex onlyNumbers = new Regex("^\\d+$");
        private readonly Regex onlyLettersAndSpaces = new Regex("^[a-zA-Z ]+$");
        private readonly Regex validateCPF = new Regex("(^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$)");
        private readonly Regex validateCNPJ = new Regex("(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)");


        /// <summary>
        /// Id do cliente
        /// </summary>
        /// <example>10</example>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Cliente
        /// </summary>
        /// <example>Gabriel Nunes Campos</example>
        [Required(ErrorMessage = "O nome do usuário é obratório.")]
        [MaxLength(30)]
        public string? userName { get; set; }

        /// <summary>
        /// Tipo de Identificador do Cliente
        /// </summary>
        /// <example>PF para pessoa física e PJ para pessoa jurídica.</example>
        [Required(ErrorMessage = "O tipo de identificador ('PF' ou 'PJ') é obrigatório.")]
        public string? accountType { get; set; }

        /// <summary>
        /// CPF ou CNPJ do cliente
        /// </summary>
        /// <example>999.999.999-99/99.999.999/0001-99 </example>
        [Required (ErrorMessage = "O número do identificador é obrigatório")]
        public string? idNumber { get; set; }

        /// <summary>
        /// Número da Agência
        /// </summary>
        /// <example>1234</example>
        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        [MaxLength(10)]
        public string? agencyNumber { get; set; }

        /// <summary>
        /// Número da Conta
        /// </summary>
        /// <example>2454</example>
        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [MaxLength(10)]
        public string? accountNumber { get; set; }

        /// <summary>
        /// Indica se o cliente está ativo ou não
        /// </summary>
        /// <example>True para cliente Ativo e False para cliente Inativo</example>
        public bool isActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!onlyLettersAndSpaces.IsMatch(userName))
                yield return new ValidationResult("Nome de usuário deve conter apenas letras e espaços em branco");

            if (!accountType.Equals("PF") && (!accountType.Equals("PJ")) && (!accountType.Equals("pf")) && (!accountType.Equals("pj")))
                yield return new ValidationResult("Tipo da conta deve ser 'PF' para pessoa física ou 'PJ' para pessoa jurídica.");

            if (accountType.Equals("PF") && (!validateCPF.IsMatch(idNumber)))
            {
                    yield return new ValidationResult("CPF inválido.");
            } else
            {
                if ((accountType.Equals("PJ")) && (!validateCNPJ.IsMatch(idNumber)))
                        yield return new ValidationResult("CNPJ inválido.");
            }

            if (!onlyNumbers.IsMatch(agencyNumber))     
                yield return new ValidationResult("Número da agência deve conter apenas números.");         

            if (!onlyNumbers.IsMatch(accountNumber))
                yield return new ValidationResult("Número da conta deve conter apenas números.");

        }
    }
}
