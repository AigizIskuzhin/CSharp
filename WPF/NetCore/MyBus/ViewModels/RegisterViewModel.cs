using Microsoft.Extensions.DependencyInjection;

namespace MyBus.ViewModels
{
    public static class RegisterViewModel
    {
        public static IServiceCollection AddViewModels(this IServiceCollection service) => service
            .AddSingleton<MainMenuViewModel>();
    }
}
