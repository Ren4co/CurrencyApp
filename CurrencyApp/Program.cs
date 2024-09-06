using Autofac;
using CurrencyApp.BLL;
using CurrencyApp.BLLContacts;
using CurrencyApp.DALContracts;
using CurrencyApp.DI;
using CurrencyApp.Domain.Entity;
using CurrencyApp.Domain.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule<DIConfigModule>();

        var container = containerBuilder.Build();

        var moneyOperation = container.Resolve<IMoneyOperation>();

        var currencyConvert = container.Resolve<ICurrencyConverter>();

        var currencyDao = container.Resolve<ICurrencyDao>();

        var rates = currencyDao.GetCurrencyRates();


        while (true)
        {

            Console.WriteLine("Выберите операцию : " +
                "\n 1 - Суммировать деньги;" +
                "\n 2 - Вычесть деньги;" +
                "\n 3 - Конвертировать деньги;" +
                "\n 4 - Выход;");

            var choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите валюту первой суммы(USD, EUR, GBR):");
                    var currencyCode1 = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(currencyCode1, out Currency currency1) || !Enum.IsDefined(typeof(Currency), currency1))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    Console.WriteLine("Введите значение первой суммы:");
                    if (!decimal.TryParse(Console.ReadLine(), out var value1))
                    {
                        Console.WriteLine("Некорректное значение.");
                        continue;
                    }

                    var money1 = new Money { Value = value1, Currency = currency1 };

                    Console.WriteLine("Введите валюту второй суммы(USD, EUR, GBR):");
                    var currencyCode2 = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(currencyCode2, out Currency currency2) || !Enum.IsDefined(typeof(Currency), currency1))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    Console.WriteLine("Введите значение второй суммы:");
                    if (!decimal.TryParse(Console.ReadLine(), out var value2))
                    {
                        Console.WriteLine("Некорректное значение.");
                        continue;
                    }

                    var money2 = new Money { Value = value2, Currency = currency2 };

                    Console.WriteLine("Введите в какую валюту будет конвертация итоговой суммы(USD, EUR, GBR):");
                    var dastinationCurrent = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(dastinationCurrent, out Currency currency3) || !Enum.IsDefined(typeof(Currency), currency3))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    try
                    {
                        var result = moneyOperation.Add(money1, money2, currency3);

                        Console.WriteLine($"Результат: {result.Value} {result.Currency}");
                    }
                    catch(Exception ex)
                    {
                        throw new Exception($"Ошибка: {ex.Message}");
                    }
                    



                    break;
                case "2":
                    Console.WriteLine("Введите валюту первой суммы (USD, EUR, GBR):");
                    var currencyCodeSt1 = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(currencyCodeSt1, out Currency currencySt1) || !Enum.IsDefined(typeof(Currency), currencySt1))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    Console.WriteLine("Введите значение первой суммы:");
                    if (!decimal.TryParse(Console.ReadLine(), out var valueSt1))
                    {
                        Console.WriteLine("Некорректное значение.");
                        continue;
                    }

                    var moneySt1 = new Money { Value = valueSt1, Currency = currencySt1 };

                    Console.WriteLine("Введите валюту второй суммы (USD, EUR, GBR):");
                    var currencyCodeSt2 = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(currencyCodeSt2, out Currency currencySt2) || !Enum.IsDefined(typeof(Currency), currencySt2))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    Console.WriteLine("Введите значение второй суммы:");
                    if (!decimal.TryParse(Console.ReadLine(), out var valueSt2))
                    {
                        Console.WriteLine("Некорректное значение.");
                        continue;
                    }

                    var moneySt2 = new Money { Value = valueSt2, Currency = currencySt2 };

                    Console.WriteLine("Введите валюту, в которую нужно конвертировать итоговую сумму (USD, EUR, GBR):");
                    var destinationCurrencyCode = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(destinationCurrencyCode, out Currency destinationCurrency) || !Enum.IsDefined(typeof(Currency), destinationCurrency))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    try
                    {
                        var result = moneyOperation.Subtract(moneySt1, moneySt2, destinationCurrency);
                        Console.WriteLine($"Результат вычитания: {result.Value} {result.Currency}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }

                    break;
                case "3":
                    Console.WriteLine("Введите валюту из которой будет проходить конвертация суммы (USD, EUR, GBR):");
                    var fromCurrency = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(fromCurrency, out Currency currencyFrom) || !Enum.IsDefined(typeof(Currency), currencyFrom))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    Console.WriteLine("Введите значение суммы:");
                    if (!decimal.TryParse(Console.ReadLine(), out var valueForConvert))
                    {
                        Console.WriteLine("Некорректное значение.");
                        continue;
                    }

                    Console.WriteLine("Введите валюту в которую будет проходить конвертация суммы (USD, EUR, GBR):");
                    var toCurrency = Console.ReadLine().ToUpper();
                    if (!Enum.TryParse(toCurrency, out Currency currencyTo) || !Enum.IsDefined(typeof(Currency), currencyTo))
                    {
                        Console.WriteLine("Неизвестная валюта.");
                        continue;
                    }

                    try
                    {
                        var resultConvert = currencyConvert.Convert(valueForConvert, currencyFrom, currencyTo, rates);
                        Console.WriteLine($"Результат: {resultConvert} {currencyTo}");
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Такой операции не существует!");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine();


        }
    }
}