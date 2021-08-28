using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Pdam.Common.Shared.Helper
{
    public static class ServiceCollectionHelper
    {
        public static IMvcBuilder AddDateTimeFormat(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            return mvcBuilder;
        }
    }
}