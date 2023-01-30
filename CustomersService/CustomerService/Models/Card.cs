using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do cartão é obrigatório.")]
        [MaxLength(16)]
        public string? cardNumber { get; set; }

        [Required(ErrorMessage = "Data de vencimento do cartão é obrigatório.")]
        public string? cardDueDate { get; set; }

        [Required(ErrorMessage = "O CVC é obrigatório.")]
        [MaxLength(3)]
        public string? cardCVC { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        public string? accountNumber { get; set; }

        [Required(ErrorMessage = "O número da agência é obrigatório.")]
        public string? agencyNumber { get; set; }

        [Required(ErrorMessage = "O limite é obrigatório.")]
        public string? totalLimit { get; set; }

        public string? currentLimit { get; set; }

        [Required(ErrorMessage = "É necessário informar se o cartão está ativo. 'true' para sim e 'false' para não. ")]
        public bool isActive { get; set; }

        public bool isBlocked { get; set; }
    }
}
