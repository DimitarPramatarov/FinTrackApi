namespace FinTrackApi.Infrastructure.Services
{

    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
