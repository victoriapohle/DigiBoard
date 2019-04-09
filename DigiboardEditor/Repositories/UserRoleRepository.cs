using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public class UserRoleRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();

        private static UserRoleRepository _instance;

        public static UserRoleRepository Instance
        {
            get { return _instance ?? (_instance = new UserRoleRepository()); }
        }



        public ObservableCollection<UserRole> GetAll()
        {
            ObservableCollection<UserRole> userRoles = new ObservableCollection<UserRole>(DB.UserRoles.ToList());
            return userRoles;
        }
    }
}

