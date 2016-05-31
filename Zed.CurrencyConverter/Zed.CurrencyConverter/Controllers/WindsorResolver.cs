using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zed.Core.Resolvers;
using System.Data.Entity;
using Zed.EntityFramework;
using Zed.Core.BusinessInterfaces;
using Zed.Business;
using Zed.Core.DALInterfaces;
using Zed.DataAccessLayer;

namespace Zed.CurrencyConverter.Controllers
{
    public class WindsorResolver : IServiceResolver
    {
        private readonly WindsorContainer _windsorContainer;

        public WindsorResolver()
        {
            _windsorContainer = new WindsorContainer();
            this.MakeBindings();
        }

        private void MakeBindings()
        {
            _windsorContainer.Register(Component.For<IServiceResolver>().Instance(this));
            _windsorContainer.Register(Component.For<DbContext>().Instance(new CurrencyConverterEntities()).LifestyleSingleton());

            _windsorContainer.Register(Component.For<ICurrencyConverterService>().ImplementedBy<CurrencyConverterService>());
            _windsorContainer.Register(Component.For<ICurrencyConverterRepository>().ImplementedBy<CurrencyConverterRepository>());
        }


        public T GetService<T>()
        {
            return _windsorContainer.Resolve<T>();
        }
    }
}