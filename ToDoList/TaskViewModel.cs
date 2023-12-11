using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoList
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        string name = "";
        private bool check = false;


        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ObservableCollection<ToDoTask> Tasks { get; } = [];

        public TaskViewModel()
        {
            AddCommand = new Command(() =>
            {
                Tasks.Add(new ToDoTask(Name, Check));
                Name = "";
                Check = false;
                Debug.WriteLine($"Task've been added: {Name} ");

            });

            DeleteCommand = new Command<ToDoTask>(DeleteTask);
        }

        private void DeleteTask(ToDoTask task)
        {
            if (task.Check)
            {
                Tasks.Remove(task);
                OnPropertyChanged(nameof(Tasks));
                Debug.WriteLine($"Deleted task: {task.Name}");
            }
            else
            {
                Debug.WriteLine($"Task {task.Name} cannot be deleted because it is not checked.");
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public bool Check
        {
            get => check;
            set
            {
                check = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
