using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string pathToPawn = "pack://application:,,,/Res/Images/Pawns/{0}_{1}.png";
        public const string pathToFloor = "pack://application:,,,/Res/Images/Floor/floor_{0}_{1}.png";
        public const string pathToDice = "pack://application:,,,/Res/Images/Dices/dice_{0}.png";

        public const string pathToMagCasting = "pack://application:,,,/Res/Images/MagStrike/mag_{0}.png"; //0 - owner (0 v 1)
        public const string pathToMagExec = "pack://application:,,,/Res/Images/MagStrike/mag_{0}_{1}_{2}.png"; //{0} - owner (0 v 1), {1} - place (1 left, 2 up, 3 right, 4 down), {2} - animation index (0 v 1). owner = 1 has only 0 place
        public const string pathToMagExecRed = "pack://application:,,,/Res/Images/MagStrike/mag_1__{0}.png"; //0 - animation index (0 v 1)




    }
}
