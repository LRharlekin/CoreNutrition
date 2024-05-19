namespace CoreNutrition.Api.Contracts;

// Contains the API endpoint routes.

public static class ApiRoutes
{

  // Contains the authentication routes.

  public static class Authentication
  {
    public const string Login = "auth/login";
    public const string Register = "auth/register";
    public const string GetTokenResponse = "auth/token";
    public const string Logout = "auth/logout";
  }


  // Contains the users routes.

  public static class Users
  {
    public const string List = "users";
    public const string GetById = "users/{userId:guid}";

    public const string Update = "users/{userId:guid}";

    public const string Delete = "users/{userId:guid}";
    public const string GetWithSession = "profile";
    public const string SetRole = "user/{userId:guid}/role";
  }

  // Contains the categories routes

  public static class Categories
  {
    public const string Create = "categories";
    public const string Update = "categories/{categoryId:guid}";
    // public const string Delete = "categories/{categoryId:guid}";
    public const string List = "categories";
    public const string GetById = "categories/{categoryId:guid}";
    // public const string GetProductLines = "categories/{categoryId:guid}/productlines";
    // public const string GetProducts = "categories/{categoryId:guid}/products";
  }

  // Contains the product lines routes

  public static class ProductLines
  {
    public const string Create = "lines";
    public const string Update = "lines/{productLineId:guid}";
    public const string Delete = "lines/{productLineId:guid}";
    public const string List = "lines";
    public const string GetById = "lines/{productLineId:guid}";
    public const string GetProductsByLine = "lines/{productLineId:guid}/products";
    public const string TogglePublished = "lines/{productLineId:guid}/publish";
  }

  // Contains the products routes

  public static class Products
  {
    public const string Create = "products";
    public const string Update = "products/{productId:guid}";
    public const string Delete = "products/{productId:guid}";
    public const string UpdateQuantityInStock = "products/{productId:guid}/inventory";
    public const string TogglePublished = "products/{productId:guid}/publish";
    public const string List = "products";
    public const string GetById = "products/{productId:guid}";
  }

  // Contains the sizes routes

  public static class ProductLineSizes
  {
    public const string Create = "sizes";
    public const string Update = "sizes/{productLineSizeId:guid}";
    public const string Delete = "sizes/{productLineSizeId:guid}";
    public const string List = "sizes";
    public const string GetById = "sizes/{productLineSizeId:guid}";
    public const string GetByProductLine = "sizes/{productLineId:guid}";
  }

  // Contains the flavours routes

  public static class ProductLineFlavours
  {
    public const string Create = "flavours";
    public const string Update = "flavours/{productLineFlavourId:guid}";
    public const string Delete = "flavours/{productLineFlavourId:guid}";
    public const string List = "flavours";
    public const string GetById = "flavours/{productLineFlavourId:guid}";
    public const string GetByProductLine = "flavours/{productLineId:guid}";
  }

  // Contains the shopping cart routes

  public static class Cart
  {
    public const string EditItems = "cart/{productId:guid}";
    public const string Get = "cart";
    public const string Clear = "cart";
  }

  // Contains the reviews routes.

  public static class Reviews
  {
    public const string Create = "reviews";
    public const string Update = "reviews/{reviewId:guid}";
    public const string Delete = "reviews/{reviewId:guid}";
    public const string List = "reviews";
    public const string GetById = "reviews/{reviewId:guid}";
    public const string GetByProduct = "reviews/{productId:guid}";
    public const string GetByProductLine = "reviews/{productLineId:guid}";
    public const string GetByCustomer = "reviews/{userId:guid}";
  }

  // Contains the discount codes routes.

  public static class DiscountCodes
  {
    public const string Create = "codes";
    public const string Update = "codes/{discountCodeId:guid}";
    public const string Delete = "codes/{discountCodeId:guid}";
    public const string ToggleActive = "codes/{discountCodeId:guid}/activate";
    public const string List = "codes";
    public const string GetById = "codes/{discountCodeId:guid}";
    public const string GetOrders = "codes/{discountCodeId:guid}/orders";
  }

  // Contains the shipping methods routes.

  public static class ShippingMethods
  {
    public const string Create = "shippingmethods";
    public const string Update = "shippingmethods/{shippingMethodId:guid}";
    public const string Delete = "shippingmethods/{shippingMethodId:guid}";
    public const string List = "shippingmethods";
    public const string GetById = "shippingmethods/{shippingMethodId:guid}";
  }

  // Contains the shop orders routes.

  public static class ShopOrders
  {
    public const string Create = "orders";
    public const string Update = "orders/{shopOrderId:guid}";
    public const string Delete = "orders/{shopOrderId:guid}";
    public const string List = "orders";
    public const string GetById = "orders/{shopOrderId:guid}";
    public const string GetByCustomer = "orders/{userId:guid}";
    public const string Cancel = "orders/{shopOrderId:guid}/cancel";
  }

  // Contains the addresses routes.

  public static class Addresses
  {
    public const string Create = "addresses";
    public const string Update = "addresses/{customerAddressId:guid}";
    public const string Delete = "addresses/{customerAddressId:guid}";
    public const string List = "addresses";
    public const string GetById = "addresses/{customerAddressId:guid}";
    public const string GetByCustomer = "addresses/{userId:guid}";
    public const string GetByOrder = "addresses/{shopOrderId:guid}";
  }

  // Contains the user roles routes.

  public static class UserRoles
  {
    public const string Create = "roles";
    public const string Update = "roles/{userRoleId:guid}";
    public const string Delete = "roles/{userRoleId:guid}";
    public const string List = "roles";
    public const string GetById = "roles/{userRoleId:guid}";
    public const string GetByUser = "roles/{userId:guid}";
    public const string GetUsersWithRole = "roles/{userRoleId:guid}/users";
  }

  // Contains the free shipping amount routes

  public static class FreeShippingAmounts
  {
    public const string Update = "freeshippingamount";
    public const string Get = "freeshippingamount";
    public const string ToggleActive = "freeshippingamount/activate";
  }
}