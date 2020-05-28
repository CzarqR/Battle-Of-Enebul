using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using ProjectB.ViewModel.Commands;
using ProjectB.ViewModel.ControlsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;

namespace ProjectB.ViewModel.WindowsVM
{
    using R = Properties.Resources;

    public class GameVM : BaseVM
    {

        #region Properties

        private readonly SoundPlayer musicBackground;


        private readonly GameState GameState;

        private readonly Random random = new Random();


        private ObservableCollection<FieldVM> fieldsVM;

        public ObservableCollection<FieldVM> FieldsVM
        {
            get
            {
                return fieldsVM;
            }
            set
            {
                fieldsVM = value;
                OnPropertyChanged(nameof(FieldsVM));
            }
        }

        public bool PrimaryAttackEnable
        {
            get; private set;
        }

        public bool SkillAttackEnable
        {
            get; private set;
        }

        public bool DiceRollEnable
        {
            get; private set;
        }

        private string dicePath;

        public string DicePath
        {
            get
            {
                return dicePath;
            }
            set
            {
                dicePath = value;
                OnPropertyChanged(nameof(DicePath));
            }
        }

        private string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
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

        private string descPawn;

        public string DescPawn
        {
            get
            {
                return descPawn;
            }
            set
            {
                descPawn = value;
                OnPropertyChanged(nameof(DescPawn));
            }
        }

        private string stats;

        public string Stats
        {
            get
            {
                return stats;
            }
            set
            {
                stats = value;
                OnPropertyChanged(nameof(Stats));
            }
        }

        private string primaryAttackName;

        public string PrimaryAttackName
        {
            get
            {
                return primaryAttackName;
            }
            set
            {
                primaryAttackName = value;
                OnPropertyChanged(nameof(PrimaryAttackName));
            }
        }

        private string primaryAttackDesc;

        public string PrimaryAttackDesc
        {
            get
            {
                return primaryAttackDesc;
            }
            set
            {
                primaryAttackDesc = value;
                OnPropertyChanged(nameof(PrimaryAttackDesc));
            }
        }

        private string skillAttackName;

        public string SkillAttackName
        {
            get
            {
                return skillAttackName;
            }
            set
            {
                Console.WriteLine("XX");
                Console.WriteLine(value);
                skillAttackName = value;
                OnPropertyChanged(nameof(SkillAttackName));
            }
        }

        private string skillAttackDesc;

        public string SkillAttackDesc
        {
            get
            {
                return skillAttackDesc;
            }
            set
            {
                skillAttackDesc = value;
                OnPropertyChanged(nameof(SkillAttackDesc));
            }
        }

        private string floorImagePath;

        public string FloorImagePath
        {
            get
            {
                return floorImagePath;
            }
            set
            {
                floorImagePath = value;
                OnPropertyChanged(nameof(FloorImagePath));
            }
        }

        private string floorDesc;

        public string FloorDesc
        {
            get
            {
                return floorDesc;
            }
            set
            {
                floorDesc = value;
                OnPropertyChanged(nameof(FloorDesc));
            }
        }

        private string floorLegend;

        public string FloorLegend
        {
            get
            {
                return floorLegend;
            }
            set
            {
                floorLegend = value;
                OnPropertyChanged(nameof(FloorLegend));
            }
        }

        private Visibility pawnPanelVisibility;

        public Visibility PawnPanelVisibility
        {
            get
            {
                return pawnPanelVisibility;
            }
            set
            {
                pawnPanelVisibility = value;
                OnPropertyChanged(nameof(PawnPanelVisibility));
            }
        }

        private Visibility floorPanelVisibility;

        public Visibility FloorPanelVisibility
        {
            get
            {
                return floorPanelVisibility;
            }
            set
            {
                floorPanelVisibility = value;
                OnPropertyChanged(nameof(FloorPanelVisibility));
            }
        }

        private Visibility customPanelVisibility;

        public Visibility CustomPanelVisibility
        {
            get
            {
                return customPanelVisibility;
            }
            set
            {
                customPanelVisibility = value;
                OnPropertyChanged(nameof(CustomPanelVisibility));
            }
        }

        private Visibility bottomButtonsVisibility;

        public Visibility BottomButtonsVisibility
        {
            get
            {
                return bottomButtonsVisibility;
            }
            set
            {
                bottomButtonsVisibility = value;
                OnPropertyChanged(nameof(BottomButtonsVisibility));
            }
        }

