using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;

namespace ProgramAcad.API.Presentation.Helpers
{
    public static class FirebaseInitializer
    {
        public static IServiceCollection AddFirebaseApp(this IServiceCollection services)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(".\\program-acad-google-credentials.json")
            });
            return services;
        }
    }
}
