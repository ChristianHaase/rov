using DBConnection.Repository;
using DBConnection.Repository.Impl;

namespace DBConnection
{
    public class Facade
    {
        private IRepository _memberRepo;
        private IForumRepository _forumRepo;

        public IRepository GetRepo()
        {
            return _memberRepo ?? (_memberRepo = new Repository.Impl.Repository());
        }

        public IForumRepository GetForumRepo()
        {
            return _forumRepo ?? (_forumRepo = new ForumRepository());
        }
    }
}
