using System.ComponentModel.DataAnnotations;

namespace CustomerIssuer.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required(ErrorMessage = "The card number is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Card number must contain only numbers.")]
        [MaxLength(16)]
        public string TransactionCardNumber { get; set; }

        public Guid TransactionApprovalId { get; set; }

        public string TransactionDate { get; set; }

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
    }
}
