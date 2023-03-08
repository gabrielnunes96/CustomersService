using CustomerService.Data.Context;

namespace CustomerService.Services.CardServices
{
    public class CardService : ICardService
    {
        private readonly DataContext _dbContext;
        public CardService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Card> GetCardById(int id)
        {
            var uniqueCard = await _dbContext.Cards.FindAsync(id);
            if (uniqueCard is null) return null;
            return uniqueCard;
        }
        public async Task<List<Card>> GetAllCards()
        {
            
            return await _dbContext.Cards.ToListAsync();
        }

        public async Task<Card> AddCards(Card _card)
        {
            _dbContext.Cards.Add(_card);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cards.FindAsync(_card.Id);
        }

        public async Task<Card> UpdateCard(Card request, int id)
        {
            var card = await _dbContext.Cards.FindAsync(id);
            if (card is null) return null;


            card.CardNumber = request.CardNumber;
            card.CardExpirationDate = request.CardExpirationDate;
            card.CVC = request.CVC;
            card.AccountNumber = request.AccountNumber;
            card.AgencyNumber = request.AgencyNumber;
            card.TotalLimit = request.TotalLimit;
            card.IsActive = request.IsActive;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Cards.FindAsync(card.Id);
        }

        public async Task<List<Card>> DeleteCard(int id)
        {
            var card = await _dbContext.Cards.FindAsync(id);
            if (card is null) return null;

            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Cards.ToListAsync();
        }

        public async Task<Card> GetCardIdByCardNumber(string cardNumber)
        {

            var uniqueCard = await _dbContext.Cards.Where(uniqueCard => uniqueCard.CardNumber == cardNumber).FirstOrDefaultAsync();
            return uniqueCard;
        }
        public async Task<bool> Subtract(int id, float value)
        {
            try
            {
                var card = await _dbContext.Cards.FindAsync(id);
                card.CurrentLimit -= value;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
