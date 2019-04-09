using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigiboardEditor;
using DigiboardEditor.Pages;

namespace DigiboardEditorTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var notes = NoteAnnouncementsRepository.Instance.GetAll();
            var userPage = new AddUserPage();
            userPage.SelectedUserRole = new UserRole() { roleID = 1 };
            

        }

    }
}
