using Microsoft.Extensions.Logging;
using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Views;
using GlutenFreeApp.Services;


namespace GlutenFreeApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");


                });
            builder.Services.AddSingleton<GlutenFreeServiceWebAPIProxy>();
            builder.Services.AddTransient<LoginPageView>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<SignUpView>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<AddCriticViewModel>();
            builder.Services.AddTransient<AddCriticView>();
            builder.Services.AddTransient<AllRecipeView>();
            builder.Services.AddTransient<AllRecipeViewModel>();
            builder.Services.AddTransient<AddRecipeView>();
            builder.Services.AddTransient<AddRecipeViewModel>();
            builder.Services.AddTransient<AdminPageView>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<InformationView>();
            builder.Services.AddTransient<InformationViewModel>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShellViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
