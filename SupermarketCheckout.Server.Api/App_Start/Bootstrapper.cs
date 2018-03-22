﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SupermarketCheckout.Server.Api.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure AutoFac  
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);

            //Configure default data
            DatabaseInitializer.Build();
        }
    }
}