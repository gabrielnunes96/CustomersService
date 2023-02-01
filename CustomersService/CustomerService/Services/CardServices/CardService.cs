using CustomerService.Data.Context;
using CustomerService.Models;

namespace CustomerService.Services.CardServices
{
    public class CardService : ICardService
    {
        private readonly DataContext _dbContext;
        public CardService(DataContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<Card?> GetCardById(int id)
        {
            var uniqueCard = await _dbContext.Cards.FindAsync(id);
            if (uniqueCard is null) return null;
            return uniqueCard;
        }
        public async Task<List<Card?>> GetAllCards()
        {
            return await _dbContext.Cards.ToListAsync();
        }

        public async Task<Card?> AddCards(Card _card)
        {
            _dbContext.Cards.Add(_card);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cards.FindAsync(_card.Id);
        }

        public async Task<Card?> UpdateCard(Card request, int id)
        {
            var card = await _dbContext.Cards.FindAsync(id);
            if (card is null) return null;

            
            card.cardNumber = request.cardNumber;
            card.cardDueDate = request.cardDueDate;
            card.cardCVC = request.cardCVC;
            card.accountNumber = request.accountNumber;
            card.agencyNumber = request.agencyNumber;
            card.totalLimit = request.totalLimit;
            card.isActive = request.isActive;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cards.FindAsync(card.Id);
        }

        public async Task<List<Card?>> DeleteCard(int id)
        {
            var card = await _dbContext.Cards.FindAsync(id);
            if (card is null) return null;

            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Cards.ToListAsync();
        }
    }
}
