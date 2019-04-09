using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public class NoteAnnouncementsRepository
    {
        private DigiboardEntities DB = new DigiboardEntities();

        private static NoteAnnouncementsRepository _instance;

        public static NoteAnnouncementsRepository Instance
        {
            get { return _instance ?? (_instance = new NoteAnnouncementsRepository()); }
        }
        public ObservableCollection<AnnouncementsNote> GetAll()
        {
            ObservableCollection<AnnouncementsNote> notes = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.isDeleted == false));
            return notes;
        }

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
    }
}
