using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface IEventsRepository
    {
        ObservableCollection<Event> GetAll();

        ObservableCollection<Event> Delete(int ID);

        Event Add(Event note);
    }

    public class EventsRepository
    {

        private static EventsRepository _instance;

        public static EventsRepository Instance
        {
            get { return _instance ?? (_instance = new EventsRepository()); }
        }

        private IEventsRepository _service;

        public IEventsRepository Service
        {
            get { return _service ?? (_service = new EventsDataService()); }
            set { _service = value; }
        }


    }


    public class EventsDataService : IEventsRepository
    {
        public DigiboardEntities DB = new DigiboardEntities();

        public ObservableCollection<Event> Delete(int ID)
        {
            Event note = new ObservableCollection<Event>(DB.Events.Where(x => x.eventID == ID)).FirstOrDefault();
            note.isDeleted = true;
            DB.SaveChanges();
            return new ObservableCollection<Event>(DB.Events.Where(x => x.isDeleted == false));
        }

        public Event Add(Event note)
        {
            DB.Events.Add(note);
            DB.SaveChanges();
            return note;
        }

        public ObservableCollection<Event> GetAll()
        {
            ObservableCollection<Event> notes = new ObservableCollection<Event>(DB.Events.Where(x => x.isDeleted == false || x.isDeleted == null));
            return notes;
        }
    }
}
