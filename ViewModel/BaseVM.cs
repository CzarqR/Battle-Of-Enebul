using System.ComponentModel;

namespace ProjectB.ViewModel
{

    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] propNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string prop in propNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
    }
}
