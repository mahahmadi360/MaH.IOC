using System.Web.UI;

namespace MaH.IOC.Web.Forms {
	public abstract class InjectablePage : Page {
		public InjectablePage() {
			this.BuildUp();
		}
	}
}
