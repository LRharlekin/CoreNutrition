using MediatR;
using ErrorOr;

using CoreNutrition.Application.Authentication.Common;

namespace CoreNutrition.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>; // defining return value via MediatR