using Microsoft.AspNetCore.Authorization;

namespace WebStore.API.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
