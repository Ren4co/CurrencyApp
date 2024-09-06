using Autofac;
using CurrencyApp.BLL;
using CurrencyApp.BLLContacts;
using CurrencyApp.DAL;
using CurrencyApp.DALContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.DI
{
    public class DIConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterLogic(builder);
            RegisterDao(builder);
        }
        private void RegisterLogic(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyConverter>().As<ICurrencyConverter>().SingleInstance();
            builder.RegisterType<MoneyOperation>().As<IMoneyOperation>().SingleInstance();
        }
        private void RegisterDao(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyDao>().As<ICurrencyDao>().SingleInstance();
        }
    }

}

