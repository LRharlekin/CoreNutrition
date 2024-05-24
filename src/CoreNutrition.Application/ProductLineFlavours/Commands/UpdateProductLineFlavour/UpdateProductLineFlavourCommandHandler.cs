using MediatR;
using ErrorOr;

using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.UpdateProductLineFlavour;

internal sealed class UpdateProductLineFlavourCommandHandler
  : IRequestHandler<UpdateProductLineFlavourCommand, ErrorOr<ProductLineFlavour>>
{
  private readonly IProductLineFlavourRepository _productLineFlavourRepository;
  public UpdateProductLineFlavourCommandHandler(
    IProductLineFlavourRepository productLineFlavourRepository
  )
  {
    _productLineFlavourRepository = productLineFlavourRepository;
  }

  public async Task<ErrorOr<ProductLineFlavour>> Handle(
    UpdateProductLineFlavourCommand command,
    CancellationToken cancellationToken
  )
  {
    await Task.CompletedTask; // TODO delete later

    // cast route param to strongly typed Id
    Guid.TryParse(command.Id, out Guid idGuid);
    ProductLineFlavourId productLineFlavourId = ProductLineFlavourId.Create(idGuid);

    ProductLineFlavour? productLineFlavourResult = _productLineFlavourRepository.GetById(productLineFlavourId);

    if (productLineFlavourResult is null)
    {
      return Errors.ProductLineFlavour.NotFound;
    }

    // cast productlineID to strongly typed Id

    if (command.ProductLineId is not null)
    {
      Guid.TryParse(command.ProductLineId, out Guid guid);
      ProductLineId productLineId = ProductLineId.Create(guid);
    }

    // Update the ProductLineFlavour
    // productLineFlavour.Update(
    //   command.Flavour,
    //   command.ProductLineId,
    //   command.FlavourImageUrl
    // );

    // Save the ProductLineFlavour
    // _productLineFlavourRepository.Update(productLineFlavourResult);

    return productLineFlavourResult;
  }
}