using Microsoft.Extensions.Logging;
using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Views;


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
            builder.Services.AddSingleton<LoginPageView>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<SignUpView>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<AddCriticViewModel>();
            builder.Services.AddSingleton<AddCriticView>();
            builder.Services.AddSingleton<AllRecipeView>();
            builder.Services.AddSingleton<AllRecipeViewModel>();
            builder.Services.AddSingleton<AddRecipeView>();
            builder.Services.AddSingleton<AddRecipeViewModel>();
            builder.Services.AddSingleton<AdminPageView>();
            builder.Services.AddSingleton<AdminPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
