using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardDisplay
{
    class EventsRepository
    {


        private DigiboardEntities DB = new DigiboardEntities();

        private static EventsRepository _instance;

        public static EventsRepository Instance
        {
            get { return _instance ?? (_instance = new EventsRepository()); }
        }
        public ObservableCollection<Event> GetAll()
        {
            ObservableCollection<Event> notes = new ObservableCollection<Event>(DB.Events.Where(x => x.isDeleted == false));
            return notes;
        }
    }
}
