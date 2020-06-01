using System.Windows;

namespace ProjectB
{
    public partial class App : Application
    {
        #region paths
        public const string musicPath = "../../Resources/music_back.mp3";
        public const string pathToCustomImageStart = "pack://application:,,,/Res/Images/Others/start.png";
        public const string cursorPath = "/Res/Cursors/{0}cursor_{1}.cur";
        public const string attack = "attack";
        public const string move = "move";
        public const string defauLt = "default";
        public const string idle = "idle";
        public const string red = "red";
        public const string blue = "blue";
        public const string l = "l";
        public const string r = "r";
        public const string pathToOptions = "pack://application:,,,/Res/Images/GemeOptions/options.png";
        public const string pathToOptionsHide = "pack://application:,,,/Res/Images/GemeOptions/options_hide.png";
        public const string pathToMuteDialogs = "pack://application:,,,/Res/Images/GemeOptions/mute_dialogs.png";
        public const string pathToUnmuteDialogs = "pack://application:,,,/Res/Images/GemeOptions/unmute_dialogs.png";
        public const string pathToMuteMusic = "pack://application:,,,/Res/Images/GemeOptions/mute_music.png";
        public const string pathToUnmuteMusic = "pack://application:,,,/Res/Images/GemeOptions/unmute_music.png";
        public const string pathToMaximize = "pack://application:,,,/Res/Images/GemeOptions/fullscreen.png";
        public const string pathToMinimize = "pack://application:,,,/Res/Images/GemeOptions/minimize.png";
        public const string pathToCustomImageEndBlue = "pack://application:,,,/Res/Images/Others/end_game_blue.png";
        public const string pathToCustomImageEndRed = "pack://application:,,,/Res/Images/Others/end_game_red.png";
        public const string pathToBigPawn = "pack://application:,,,/Res/Images/BigPawns/{0}_{1}_big.png";
        public const string pathToPawn = "pack://application:,,,/Res/Images/Pawns/{0}/{1}/{2}/{0}_{1}_{2}_{3}_{4}.png"; // 0 - class, 1 - color, 2 - attack/move/idle, 3 - turn, 4 frame
        public const string pathToFloor = "pack://application:,,,/Res/Images/Floor/floor_{0}_{1}.png";
        public const string pathToDice = "pack://application:,,,/Res/Images/Dices/dice_{0}.png";

        //public const string pathToMagCasting = "pack://application:,,,/Res/Images/MagStrike/mag_{0}.png"; //0 - owner (0 v 1)
        //public const string pathToMagExec = "pack://application:,,,/Res/Images/MagStrike/mag_{0}_{1}_{2}.png"; //{0} - owner (0 v 1), {1} - place (1 left, 2 up, 3 right, 4 down), {2} - animation index (0 v 1). owner = 1 has only 0 place
        //public const string pathToMagExecRed = "pack://application:,,,/Res/Images/MagStrike/mag_1__{0}.png"; //0 - animation index (0 v 1)


        public const string pathToMagExecute = "pack://application:,,,/Res/Images/MagStrike/{0}/mag_{0}_execute_{1}_{2}.png"; // 0 - color, 1 - position, 2 - frame
        public const string center = "center";
        public const string up = "up";
        public const string right = "right";
        public const string down = "down";
        public const string left = "left";
        public const string side = "side";

        public const string pathToMagMarking = "pack://application:,,,/Res/Images/MagStrike/{0}/mag_mark_{0}_{1}.png"; // 0 - color, 1 - frame


        public const string gameInfoWeb = "https://docs.google.com/document/d/1LaA6LEnUAcHtcc7JQQssgEe_TgOeazlZyRm5bHuJzhY/edit?fbclid=IwAR10VYlseNwfg61W_CmYvrpXRsVOZQbcYFw9LogFtzU4YVBU3Mhxno3nPLw";

        public const string comapnyWeb = "http://myniprojects.simplesite.com/446267881";


        #endregion

        public App()
        {
            // changing language
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");

        }
    }
}
