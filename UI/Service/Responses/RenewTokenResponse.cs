﻿namespace UI.Service.Responses;

public class RenewTokenResponse
{
    public bool Success { get; set; } = true;
    
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
}