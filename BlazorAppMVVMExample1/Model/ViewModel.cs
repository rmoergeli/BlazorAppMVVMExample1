using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorAppMVVMExample1.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> list = new List<string>();
        public List<string> List
        {
            get => list;
            private set
            {
                list = value;
                OnPropertyChanged();
            }
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;

                if (isBusy)
                {
                    IsBusySpinner = "spinner-grow text-success";
                }
                else
                {
                    IsBusySpinner = null;
                }

                OnPropertyChanged();
            }
        }

        private string isBusySpinner = string.Empty;
        public string IsBusySpinner
        {
            get => isBusySpinner;
            set
            {
                isBusySpinner = value;
                OnPropertyChanged();
            }
        }

        public void AddItems()
        {
            IsBusy = true;

            CancellationTokenSource stoppingToken = null;
            stoppingToken = new CancellationTokenSource();
            CancellationToken token = stoppingToken.Token;

            for (int i = 0; i < 10; i++)
            {
                List.Add($"{i}");
                OnPropertyChanged(nameof(List));
                var cancellationTriggered = token.WaitHandle.WaitOne(1000);
            }

            IsBusy = false;
        }
    }
}
