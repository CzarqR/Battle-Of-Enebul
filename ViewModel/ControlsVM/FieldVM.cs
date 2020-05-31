using ProjectB.Model.Board;
using System.Windows.Input;

namespace ProjectB.ViewModel.ControlsVM
{
    public class FieldVM : BaseVM
    {
        #region Properties

        private string backgroundPath;

        public string BackgroundPath
        {
            get
            {
                return backgroundPath;
            }
            set
            {
                backgroundPath = value;
                OnPropertyChanged(nameof(BackgroundPath));
            }
        }

        private string skillCastingPath;

        public string SkillCastingPath
        {
            get
            {
                return skillCastingPath;
            }
            set
            {
                skillCastingPath = value;
                OnPropertyChanged(nameof(SkillCastingPath));
            }
        }

        private string skillExecutingPath;

        public string SkillExecutingPath
        {
            get
            {
                return skillExecutingPath;
            }
            set
            {
                skillExecutingPath = value;
                OnPropertyChanged(nameof(SkillExecutingPath));
            }
        }

        private string pawnImagePath;

        public string PawnImagePath
        {
            get
            {
                return pawnImagePath;
            }
            set
            {
                pawnImagePath = value;
                OnPropertyChanged(nameof(PawnImagePath));
            }
        }

        private string pawnManna;

        public string PawnManna
        {
            get
            {
                return pawnManna;
            }
            set
            {
                pawnManna = value;
                OnPropertyChanged(nameof(PawnManna));
            }
        }

        private string pawnHP;

        public string PawnHP
        {
            get
            {
                return pawnHP;
            }
            set
            {
                pawnHP = value;
                OnPropertyChanged(nameof(PawnHP));
            }
        }

        private FloorStatus floorStatuss;

        public FloorStatus FloorStatus
        {
            get
            {
                return floorStatuss;
            }
            set
            {
                floorStatuss = value;
                OnPropertyChanged(nameof(FloorStatus));
            }
        }

        private string infoToolTip;

        public string InfoToolTip
        {
            get
            {
                return infoToolTip;
            }
            set
            {
                infoToolTip = value;
                OnPropertyChanged(nameof(InfoToolTip));
            }
        }

        public ICommand PawnClick
        {
            get; set;
        }

        #endregion
    }
}
