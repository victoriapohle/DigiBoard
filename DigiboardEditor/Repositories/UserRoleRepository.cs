using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface IUserRoleRepository
    {
        ObservableCollection<UserRole> GetAll();
    }
    public class UserRoleRepository
    {
        private static UserRoleRepository _instance;

        public static UserRoleRepository Instance
        {
            get { return _instance ?? (_instance = new UserRoleRepository()); }
        }

        private IUserRoleRepository _service;

        public IUserRoleRepository Service
        {
            get { return _service ?? (_service = new UserRoleRepositoryDataService()); }
            set { _service = value; }
        }

    }
    public class UserRoleRepositoryDataService:IUserRoleRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();



        public ObservableCollection<UserRole> GetAll()
        {
            ObservableCollection<UserRole> userRoles = new ObservableCollection<UserRole>(DB.UserRoles.ToList());
            return userRoles;
        }
    }
}

