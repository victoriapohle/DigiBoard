using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigiboardEditor;
using DigiboardEditor.Pages;
using Moq;
using System.Linq;
//using Telerik.JustMock;


namespace DigiboardEditorTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddNewUser_Test()
        {

            ////var notes = NoteAnnouncementsRepository.Instance.GetAll();
            //var userPage = new AddUserPage();
            //userPage.AddNewUser("Jane Doe", "janedoe@southern.edu", false, 2, "star");
            ////var mockRepo = Mock.Create<INoteAnnouncementsRepository>();
            ////Mock.Arrange(() => mockRepo.HasInventory("Camera", 2)).DoInstead(() => called = true);

            ////userPage.SelectedUserRole = new UserRole() { roleID = 1 };
            //// Set fields like password, username etc. for unit test
            //var mockRepo = new Mock<INoteAnnouncementsRepository>();
            //mockRepo.Setup(x => x.Add(It.IsAny<AnnouncementsNote>())).Ret;

            ////TODO: Mock Repository


        }

        [TestMethod]
        public void DeleteNewUser_Test()
        {


        }
        [TestMethod]
        public void AddNewNoteAnnouncement_Test()
        {



        }
        [TestMethod]
        public void PasswordSaltHash_Test()
        {



        }

    }
}
