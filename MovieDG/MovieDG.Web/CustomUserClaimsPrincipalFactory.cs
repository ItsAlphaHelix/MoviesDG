//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using MovieDG.Data.Data.Models;
//using System.Security.Claims;
//using System.Threading.Tasks;

//public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
//{
//    public CustomUserClaimsPrincipalFactory(
//        UserManager<ApplicationUser> userManager,
//        IOptions<IdentityOptions> optionsAccessor)
//        : base(userManager, optionsAccessor)
//    {
//    }

//    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
//    {
//        var identity = await base.GenerateClaimsAsync(user);
//        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName)); // Ensure username is used
//        return identity;
//    }
//}