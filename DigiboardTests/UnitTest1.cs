using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DigiboardEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DigiboardTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void SetUpRepository()
        {
            var notes = new ObservableCollection<AnnouncementsNote>()
                {
                    new AnnouncementsNote(){ noteID = 123, announcementHeader = "TEST HEADER", announcementBody = "TEST"},
                    new AnnouncementsNote(){ noteID = 1234, announcementHeader = "TEST HEADER2", announcementBody = "TEST2"}
                };
            var notesRepo = new Mock<INoteAnnouncementsRepository>();
            notesRepo.Setup(x => x.GetAll())
                .Returns(notes);
            notesRepo.Setup(x => x.Add(It.IsAny<AnnouncementsNote>()))
                .Returns((AnnouncementsNote note) =>
                {
                    notes.Add(note);
                    return note;
                });

            var users = new ObservableCollection<User>()
            {
                new User(){userName = "victoria", email= "victoria@southern.edu", isDeleted = false},
                new User(){userName = "john", email= "john@southern.edu", isDeleted = false},
                new User(){userName = "javen", email= "javen@southern.edu", isDeleted = false}
            };
            var usersRepo = new Mock<IUserRepository>();
            usersRepo.Setup(x => x.GetAll())
                .Returns(users);


            var userRoles = new ObservableCollection<UserRole>()
            {
                new UserRole(){roleName = "Secretary", roleID = 1, isDeleted = false },
                new UserRole(){roleName = "Professor", roleID = 2, isDeleted = false },
                new UserRole(){roleName = "Lab Monitor", roleID = 3, isDeleted = false }

            };
            var userRoleRepo = new Mock<IUserRoleRepository>();
            userRoleRepo.Setup(x => x.GetAll())
                .Returns(userRoles);


            UserRoleRepository.Instance.Service = userRoleRepo.Object;
            UserRespository.Instance.Service = usersRepo.Object;
            NoteAnnouncementsRepository.Instance.Service = notesRepo.Object;


        } 

        //method name | scenario | result
        [TestMethod]
        public void GetAllNotes_OneBack()
        {
            var results = NoteAnnouncementsRepository.Instance.Service.GetAll();
            Assert.AreEqual(2, results.Count);
        }
        [TestMethod]
        public void GetAllUsers_OneBack()
        {
            var results = UserRespository.Instance.Service.GetAll();
            Assert.AreEqual(3, results.Count);
        }
        [TestMethod]
        public void GetAllUserRoles_OneBack()
        {
            var results = UserRoleRepository.Instance.Service.GetAll();
            Assert.AreEqual(3, results.Count);
        }
        [TestMethod]

        public void DeleteOne_AllBack()
        {
            //var results1 = NoteAnnouncementsRepository.Instance.Service.Delete(123);

            //Assert.AreEqual(1, results1.Count);
        }

    }
}
