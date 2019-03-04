using AutoMapper;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Site.Data.Entities.Oauth;
using Site.UI.Models;
using Site.UI.Oauth;
using System;

namespace Site.UI
{
    public partial class Startup
    {
        public void AutoMapper(IAppBuilder app)
        {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<RegisterViewModel, Account>();
            });
        }
    }
}