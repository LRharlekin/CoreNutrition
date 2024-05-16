using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.UserAggregate.ValueObjects;
using CoreNutrition.Domain.UserAggregate.Events;
using CoreNutrition.Domain.ShopOrderAggregate.ValueObjects; // FK, and related / referencing entities
using CoreNutrition.Domain.ReviewAggregate.ValueObjects; // FK, and related / referencing entities
using CoreNutrition.Domain.CustomerAddressAggregate.ValueObjects; // FK, and related / referencing entities
using CoreNutrition.Domain.CartAggregate.ValueObjects;
using CoreNutrition.Domain.Services; // FK, and related / referencing entities


/* 
  FK 
  - ...

  Compleks value objects:
  - Email
  - Password
  - FirstName
  - LastName

  Related / referencing entities:
  - customer address ids
  - cart id // don't inlclude here
  - review ids
  - shop order ids
  */

namespace CoreNutrition.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
  private string _passwordHash;

  private List<CustomerAddressId> _customerAddressIds = new List<CustomerAddressId>();
  private List<ReviewId> _reviewIds = new List<ReviewId>();
  private List<ShopOrderId> _shopOrderIds = new List<ShopOrderId>();

  public FirstName FirstName { get; private set; }
  public LastName LastName { get; private set; }
  public Email Email { get; private set; }

  public IReadOnlyList<CustomerAddressId> CustomerAddressIds => _customerAddressIds.AsReadOnly();
  public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
  public IReadOnlyList<ShopOrderId> ShopOrderIds => _shopOrderIds.AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  // private constructor
  private User(
    UserId userId,
    FirstName firstName,
    LastName lastName,
    Email email,
    string passwordHash,
    DateTime createdDateTime)
    : base(userId)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    _passwordHash = passwordHash;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  // public factory method
  public static User Create(
    FirstName firstName,
    LastName lastName,
    Email email,
    string passwordHash)

  {
    // TODO: Enforce invariants
    var user = new User(
      UserId.CreateUnique(),
      firstName,
      lastName,
      email,
      passwordHash,
      DateTime.UtcNow
    );

    user.AddDomainEvent(new UserCreated(user));

    return user;
  }

  // TODO: invoked by relevant domain events
  // public void AddRelatedEntityId(RelatedEntityId relatedEntityId) // related / referenced entities
  // {
  // _relatedEntityIds.Add(relatedEntityId);
  // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  // }

  public bool VerifyPasswordHash(
    string password,
    IPasswordHashChecker passwordHashChecker)
  {
    return !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);
  }
}