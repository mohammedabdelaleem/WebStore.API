﻿using Microsoft.AspNetCore.Authorization;

namespace WebStore.API.Authentication.Filters;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
	public string Permission { get; } = permission;
}
