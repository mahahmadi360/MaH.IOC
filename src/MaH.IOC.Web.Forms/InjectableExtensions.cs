using System.Linq;
using System.Reflection;

namespace MaH.IOC.Web.Forms {
	internal static class InjectableExtensions {
		internal static void BuildUp(this object obj) {
			var classType = obj.GetType();
			var fields = classType.GetFields(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance);

			var fieldsWithAttribute = fields.Where(field => field.GetCustomAttributes(typeof(InjectAttribute), true).Any()).ToArray();
			if (!fieldsWithAttribute.Any())
				return;

			var container = HttpContextServiceLocator.ServiceProvider;

			foreach (var field in fieldsWithAttribute) {
				var service = container.GetService(field.FieldType);
				field.SetValue(obj, service);
			}
		}
	}
}
