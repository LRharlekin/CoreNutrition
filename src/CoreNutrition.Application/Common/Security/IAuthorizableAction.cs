using MediatR;

namespace CoreNutrition.Application.Common.Security;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
  Guid UserId { get; }
}