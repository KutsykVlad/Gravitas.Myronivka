using System.Collections.Generic;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.Attribute
{
    public class PerformanceTestFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return new[] {new Filter(new PerformanceTestFilter(), FilterScope.Global, 0)};
        }
    }
}