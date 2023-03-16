using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public class Card
    {
        /// <summary>
        /// Id do cartão
        /// </summary>
        /// <example> 1 </example>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Número do cartão
        /// </summary>
        /// <example> 5322574141570170 </example>
        [Required(ErrorMessage = "The card number is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Card number must contain only numbers.")]
        [MaxLength(16)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Data de expiração do cartão
        /// </summary>
        /// <example> 08/23 </example>
        [Required(ErrorMessage = "Card expiration date is required.")]
        [MaxLength(5)]
        [RegularExpression("^(0[1-9]|1[0-2])/?([0-9]{2})$", ErrorMessage = "Invalid expiration date")]
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Código CVC do Cartão
        /// </summary>
        /// <example> 427 </example>
        [Required(ErrorMessage = "CVC is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "CVC must contain only numbers")]
        [MaxLength(3)]
        [MinLength(3)]
        public string CVC { get; set; }

        /// <summary>
        /// Número da Conta do Cartão
        /// </summary>
        /// <example> 6640 </example>
        [Required(ErrorMessage = "The account number is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Account number must contain only numbers")]
        [MaxLength(10)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Número da Agência do cartão
        /// </summary>
        /// <example> 2310 </example>
        [Required(ErrorMessage = "The agency number is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Agency number must contain only numbers")]
        [MaxLength(10)]
        public string AgencyNumber { get; set; }

        /// <summary>
        /// Limite TOTAL do cartão
        /// </summary>
        /// <example> 6000 </example>
        [Required(ErrorMessage = "The total limit is required.")]
        public float TotalLimit { get; set; }

        /// <summary>
        /// Limite ATUAL do cartão
        /// </summary>
        /// <example> 3000 </example>
        public float CurrentLimit { get; set; }

        /// <summary>
        /// Status de ativo ou inativo do cartão
        /// </summary>
        /// <example> 'true' or 'false' </example>
        [Required(ErrorMessage = "It is necessary to inform if the card is active. 'true' for yes and 'false' for no.")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Status que indica se o cartão está bloqueado ou não
        /// </summary>
        /// <example> 'true' or 'false' </example>
        public bool IsBlocked { get; set; }

        public static bool IsValidCard(string card)
        {
            // Check if the input string is empty or contains non-numeric characters
            if (string.IsNullOrEmpty(card) || !card.All(char.IsDigit))
                return false;

            // Convert the input string to an array of integers
            int[] cardInt = card.Select(c => c - '0').ToArray();

            // Reverse the array of integers
            Array.Reverse(cardInt);

            // Perform the Luhn algorithm
            int sum = 0;
            for (int i = 0; i < cardInt.Length; i++)
            {
                int digit = cardInt[i];
                if (i % 2 == 1)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }
                sum += digit;
            }

            // Check if the sum is a multiple of 10
            return sum % 10 == 0;
        }

        public static bool IsValidLimit(Card card)
        {
            bool result = true;
            if (card.TotalLimit < card.CurrentLimit)
                return false;

            return result;
        }
    }
}