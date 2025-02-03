using Microsoft.Extensions.Logging;
using GlutenFreeApp.ViewModel;
using GlutenFreeApp.Views;
using GlutenFreeApp.Services;
using CommunityToolkit.Maui;

namespace GlutenFreeApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Toronto College.ttf", "TorontoFont");
            }).UseMauiCommunityToolkit();
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
            builder.Services.AddTransient<InfoTabs>();
            builder.Services.AddTransient<AllRestaurantsView>();
            builder.Services.AddTransient<AllRestaurantsViewModel>();
            builder.Services.AddTransient<UpdateProfileView>();
            builder.Services.AddTransient<UpdateProfileViewModel>();
            builder.Services.AddTransient<UpdateRestaurantProfileViewModel>();
            builder.Services.AddTransient<UpdateRestaurantProfileView>();
            builder.Services.AddTransient<AddRestaurantView>();
            builder.Services.AddTransient<AddRestaurantViewModel>();
            builder.Services.AddTransient<PopupPageView>();
            builder.Services.AddTransient<PopupPageRecipesView>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}