using Microsoft.AspNetCore.Mvc.ApplicationModels;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace Api.Conventions;

public class GlobalRoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public GlobalRoutePrefixConvention(string prefix)
    {
        _routePrefix = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(prefix));
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var hasRouteAttributes = controller.Selectors.Any(x => x.AttributeRouteModel != null);

            if (hasRouteAttributes)
            {
                foreach (var selector in controller.Selectors.Where(x => x.AttributeRouteModel != null))
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
            }
            else
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = _routePrefix
                });
            }
        }
    }
}