using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CustomerIssuer.Models
{
    public class Transaction
    {
        /// <summary>
        /// Id da transação
        /// </summary>
        /// <example> 3D17C904-2F6D-483D-A464-0F048EE6EE99 </example>
        [Key]
        [JsonIgnore]
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Id de aprovação da transação
        /// </summary>
        /// <example> F340A703-F6BE-4ED3-A4D7-35516DCEF1CD </example>
        [JsonIgnore]
        public Guid TransactionApprovalId { get; set; }

        /// <summary>
        /// Número do cartão de crédito da transação
        /// </summary>
        /// <example> 5320123433402171 </example>
        [Required(ErrorMessage = "The card number is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Card number must contain only numbers.")]
        [MaxLength(16)]
        public string TransactionCardNumber { get; set; }

        /// <summary>
        /// Data da transação
        /// </summary>
        /// <example> 15/03/2023 01:37:42 </example>
        public string TransactionDate { get; set; }

        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <example> 500 </example>
        [Required(ErrorMessage = "The transaction value is required.")]
        public float TransactionValue { get; set; }

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

        public static bool isValidValue(float value)
        {
            bool result = true;
            if (value < 0)
                result = false;

            return result ;

        }
    }
}
