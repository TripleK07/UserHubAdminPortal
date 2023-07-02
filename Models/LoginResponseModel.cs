
namespace UserHubAdminPortal.Models
{
    public class LoginResponseModel
    {
        public String Token { get; set; } = null!;

        public List<Menus> Menus { get; set; } = null!;
    }
}