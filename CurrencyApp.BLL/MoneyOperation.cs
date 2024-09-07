using CurrencyApp.BLLContacts;
using CurrencyApp.DALContracts;
using CurrencyApp.Domain.Entity;
using CurrencyApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.BLL
{
    public class MoneyOperation : IMoneyOperation
    {
        private readonly ICurrencyConverter _currencyConverter;
        private readonly ICurrencyDao _currencyDao;

        public MoneyOperation(ICurrencyConverter currencyConverter, ICurrencyDao currencyDao)
        {
            _currencyConverter = currencyConverter;
            _currencyDao = currencyDao;
        }

        public Money Add(Money money1, Money money2,Currency destinationCurrency)
        {
            try
            {
                decimal value = 0;

                IDictionary<(Currency, Currency), decimal> rates = null;

                if (money1.Currency != destinationCurrency || money2.Currency != destinationCurrency)
                {
                    rates = _currencyDao.GetCurrencyRates();
                }

                if (money1.Currency == destinationCurrency)
                {
                    value += money1.Value;
                }
                else
                {
                    var convertedValue = _currencyConverter.Convert(money1.Value, money1.Currency, destinationCurrency, rates);
                    value += convertedValue;
                }

                if (money2.Currency == destinationCurrency)
                {
                    value += money2.Value;
                }
                else
                {

                    var convertedValue = _currencyConverter.Convert(money2.Value, money2.Currency, destinationCurrency, rates);
                    value += convertedValue;
                }
                var result = new Money() { Value = value, Currency = destinationCurrency };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка сложения", ex);
            }

        }

        public Money Subtract(Money money1, Money money2, Currency destinationCurrency)
        {
            try
            {
                decimal value = 0;

                IDictionary<(Currency, Currency), decimal> rates = null;

                if (money1.Currency != destinationCurrency || money2.Currency != destinationCurrency)
                {
                    rates = _currencyDao.GetCurrencyRates();
                }

                if (money1.Currency == destinationCurrency)
                {
                    value += money1.Value;
                }
                else
                {
                    var convertedValue = _currencyConverter.Convert(money1.Value, money1.Currency, destinationCurrency, rates);
                    value += convertedValue;
                }

                if (money2.Currency == destinationCurrency)
                {
                    value -= money2.Value;
                }
                else
                {
                    var convertedValue = _currencyConverter.Convert(money2.Value, money2.Currency, destinationCurrency, rates);
                    value -= convertedValue;
                }

                if (value < 0)
                {
                    throw new InvalidOperationException("Результат вычитания не может быть отрицательным.");
                }

                var result = new Money() { Value = value, Currency = destinationCurrency };
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка вычитания", ex);
            }
        }
        
        public decimal Convert(Currency fromCurrency, Currency toCurrancy, decimal Value)
        {
            var rates = _currencyDao.GetCurrencyRates();

            try
            {
                var result = _currencyConverter.Convert(Value, fromCurrency, toCurrancy, rates);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка конвертации", ex);
            }



        }
    }
}
