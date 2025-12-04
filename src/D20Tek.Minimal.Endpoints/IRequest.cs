namespace D20Tek.Minimal.Endpoints;

public interface IRequest<out TResult> where TResult : IResult
{
}
