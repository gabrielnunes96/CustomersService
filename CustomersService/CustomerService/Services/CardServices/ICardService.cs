namespace CustomerService.Services.CardServices
{
    public interface ICardService
    {
        Task<Card> GetCardById(int id);
        Task<List<Card>> GetAllCards();
        Card GetCardByNumber(string cardnumber);
        Task<Card> AddCards(Card _card);
        Task<Card> UpdateCard(Card request, int id);
        Task<List<Card>> DeleteCard(int id);
    }
}
