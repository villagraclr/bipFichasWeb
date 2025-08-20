using MDS.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SistemasBIPS.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SistemasBIPS
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte su servicio de correo electrónico aquí para enviar correo electrónico.
            return configSendGridasync(message);
            //return Task.FromResult(0);
        }
        private Task configSendGridasync(IdentityMessage message)
        {
            SmtpClient obj = new SmtpClient();
            MailMessage Mailmsg = new MailMessage();
            Mailmsg.From = new MailAddress("monitoreo@desarrollosocial.cl");
            Mailmsg.To.Add(message.Destination);
            Mailmsg.Subject = message.Subject;
            //Mailmsg.Body = message.Body;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            LinkedResource logo = new LinkedResource(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Content\\Images\\logoMDS.png"));
            logo.ContentId = "logoMDS";
            htmlView.LinkedResources.Add(logo);
            Mailmsg.AlternateViews.Add(htmlView);
            //servidor smtp                
            obj.Host = "smtp.mideplan.cl";
            obj.Port = 25;
            //Envia el correo en formato HTML
            Mailmsg.IsBodyHtml = true;
            //Envia el correo
            obj.Send(Mailmsg);
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte el servicio SMS aquí para enviar un mensaje de texto.
            return Task.FromResult(0);
        }
    }

    // Configure el administrador de usuarios de aplicación que se usa en esta aplicación. UserManager se define en ASP.NET Identity y se usa en la aplicación.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            //var manager =     new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>() as OracleDatabase));           
            // Configure la lógica de validación de nombres de usuario
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true                
            };

            // Configure la lógica de validación de contraseñas
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configurar valores predeterminados para bloqueo de usuario
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registre proveedores de autenticación en dos fases. Esta aplicación usa los pasos Teléfono y Correo electrónico para recibir un código para comprobar el usuario
            // Puede escribir su propio proveedor y conectarlo aquí.
            manager.RegisterTwoFactorProvider("Código telefónico", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Su código de seguridad es {0}"
            });
            manager.RegisterTwoFactorProvider("Código de correo electrónico", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de seguridad",
                BodyFormat = "Su código de seguridad es {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("BIPS Identity")) { TokenLifespan = TimeSpan.FromDays(10) };
            }
            return manager;
        }
    }

    // Configure el administrador de inicios de sesión que se usa en esta aplicación.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {                        
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    //public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    //{
    //    private string claimType;
    //    private string claimValue;
    //    public ClaimsAuthorizeAttribute(string type, string value)
    //    {
    //        this.claimType = type;
    //        this.claimValue = value;
    //    }
    //    public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
    //    {
    //        var user = filterContext.HttpContext.User as ClaimsPrincipal;
    //        if (user != null && user.HasClaim(claimType, claimValue))
    //        {
    //            base.OnAuthorization(filterContext);
    //        }
    //        else
    //        {
    //            base.HandleUnauthorizedRequest(filterContext);
    //        }
    //    }
    //}
}
