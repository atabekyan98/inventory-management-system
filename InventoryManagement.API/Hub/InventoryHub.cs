using Microsoft.AspNetCore.SignalR;

public class InventoryHub : Hub
{
    public async Task SubscribeToStockUpdates(int productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"product-{productId}");
    }
}