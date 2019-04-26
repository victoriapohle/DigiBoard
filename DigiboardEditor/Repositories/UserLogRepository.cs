using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface IUserLogRepository
    {
        ObservableCollection<UserLogHistory> GetAll();
        UserLogHistory Add(UserLogHistory inUser);
        void SaveChanges();

    }
    public class UserLogRepository
    {
        private static UserLogRepository _instance;

        public static UserLogRepository Instance
        {
            get { return _instance ?? (_instance = new UserLogRepository()); }
        }
        private IUserLogRepository _service;

        public IUserLogRepository Service
        {
            get { return _service ?? (_service = new UserLogRepositoryDataService()); }
            set { _service = value; }
        }



    }

    public class UserLogRepositoryDataService : IUserLogRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();


        public ObservableCollection<UserLogHistory> GetAll()
        {
            ObservableCollection<UserLogHistory> userRoles = new ObservableCollection<UserLogHistory>(DB.UserLogHistories);
            return userRoles;
        }

        public UserLogHistory Add(UserLogHistory inUser)
        {
            DB.UserLogHistories.Add(inUser);
            DB.SaveChanges();
            return inUser;
        }

        public void SaveChanges()
        {
            DB.SaveChanges();

        }
    }
}

