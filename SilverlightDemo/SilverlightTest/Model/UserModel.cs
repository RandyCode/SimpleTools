using System;
using System.ComponentModel;


namespace SilverlightTest.Model
{
    public class UserModel : INotifyPropertyChanged
    {
             
        private string username;
    
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                {
                    throw new Exception("用户名不能为空.");
                }
                username = value;
                OnPropertyChanged("Username");
               
            }
        }

        private string password;

        public string Password

        {
            get { return password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("密码不能为空.");
                }
                password = value;
                OnPropertyChanged("Password");
             
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
