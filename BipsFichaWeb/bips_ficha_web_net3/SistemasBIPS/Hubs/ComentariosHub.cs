using MDS.Dto;
using Microsoft.AspNet.SignalR;

namespace SistemasBIPS.Hubs
{
    public class ComentariosHub : Hub
    {
        public void ComentariosChat(TablaConsultasDto comentario)
        {
            Clients.All.addComentario(comentario);            
        }

        public void BorraComentariosChat(int idPrograma, string idUsuario, int idMenuHijo, int idMenuPadre, int idPregunta)
        {
            Clients.All.deleteComentario(idPrograma, idUsuario, idMenuHijo, idMenuPadre, idPregunta);
        }
    }
}