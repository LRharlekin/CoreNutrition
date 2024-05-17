using MediatR;

namespace CoreNutrition.Application.Common.Security.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
  Guid UserId { get; }
}