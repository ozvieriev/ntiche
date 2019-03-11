using AutoMapper;
using Owin;
using Site.Data.Entities.Oauth;
using Site.Data.Entities.Test;
using Site.Identity.Models;
using Site.UI.Models;

namespace Site.UI
{
    public partial class Startup
    {
        public void AutoMapper(IAppBuilder app)
        {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<RegisterViewModel, Account>();
                cfg.CreateMap<FeedbackPostViewModel, Feedback>();

                cfg.CreateMap<FeedbackReportGetViewModel, vFeedbackReportViewModel>();
            });
        }
    }
}