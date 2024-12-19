using Microsoft.AspNetCore.SignalR;

namespace Impinj_Reader.Hubs
{
    public class MessageHub : Hub
    {
        // Método para unirse a un grupo
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Cliente {Context.ConnectionId} unido al grupo {groupName}");
        }

        // Método para salir de un grupo
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Cliente {Context.ConnectionId} salió del grupo {groupName}");
        }

        // Método para enviar un mensaje a un grupo específico (opcional)
        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("sendMessage", message);
            Console.WriteLine($"Mensaje enviado al grupo {groupName}: {message}");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Cliente conectado: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Cliente desconectado: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
