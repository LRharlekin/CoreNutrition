using Mapster;

using CoreNutrition.Application.Authentication.Commands.Register;
using CoreNutrition.Application.Authentication.Queries.Login;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Contracts.Authentication;

namespace CoreNutrition.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterCommand>();
    config.NewConfig<LoginRequest, LoginQuery>();
    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
      .Map(dest => dest.Token, (src) => src.Token)
      .Map(dest => dest.Id, (src) => src.User.Id.Value.ToString())
      .Map(dest => dest, src => src.User);
  }
}