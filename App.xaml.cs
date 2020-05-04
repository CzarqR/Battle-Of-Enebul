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
        public const string pathToCustomImageStart = "pack://application:,,,/Res/Images/Other/start_image.png";
        public const string pathToCustomImageEnd = "pack://application:,,,/Res/Images/Other/end_image.png";
        public const string pathToBigPawn = "pack://application:,,,/Res/Images/BigPawns/{0}_{1}_big.png";
        public const string pathToPawn = "pack://application:,,,/Res/Images/Pawns/{0}_{1}.png";
        public const string pathToFloor = "pack://application:,,,/Res/Images/Floor/floor_{0}_{1}.png";
        public const string pathToDice = "pack://application:,,,/Res/Images/Dices/dice_{0}.png";
        public const string pathToMagCasting = "pack://application:,,,/Res/Images/MagStrike/mag_{0}.png"; //0 - owner (0 v 1)
        public const string pathToMagExec = "pack://application:,,,/Res/Images/MagStrike/mag_{0}_{1}_{2}.png"; //{0} - owner (0 v 1), {1} - place (1 left, 2 up, 3 right, 4 down), {2} - animation index (0 v 1). owner = 1 has only 0 place
        public const string pathToMagExecRed = "pack://application:,,,/Res/Images/MagStrike/mag_1__{0}.png"; //0 - animation index (0 v 1)
        public const string webPageUrl = "https://l.facebook.com/l.php?u=https%3A%2F%2Fdocs.google.com%2Fdocument%2Fd%2F1LaA6LEnUAcHtcc7JQQssgEe_TgOeazlZyRm5bHuJzhY%2Fedit%3Fusp%3Dsharing%26fbclid%3DIwAR0qzaXgCakOvLC4KyGwOe1vxU7Gi7ippLWAafQxVqZqZOJhhm0S2UXuGrA&h=AT3p5oseFj8qky48v8G7X2RhCSRnAsDlbzMHyb10uBZgWHNMI7_R1rA81Ailvkmw8AI4TLflBtJ933B-3oYhUvBl6QBbp3f5FawBTGGLyXFO11ER8h58CDEQkAgf_Df6f5z8vw";

        public App()
        {
            // changing language
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");

        }


    }
}
