namespace stid_management.Config;

public class GraphConfig
{
    public string TenantId  { get; set; }
    public string ClientId  { get; set; }
    public string ClientSecret  { get; set; }
    public string Scopes { get; set; }
    public (string, string, string, string[]) Deconstruct() => 
        (TenantId, ClientId, ClientSecret, Scopes.Split(' '));
}