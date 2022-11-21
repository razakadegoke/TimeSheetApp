using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace TimeSheetApp.Auth
{
    //Le service qui permet a l'appliacaton d'authentifier et d'autoriser les acces des utilisateurs.
    class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //La mémoire unique utiliser par l'application pour sauvegarder les données de chaque utilisateur
        //entre autre le ClaimsPrincipale.
        private readonly ProtectedSessionStorage sessionStorage;
        //Représente un utilisateur qui n'a aucun donné utile à son authentification.
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        //A chaque nouvelle instance du service d'authentification le sessionStorage du current utilisateur
        //va être passé en paramètre afin que l'application puisse avoir acces aux données de l'utilisateur.
        public CustomAuthStateProvider (ProtectedSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage; 
        }

        //Cette methode va être utiliser par l'application pour déterminer les données qui se trouve
        //dans le sessionStorage de l'utilisateur.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //L'app va essayer de fetch le UserSession dans sessionStorage de l'utilisateur 
                var userSessionStorageRes = await sessionStorage.GetAsync<UserSession>("UserSession");
                //Si le sessionStorage est null cela veut dire qu'aucun utilisateur n'est connecter. 
                //Dans cas ou un UserSession existe dans le sessionStorage on va utiliser les donnés
                //qui se trouvent dans ce UserSession pour créer un nouveau ClaimsPrincipal
                //et update le AuthenticationState avec le ClaimsPrincipal créer précédement.
                var userSession = userSessionStorageRes.Success? userSessionStorageRes.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(anonymous));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name , userSession.UserName)
                    }, "CustomAuth"
                ));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));

            }
        }

        //Cette fonction va permettre de vérifier si l'utilisateur est conncecter ou déconnecter
        //update le AuthenticationState avec un nouveau claimsprincipal
        public async Task UpdateAuthState(UserSession _userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if(_userSession != null)
            {
                await sessionStorage.SetAsync("UserSession", _userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _userSession.UserName)
                }));
            }
            else
            {
                await sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
