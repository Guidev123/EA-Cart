using Cart.Application.Response;

namespace Cart.Application.UseCases
{
    public interface IUseCase<Inp, Out>
    {
        Task<Response<Out>> HandleAsync(Inp input);
    }
}
