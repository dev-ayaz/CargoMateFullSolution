using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Transpoters
{
    public class TranspotersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Transpoters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Transpoters_default",
                "Transpoters/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}