using MediatR;
using ErrorOr;

using CoreNutrition.Application.Authentication.Common;

namespace CoreNutrition.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>; // defining return value via MediatR