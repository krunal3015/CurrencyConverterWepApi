﻿using System.Web;
using System.Web.Mvc;

namespace Zed.CurrencyConverter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
