using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DBConnection.Repository
{
    public interface IForumRepository
    {
        List<Forum> AllForums();
        List<Subforum> AllSubforumsForForum(int id);
        Subforum Subforum(int id);
        List<Thread> AllThreadsForSubforum(int id);
        Thread Thread(int id);
        List<Comment> AllCommentsForThread(int id);
        void CreateSubforumHorde(Subforum subforum);
        void CreateSubforumAlliance(Subforum subforum);
        void CreateSubforumBoth(Subforum subforum);
        void CreateThread(Thread thread);
        void CreateComment(Comment comment);
        void DeleteSubforum(int id);
        void DeleteThread(int id);
        void DeleteComment(int id);
        void DeleteCommentWithMessage(int id);
        void SetSticky(int id);
        void SetReadOnly(int id);
    }
}
