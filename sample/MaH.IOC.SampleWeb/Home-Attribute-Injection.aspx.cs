using MaH.IOC.SampleWeb.Services;
using MaH.IOC.Web.Forms;
using System;

namespace MaH.IOC.SampleWeb
{
    public partial class HomeAttributeInjection : InjectablePage
    {
        [Inject] protected IScopedService _scopedService;
        protected void Page_Load(object sender, EventArgs e)
        {
            StringRepeater.DataSource = _scopedService.GetValues();
            StringRepeater.DataBind();
        }
    }
}