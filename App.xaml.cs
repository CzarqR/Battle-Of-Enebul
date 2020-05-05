﻿using System;
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
        public const string pathToCustomImageStart = "pack://application:,,,/Res/Images/Others/start.png";
        public const string cursorPath = "/Res/Cursors/{0}cursor_{1}.cur";
        public const string attack = "attack";
        public const string move = "move";
        public const string defauLt = "default";

        public const string pathToCustomImageEndBlue = "pack://application:,,,/Res/Images/Others/end_game_blue.png";
        public const string pathToCustomImageEndRed = "pack://application:,,,/Res/Images/Others/end_game_red.png";
        public const string pathToBigPawn = "pack://application:,,,/Res/Images/BigPawns/{0}_{1}_big.png";
        public const string pathToPawn = "pack://application:,,,/Res/Images/Pawns/{0}_{1}.png";
        public const string pathToFloor = "pack://application:,,,/Res/Images/Floor/floor_{0}_{1}.png";
        public const string pathToDice = "pack://application:,,,/Res/Images/Dices/dice_{0}.png";
        public const string pathToMagCasting = "pack://application:,,,/Res/Images/MagStrike/mag_{0}.png"; //0 - owner (0 v 1)
        public const string pathToMagExec = "pack://application:,,,/Res/Images/MagStrike/mag_{0}_{1}_{2}.png"; //{0} - owner (0 v 1), {1} - place (1 left, 2 up, 3 right, 4 down), {2} - animation index (0 v 1). owner = 1 has only 0 place
        public const string pathToMagExecRed = "pack://application:,,,/Res/Images/MagStrike/mag_1__{0}.png"; //0 - animation index (0 v 1)
        public const string webPageUrl = "https://docs.google.com/document/d/1LaA6LEnUAcHtcc7JQQssgEe_TgOeazlZyRm5bHuJzhY/edit?fbclid=IwAR10VYlseNwfg61W_CmYvrpXRsVOZQbcYFw9LogFtzU4YVBU3Mhxno3nPLw";




        public App()
        {
            // changing language
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");

        }


    }
}