        private Visibility endGameButtonVisibility;

        public Visibility EndGameButtonVisibility
        {
            get
            {
                return endGameButtonVisibility;
            }
            set
            {
                endGameButtonVisibility = value;
                OnPropertyChanged(nameof(EndGameButtonVisibility));
            }
        }

        private Visibility startBottomTitleVisibility;

        public Visibility StartBottomTitleVisibility
        {
            get
            {
                return startBottomTitleVisibility;
            }
            set
            {
                startBottomTitleVisibility = value;
                OnPropertyChanged(nameof(StartBottomTitleVisibility));
            }
        }

        private string customLegend;

        public string CustomLegend
        {
            get
            {
                return customLegend;
            }
            set
            {
                customLegend = value;
                OnPropertyChanged(nameof(CustomLegend));
            }
        }

        private string customImagePath;

        public string CustomImagePath
        {
            get
            {
                return customImagePath;
            }
            set
            {
                customImagePath = value;
                OnPropertyChanged(nameof(CustomImagePath));
            }
        }

        private string customBottomTitle;

        public string CustomBottomTitle
        {
            get
            {
                return customBottomTitle;
            }
            set
            {
                customBottomTitle = value;
                OnPropertyChanged(nameof(CustomBottomTitle));
            }
        }

        private Cursor cursor;

        public Cursor Cursor
        {
            get
            {
                return cursor;
            }
            set
            {
                cursor = value;
                OnPropertyChanged(nameof(Cursor));
            }
        }

        private string muteDialogIcon;

        public string MuteDialogIcon
        {
            get
            {
                return muteDialogIcon;
            }
            set
            {
                muteDialogIcon = value;
                OnPropertyChanged(nameof(MuteDialogIcon));
            }
        }

        private string muteMusicIcon;
        public string MuteMusicIcon
        {
            get
            {
                return muteMusicIcon;
            }
            set
            {
                muteMusicIcon = value;
                OnPropertyChanged(nameof(MuteMusicIcon));
            }
        }

        private string muteMusicToolTip;

        public string MuteMusicToolTip
        {
            get
            {
                return muteMusicToolTip;
            }
            set
            {
                muteMusicToolTip = value;
                OnPropertyChanged(nameof(MuteMusicToolTip));
            }
        }

        private string muteDialogToolTip;
        public string MuteDialogToolTip
        {
            get
            {
                return muteDialogToolTip;
            }
            set
            {
                muteDialogToolTip = value;
                OnPropertyChanged(nameof(MuteDialogToolTip));
            }
        }


        #endregion


        #region Commands

        private ICommand butAttackCLickCommand;
        public ICommand ButAttackClickCommand
        {
            get
            {
                return butAttackCLickCommand ?? (butAttackCLickCommand = new CommandHandlerExecParameter(AttackClick, GetAttack));
            }
        }

        private bool GetAttack(bool attackType)
        {
            if (attackType)
            {
                return PrimaryAttackEnable;
            }
            else
            {
                return SkillAttackEnable;
            }
        }

        private void AttackClick(bool attackType)
        {
            GameState.MarkFieldsToAttack(attackType);
        }


        private ICommand mouseEnterAttackCommand;
        public ICommand MouseEnterAttackCommand
        {
            get
            {
                return mouseEnterAttackCommand ?? (mouseEnterAttackCommand = new CommandHandlerParameter(EnterAttack, () => { return true; }));
            }
        }

        private void EnterAttack(bool attackType)
        {
            GameState.ShowPossibleAttack(attackType);
        }


        private ICommand mouseLeaveAttackCommand;
        public ICommand MouseLeaveAttackCommand
        {
            get
            {
                return mouseLeaveAttackCommand ?? (mouseLeaveAttackCommand = new CommandHandler(LeaveAttack, () => { return true; }));
            }
        }

        private void LeaveAttack()
        {
            GameState.HidePossibleAttack();
        }


        private ICommand mouseInterractDiceCommand;

        public ICommand MouseInterractDiceCommand
        {
            get
            {
                return mouseInterractDiceCommand ?? (mouseInterractDiceCommand = new CommandHandlerParameter(SetDiceImage, () => { return DiceRollEnable; }));
            }

        }

