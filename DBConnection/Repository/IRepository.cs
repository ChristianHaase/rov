using System.Collections.Generic;

namespace DBConnection.Repository
{
    public interface IRepository
    {
        User GetUser(string username);
        User GetUser(int id);
        void CreateUser(User user);
        void EditUser(User user);
        List<User> AllMembers();
        List<Role> AllRoles();
        void ChangeRole(int userId, int roleId);
        void DeleteUser(int userId, int? adminId, string message);
        List<DeletedUsers> DeletedUsers();
        About GetAboutAlliance();
        About GetAboutHorde();
        void ChangeAbout(int id, string about);        
        List<Videos> GetAllVideos();
        void AddNewVideo(Videos video);
        void RemoveVideo(int id);
    }
}
