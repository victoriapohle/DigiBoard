using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace DigiboardEditor
{   public interface IUserRepository
    {
        ObservableCollection<User> GetAll();
        int DeleteUser(int userID);
        User Add(User inUser);
        void SaveChanges();

    }
    public class UserRespository
    {
        private static UserRespository _instance;

        public static UserRespository Instance
        {
            get { return _instance ?? (_instance = new UserRespository()); }
        }
        private IUserRepository _service;

        public IUserRepository Service
        {
            get { return _service ?? (_service = new UserRepositoryDataService()); }
            set { _service = value;}
        }



    }

    public class UserRepositoryDataService: IUserRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();


        public ObservableCollection<User> GetAll()
        {
            ObservableCollection<User> userRoles = new ObservableCollection<User>(DB.Users.Where(x => x.isDeleted == false));
            return userRoles;
        }

        public User Add(User inUser)
        {
            DB.Users.Add(inUser);
            DB.SaveChanges();
            return inUser;
        }

        public int DeleteUser(int userID)
        {
            User user = new ObservableCollection<User>(DB.Users.Where(x => x.UserID == userID)).FirstOrDefault();
            user.isDeleted = true;
            return DB.SaveChanges();

        }
        public void SaveChanges()
        {
            DB.SaveChanges();

        }
    }
}
