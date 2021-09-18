using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Helper
    // ReSharper restore CheckNamespace
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