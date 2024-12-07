using MaH.IOC.SampleWeb.Services;
using MaH.IOC.Web;
using System;

namespace MaH.IOC.SampleWeb
{
    public partial class HomeServiceLocatorInjection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = HttpContextServiceLocator.ServiceProvider.GetService<IScopedService>();
            StringRepeater.DataSource = service.GetValues();
            StringRepeater.DataBind();
        }
    }
}