namespace ShoppingLikeFiles.DomainServices.Core;

internal interface INativeCommunicator
{
    public string? Communicate(string? args);
    public Task<string?> CommunicateAsync(string? args);
}
