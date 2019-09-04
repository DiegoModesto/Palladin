using Palladin.Services.ViewModel.User;

namespace Palladin.Services.LogicService.Interfaces
{
    public interface IAuthentication
    {
        UserViewModel Authenticate(LoginPasswordViewModel viewModel);
    }
}
