using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Transpoters
{
    public class TranspotersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Transporters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Transporters_default",
                "Transporters/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}