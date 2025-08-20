using MDS.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);            
            // Agregar aquí notificaciones personalizadas de usuario
            userIdentity.AddClaim(new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()));            
            return userIdentity;
        }
    }

    public class ApplicationDbContext : OracleDatabase
    {
        public ApplicationDbContext() : base("DB_MDS2")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
