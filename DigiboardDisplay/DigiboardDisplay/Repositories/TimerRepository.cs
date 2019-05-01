using DigiboardDisplay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiboardEditor
{
    public interface ITimersRepository
    {
        ObservableCollection<Timer> GetAll();

        ObservableCollection<Timer> Delete(int ID);

        Timer Add(Timer note);
    }

    public class TimerRepository
    {

        private static TimerRepository _instance;

        public static TimerRepository Instance
        {
            get { return _instance ?? (_instance = new TimerRepository()); }
        }

        private ITimersRepository _service;

        public ITimersRepository Service
        {
            get { return _service ?? (_service = new TimersDataService()); }
            set { _service = value; }
        }


    }


    public class TimersDataService : ITimersRepository
    {
        public DigiboardEntities DB = new DigiboardEntities();

        public ObservableCollection<Timer> Delete(int ID)
        {
            Timer note = new ObservableCollection<Timer>(DB.Timers.Where(x => x.TimerID == ID)).FirstOrDefault();
            note.isDeleted = true;
            DB.SaveChanges();
            return new ObservableCollection<Timer>(DB.Timers.Where(x => x.isDeleted == false));
        }

        public Timer Add(Timer note)
        {
            DB.Timers.Add(note);
            DB.SaveChanges();
            return note;
        }

        public ObservableCollection<Timer> GetAll()
        {
            ObservableCollection<Timer> notes = new ObservableCollection<Timer>(DB.Timers.Where(x => x.isDeleted == false || x.isDeleted == null));
            return notes;
        }
    }
}
