using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardDisplay
{
    class NoteAnnouncementRepository
    {


        private DigiboardEntities DB = new DigiboardEntities();

        private static NoteAnnouncementRepository _instance;

        public static NoteAnnouncementRepository Instance
        {
            get { return _instance ?? (_instance = new NoteAnnouncementRepository()); }
        }
        public ObservableCollection<AnnouncementsNote> GetAll()
        {
            ObservableCollection<AnnouncementsNote> notes = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.isDeleted == false));
            return notes;
        }

        public void DeleteAnnouncement(int ID)
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