        private void SetDiceImage(bool inOff)
        {
            if (inOff) //enter dice image
            {
                DicePath = string.Format(App.pathToDice, "00");
            }
            else
            {
                DicePath = string.Format(App.pathToDice, "0");

            }
        }



        private ICommand diceRollCommand;

        public ICommand DiceRollCommand
        {
            get
            {
                return diceRollCommand ?? (diceRollCommand = new CommandHandler(RollDice, () => { return DiceRollEnable; }));
            }

        }

        private void RollDice()
        {
            DiceRollEnable = false;
            int bonus = random.Next(1, 7);
            DicePath = string.Format(App.pathToDice, bonus);
            GameState.RollDice(Convert.ToByte(bonus));
            PlaySound("../../Res/Sounds/dices.wav");

        }


        private ICommand endRoundCommand;

        public ICommand EndRoundCommand
        {
            get
            {
                return endRoundCommand ?? (endRoundCommand = new CommandHandler(EndRound, () => { return true; }));
            }
        }

        private void EndRound()
        {
            GameState.EndRound();
            DicePath = string.Format(App.pathToDice, 0);
            DiceRollEnable = false;
            PrimaryAttackEnable = false;
            SkillAttackEnable = false;
        }


        private ICommand skipMovementCommand;

        public ICommand SkipMovementCommand
        {
            get
            {
                return skipMovementCommand ?? (skipMovementCommand = new CommandHandler(GameState.SkipMovement, () => { return true; }));
            }
        }


        private ICommand windowClosed;

        public ICommand WindowClosed
        {
            get
            {
                return windowClosed ?? (windowClosed = new CommandHandler(() => { musicBackground?.Stop(); }, () => { return true; }));
            }
        }


        private ICommand muteDialogsCommand;

        public ICommand MuteDialogsCommand
        {
            get
            {
                return muteDialogsCommand ?? (muteDialogsCommand = new CommandHandler(MuteDialogs, () => { return true; }));
            }
        }

        private void MuteDialogs()
        {
            if (MuteDialogIcon == App.pathToUnmuteDialogs)
            {
                Console.WriteLine("Dialogs Muted");
                MuteDialogIcon = App.pathToMuteDialogs;
                MuteDialogToolTip = R.unmute_dialogs;
            }
            else
            {
                Console.WriteLine("Dialogs Unmuted");
                MuteDialogIcon = App.pathToUnmuteDialogs;
                MuteDialogToolTip = R.mute_dialogs;
            }
        }

        private ICommand muteMusicCommand;

        public ICommand MuteMusicCommand
        {
            get
            {
                return muteMusicCommand ?? (muteMusicCommand = new CommandHandler(MuteMusic, () => { return true; }));
            }
        }

        private void MuteMusic()
        {
            if (MuteMusicIcon == App.pathToMuteMusic)
            {
                Console.WriteLine("Music Muted");
                MuteMusicIcon = App.pathToUnmuteMusic;
                musicBackground.PlayLooping();
                MuteMusicToolTip = R.unmute_music;
            }
            else
            {
                Console.WriteLine("Music Unmuted");
                MuteMusicIcon = App.pathToMuteMusic;
                musicBackground.Stop();
                MuteMusicToolTip = R.mute_music;
            }
        }

        #endregion


        #region event bindings


        private void UpdateField(string[] fieldValues, int index, FloorStatus floorStatus)
        {
            FieldsVM.ElementAt(index).BackgroundPath = fieldValues[0];
            FieldsVM.ElementAt(index).SkillCastingPath = fieldValues[1];
            FieldsVM.ElementAt(index).SkillExecutingPath = fieldValues[2];
            FieldsVM.ElementAt(index).PawnImagePath = fieldValues[3];
            FieldsVM.ElementAt(index).PawnHP = fieldValues[4];
            FieldsVM.ElementAt(index).PawnManna = fieldValues[5];
            FieldsVM.ElementAt(index).InfoToolTip = fieldValues[6];
            FieldsVM.ElementAt(index).FloorStatus = floorStatus;
        }

        private void AttactEnable(bool primaryAttack, bool skillAttack)
        {
            PrimaryAttackEnable = primaryAttack;
            SkillAttackEnable = skillAttack;
        }

        private void StartAttack()
        {
            DiceRollEnable = true;
        }

