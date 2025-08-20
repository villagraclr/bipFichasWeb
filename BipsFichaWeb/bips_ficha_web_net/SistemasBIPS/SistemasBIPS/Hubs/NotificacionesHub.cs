using Microsoft.AspNet.SignalR;

namespace SistemasBIPS.Hubs
{
    public class NotificacionesHub : Hub
    {
        public void Notificacion(int idPrograma)
        {
            Clients.All.liberar(idPrograma);
        }
    }
}