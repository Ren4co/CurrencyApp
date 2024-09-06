using CurrencyApp.Domain.Enum;

namespace CurrencyApp.DALContracts
{
    public interface ICurrencyDao
    {
        public IDictionary<(Currency, Currency), decimal> GetCurrencyRates();
    }
}
