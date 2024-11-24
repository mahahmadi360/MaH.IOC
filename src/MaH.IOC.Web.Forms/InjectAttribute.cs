using System;

namespace MaH.IOC.Web.Forms {
	/// <summary>
	/// use this attribute to inject dependencies in webforms components like pages, usercontrols, datasources,...
	/// it is possible in controls that inherited from base abstract classes in this library
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class InjectAttribute : Attribute { }
}
