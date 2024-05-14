using MediatR;
using ErrorOr;

using CoreNutrition.Application.Authentication.Commands.Register;
using CoreNutrition.Application.Authentication.Common;

namespace CoreNutrition.Application.Common.Behaviors;
/* 
public class ValidateRegisterCommandBehavior<TRequest, TResponse> 
  : IPipelineBehavior<TRequest, TResponse> // TRequest = mediator request we want to send through the pipeline, TResponse = the response we want to get back
  where TRequest : IRequest<TResponse>
*/
public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
    where RegisterCommand : IRequest<TResponse>
{
  public async Task<ErrorOr<AuthenticationResult>> Handle(
    RegisterCommand request, // investigate, log, validate, etc. before calling the handler delegate
    RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next, // will eventually invoke the command handler
    CancellationToken cancellationToken
  )
  {
    // runs before the handler



    // pass RegisterCommand to RegisterCommandHandler
    var result = await next();

    // runs after the pipeline handler before return result

    return result;
  }
}