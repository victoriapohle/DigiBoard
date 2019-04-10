﻿using System;
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

        ObservableCollection<AnnouncementsNote> Delete(int ID);

        AnnouncementsNote Add(AnnouncementsNote note);
    }

    public class NoteAnnouncementsRepository 
    {

        private static NoteAnnouncementsRepository _instance;

        public static NoteAnnouncementsRepository Instance
        {
            get { return _instance ?? (_instance = new NoteAnnouncementsRepository()); }
        }

        private INoteAnnouncementsRepository _service;

        public INoteAnnouncementsRepository Service
        {
            get { return _service ?? (_service = new NoteAnnouncementsDataService()); }
            set { _service = value; }
        }


    }


    public class NoteAnnouncementsDataService : INoteAnnouncementsRepository
    {
        public DigiboardEntities DB = new DigiboardEntities();

        public ObservableCollection<AnnouncementsNote> Delete(int ID)
        {
            AnnouncementsNote note = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.noteID == ID)).FirstOrDefault();
            note.isDeleted = true;
            DB.SaveChanges();
            return new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.isDeleted == false));
        }

        public AnnouncementsNote Add(AnnouncementsNote note)
        {
            DB.AnnouncementsNotes.Add(note);
            DB.SaveChanges();
            return note;
        }

        public ObservableCollection<AnnouncementsNote> GetAll()
        {
            ObservableCollection<AnnouncementsNote> notes = new ObservableCollection<AnnouncementsNote>(DB.AnnouncementsNotes.Where(x => x.isDeleted == false));
            return notes;
        }
    }
}
