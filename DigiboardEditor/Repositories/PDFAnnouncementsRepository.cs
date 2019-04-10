
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface IPDFAnnouncementsRepository
    {
        ObservableCollection<AnnouncementsPDF> GetAll();

        ObservableCollection<AnnouncementsPDF> Delete(int ID);

        AnnouncementsPDF Add(AnnouncementsPDF note);
    }

    public class PDFAnnouncementsRepository
    {

        private static PDFAnnouncementsRepository _instance;

        public static PDFAnnouncementsRepository Instance
        {
            get { return _instance ?? (_instance = new PDFAnnouncementsRepository()); }
        }

        private IPDFAnnouncementsRepository _service;

        public IPDFAnnouncementsRepository Service
        {
            get { return _service ?? (_service = new PDFAnnouncementsDataService()); }
            set { _service = value; }
        }


    }


    public class PDFAnnouncementsDataService : IPDFAnnouncementsRepository
    {
        public DigiboardEntities DB = new DigiboardEntities();

        public ObservableCollection<AnnouncementsPDF> Delete(int ID)
        {
            AnnouncementsPDF note = new ObservableCollection<AnnouncementsPDF>(DB.AnnouncementsPDFs.Where(x => x.pdfID == ID)).FirstOrDefault();
            note.isDeleted = true;
            DB.SaveChanges();
            return new ObservableCollection<AnnouncementsPDF>(DB.AnnouncementsPDFs.Where(x => x.isDeleted == false));
        }

        public AnnouncementsPDF Add(AnnouncementsPDF note)
        {
            DB.AnnouncementsPDFs.Add(note);
            DB.SaveChanges();
            return note;
        }

        public ObservableCollection<AnnouncementsPDF> GetAll()
        {
            ObservableCollection<AnnouncementsPDF> notes = new ObservableCollection<AnnouncementsPDF>(DB.AnnouncementsPDFs.Where(x => x.isDeleted == false));
            return notes;
        }
    }
}
