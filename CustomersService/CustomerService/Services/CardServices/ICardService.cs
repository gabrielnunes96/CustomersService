namespace CustomerService.Services.CardServices
{
    public interface ICardService
    {
        Task<Card> GetCardById(int id);
        Task<List<Card>> GetAllCards();
        Task<Card> AddCards(Card _card);
        Task<Card> UpdateCard(Card request, int id);
        Task<List<Card>> DeleteCard(int id);
        Task<Card> GetCardIdByCardNumber(string cardNumber);
        Task<bool> Subtract(int id, float value);
        Task<Client> RetornaClientConciliado(string cardNumber);
    }
}