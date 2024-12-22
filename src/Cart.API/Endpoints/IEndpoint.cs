using Microsoft.AspNetCore.Routing;

namespace Cart.API.Endpoints
{
    public interface IEndpoint
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
