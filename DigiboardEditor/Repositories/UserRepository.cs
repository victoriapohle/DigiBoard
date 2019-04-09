using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace DigiboardEditor
{
    public class UserRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();

        private static UserRepository _instance;

        public static UserRepository Instance
        {
            get { return _instance ?? (_instance = new UserRepository()); }
        }


        public ObservableCollection<User> GetAll()
        {
            ObservableCollection<User> userRoles = new ObservableCollection<User>(DB.Users.Where(x => x.isDeleted == false));
            return userRoles;
        }

        public void Add(User inUser)
        {
            DB.Users.Add(inUser);
            DB.SaveChanges();
        }

        public void DeleteUser(int userID)
        {
            User user = new ObservableCollection<User>(DB.Users.Where(x => x.UserID == userID)).FirstOrDefault();
            user.isDeleted = true;
            DB.SaveChanges();
        }
    }
}