        private void UpdatePanelPawn(string title, string pawnImagePath, string descPawn, string stats, string primaryAttackName, string primaryAttackDesc, string skillAttackName, string skillAttackDesc)
        {
            Title = title;
            PawnImagePath = pawnImagePath;
            DescPawn = descPawn;
            Stats = stats;
            PrimaryAttackDesc = primaryAttackDesc;
            PrimaryAttackName = primaryAttackName;
            SkillAttackDesc = skillAttackDesc;
            SkillAttackName = skillAttackName;
            PawnPanelVisibility = Visibility.Visible;
            FloorPanelVisibility = Visibility.Collapsed;
            CustomPanelVisibility = Visibility.Collapsed;
            BottomButtonsVisibility = Visibility.Visible;
            StartBottomTitleVisibility = Visibility.Collapsed;
        }

        private void UpdatePanelFloor(string title, string floorImagePath, string floorDesc, string legend)
        {
            Title = title;
            FloorImagePath = floorImagePath;
            FloorLegend = legend;
            FloorDesc = floorDesc;
            PawnPanelVisibility = Visibility.Collapsed;
            FloorPanelVisibility = Visibility.Visible;
            BottomButtonsVisibility = Visibility.Visible;
            CustomPanelVisibility = Visibility.Collapsed;
            StartBottomTitleVisibility = Visibility.Collapsed;

        }

        private void UpdatePanelCustom(string title, string imgPath, string legend, string bottomTitle)
        {
            CustomImagePath = imgPath;
            CustomLegend = legend;
            Title = title;
            CustomBottomTitle = bottomTitle;
            PawnPanelVisibility = Visibility.Collapsed;
            FloorPanelVisibility = Visibility.Collapsed;
            BottomButtonsVisibility = Visibility.Collapsed;
            CustomPanelVisibility = Visibility.Visible;
            if (bottomTitle.Equals(R.end_game_bottom_title)) //end game
            {
                EndGameButtonVisibility = Visibility.Visible;
            }
            else
            {
                EndGameButtonVisibility = Visibility.Collapsed;
            }
        }

        private void CursorUpdate(string cursorPath)
        {
            StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri(cursorPath, UriKind.Relative));
            Cursor = new Cursor(sriCurs.Stream);
        }

        readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private void PlaySound(string fileName)
        {

            if (MuteDialogIcon == App.pathToUnmuteDialogs)
            {
                mediaPlayer.Open(new Uri(fileName, UriKind.Relative));
                mediaPlayer.Play();
            }
        }


        #endregion


        /// <summary>
        /// ctor
        /// </summary>
        public GameVM()
        {
            GameState = new GameState();

            /// Init fields
            FieldsVM = new ObservableCollection<FieldVM>();
            for (int i = 0; i < Arena.HEIGHT; i++)
            {
                for (int j = 0; j < Arena.WIDTH; j++)
                {
                    Cord cord = new Cord(i, j);
                    var x = GameState.GetFieldView(i, j);

                    FieldsVM.Add(new FieldVM
                    {
                        BackgroundPath = x[0],
                        SkillCastingPath = x[1],
                        SkillExecutingPath = x[2],
                        PawnImagePath = x[3],
                        PawnHP = x[4],
                        PawnManna = x[5],
                        InfoToolTip = x[6],
                        PawnClick = new CommandHandler(() => { GameState.HandleInput(cord); }, () => { return true; })

                    });
                }
            }

            /// seting startup values
            PrimaryAttackEnable = false;
            SkillAttackEnable = false;
            DiceRollEnable = false;
            DicePath = string.Format(App.pathToDice, 0);
            MuteDialogIcon = App.pathToUnmuteDialogs;
            MuteMusicIcon = App.pathToUnmuteMusic;
            MuteDialogToolTip = R.mute_dialogs;
            MuteMusicToolTip = R.mute_music;

            GameState.UpdateUIEvent += UpdateField;
            GameState.StartAttackEvent += AttactEnable;
            GameState.FieldToAttackSelectedEvent += StartAttack;
            GameState.ShowPawnInfoEvent += UpdatePanelPawn;
            GameState.ShowFloorInfoEvent += UpdatePanelFloor;
            GameState.ShowCustomPanelEvent += UpdatePanelCustom;
            GameState.CursosUpdateEvent += CursorUpdate;
            GameState.StartGame();

            musicBackground = new SoundPlayer(R.music_back);
            musicBackground.Play();
        }

    }
}
