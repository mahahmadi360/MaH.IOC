using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Unity;

namespace MaH.IOC.Web.MVC
{
    internal class MaHFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private IServiceProvider _serviceProvider => HttpContextServiceLocator.ServiceProvider;

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> actionAttributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            return BuildUp(actionAttributes);
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> controllerAttributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            return BuildUp(controllerAttributes);
        }

        private IEnumerable<FilterAttribute> BuildUp(IEnumerable<FilterAttribute> attributes)
        {
            foreach (FilterAttribute item in attributes)
            {
                BuildUp(item);
            }

            return attributes;
        }

        private void BuildUp(FilterAttribute item)
        {
            var fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => !field.FieldType.IsValueType && !field.FieldType.IsPrimitive && field.FieldType != typeof(string));

            foreach (var field in fields)
            {
                try
                {
                    var dependency = _serviceProvider.GetService(field.FieldType);
                    field.SetValue(item, dependency);
                }
                catch (ResolutionFailedException) { }
            }
        }
    }
}
