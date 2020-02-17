using System.ComponentModel;
using SQLite;

namespace ClubEx
{
    class User : INotifyPropertyChanged
    {
        private int _id;[PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _userFirstname;
        [NotNull, MaxLength(10)]
        public string UserFirstname
        {
            get
            {
                return _userFirstname;
            }
            set
            {
                this._userFirstname = value;
                OnPropertyChanged(nameof(UserFirstname));
            }
        }

        private string _userLastname;
        [NotNull, MaxLength(15)]
        public string UserLastname
        {
            get
            {
                return _userLastname;
            }
            set
            {
                this._userLastname = value;
                OnPropertyChanged(nameof(UserLastname));
            }
        }


        public string UserFullName => $"{UserFirstname} {UserLastname}";


        private string _userEmail;
        [NotNull, MaxLength(30)]
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                this._userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }

        private string _userAccountNumber;
        [Unique, NotNull, MaxLength(15)]
        public string UserAccountNumber
        {
            get
            {
                return _userAccountNumber;
            }
            set
            {
                this._userAccountNumber = value;
                OnPropertyChanged(nameof(UserAccountNumber));
            }
        }

        private bool _existingMember;
        public bool ExistingMember
        {
            get
            {
                return _existingMember;
            }
            set
            {
                this._existingMember = value;
                OnPropertyChanged(nameof(ExistingMember));
            }
        }

        private bool _memberSubscription;
        public bool MemberSubscription
        {
            get
            {
                return _memberSubscription;
            }
            set
            {
                this._memberSubscription = value;
                OnPropertyChanged(nameof(MemberSubscription));
            }
        }

        private bool _adminUser;
        public bool AdminUser
        {
            get
            {
                return _adminUser;
            }
            set
            {
                this._adminUser = value;
                OnPropertyChanged(nameof(AdminUser));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
