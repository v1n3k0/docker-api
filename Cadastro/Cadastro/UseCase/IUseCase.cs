public interface IUseCase<TRequest, TResponse>
{
    public TResponse? ExecuteAsync(TRequest request);
}