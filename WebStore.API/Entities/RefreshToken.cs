﻿namespace WebStore.API.Entities;

[Owned]
public class RefreshToken
{
	public string Token { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime ExpiresOn { get; set; }

	public DateTime? RevokedOn { get; set; }
	public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
	public bool IsActive => RevokedOn is null && !IsExpired;

}