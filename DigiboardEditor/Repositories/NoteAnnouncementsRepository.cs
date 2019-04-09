using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface INoteAnnouncementsRepository
    {
        ObservableCollection<AnnouncementsNote> GetAll();

        void DeleteUser(int ID);

        void Add(AnnouncementsNote note);
    }

    public class NoteAnnouncementsRepository 
    {

        private static INoteAnnouncementsRepository _instance;

        public static INoteAnnouncementsRepository Instance
        {
            get { return _instance ?? (_instance = new NoteAnnouncementsDataService()); }
        }

    }

    public class MyOtherImplementation : INoteAnnouncementsRepository
    {
        public void Add(AnnouncementsNote note)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int ID)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<AnnouncementsNote> GetAll()
        {
            return null;
        }
    }

    public class NoteAnnouncementsDataService : INoteAnnouncementsRepository
    {
        public DigiboardEntities DB = new DigiboardEntities();

        public void DeleteUser(int ID)
        {
            AnnouncementsNote note = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.noteID == ID)).FirstOrDefault();
            note.isDeleted = true;
            DB.SaveChanges();
        }

        public void Add(AnnouncementsNote note)
        {
            DB.AnnouncementsNotes.Add(note);
            DB.SaveChanges();
        }

        public ObservableCollection<AnnouncementsNote> GetAll()
        {
            ObservableCollection<AnnouncementsNote> notes = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.isDeleted == false));
            return notes;
        }
    }
}
