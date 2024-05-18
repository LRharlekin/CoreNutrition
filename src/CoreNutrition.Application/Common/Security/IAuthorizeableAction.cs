using MediatR;

namespace CoreNutrition.Application.Common.Security;

public interface IAuthorizeableAction<T> : IRequest<T>
{
  // Guid UserId { get; }
}