﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace Monopoly
{
    static class BoardData
    {
        public const byte chanceCards = 5;
        public const byte commChestCards = 10;

        static public string[] fieldName = new string[40];
        static public BitmapImage[] fieldIcon = new BitmapImage[40];
        static public int[] fieldPrice = new int[40];
        static public int[] fieldNoSetRent = new int[40];
        static public int[] field1Rent = new int[40];
        static public int[] field2Rent = new int[40];
        static public int[] field3Rent = new int[40];
        static public int[] field4Rent = new int[40];
        static public int[] fieldHRent = new int[40];
        static public byte[] fieldSet1 = new byte[40];
        static public byte[] fieldSet2 = new byte[40];
        static public bool[] fieldChance = new bool[40];
        static public bool[] fieldCommChest = new bool[40];
        static public bool[] fieldRailroad = new bool[40];
        static public bool[] fieldExtra = new bool[40];
        static public bool[] fieldTax = new bool[40];
        static public int[] fieldTaxCost = new int[40];
        static public string[] chanceText = new string[chanceCards];
        static public byte[] chanceAction = new byte[chanceCards];
        static public int[] chanceXValue = new int[chanceCards];
        static public string[] commChestText = new string[commChestCards];
        static public byte[] commChestAction = new byte[commChestCards];
        static public int[] commChestXValue = new int[commChestCards];

        static public void gameDataWriter()
        {
            FileStream fs = new FileStream(MainWindow.currentThemeDir + @"\BoardContent.mtf", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            for (int i = 0; i < 40; i++)
            {
                switch (i)
                {
                    case 0:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldStart.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 1:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldBronze1.jpg"));
                        fieldPrice[i] = 60;
                        fieldNoSetRent[i] = 2;
                        field1Rent[i] = 10;
                        field2Rent[i] = 30;
                        field3Rent[i] = 90;
                        field4Rent[i] = 160;
                        fieldHRent[i] = 250;
                        fieldSet1[i] = 3;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 2:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldCommChest.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = true;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 3:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldBronze2.jpg"));
                        fieldPrice[i] = 60;
                        fieldNoSetRent[i] = 4;
                        field1Rent[i] = 20;
                        field2Rent[i] = 60;
                        field3Rent[i] = 180;
                        field4Rent[i] = 320;
                        fieldHRent[i] = 450;
                        fieldSet1[i] = 1;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 4:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldTax1.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = true;
                        fieldTaxCost[i] = 200;
                        break;

                    case 5:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRailroad1.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 25;
                        field2Rent[i] = 50;
                        field3Rent[i] = 100;
                        field4Rent[i] = 200;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = true;
                        fieldExtra[i] = false;
                        fieldTax[i] = true;
                        fieldTaxCost[i] = 200;
                        break;

                    case 6:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldLightblue1.jpg"));
                        fieldPrice[i] = 100;
                        fieldNoSetRent[i] = 6;
                        field1Rent[i] = 30;
                        field2Rent[i] = 90;
                        field3Rent[i] = 270;
                        field4Rent[i] = 400;
                        fieldHRent[i] = 550;
                        fieldSet1[i] = 8;
                        fieldSet2[i] = 9;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 7:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldChance.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = true;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 8:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldLightblue2.jpg"));
                        fieldPrice[i] = 100;
                        fieldNoSetRent[i] = 6;
                        field1Rent[i] = 30;
                        field2Rent[i] = 90;
                        field3Rent[i] = 270;
                        field4Rent[i] = 400;
                        fieldHRent[i] = 550;
                        fieldSet1[i] = 6;
                        fieldSet2[i] = 9;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 9:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldLightblue3.jpg"));
                        fieldPrice[i] = 120;
                        fieldNoSetRent[i] = 8;
                        field1Rent[i] = 40;
                        field2Rent[i] = 100;
                        field3Rent[i] = 300;
                        field4Rent[i] = 450;
                        fieldHRent[i] = 600;
                        fieldSet1[i] = 8;
                        fieldSet2[i] = 9;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 10:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldPrison.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 11:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldMagenta1.jpg"));
                        fieldPrice[i] = 140;
                        fieldNoSetRent[i] = 10;
                        field1Rent[i] = 50;
                        field2Rent[i] = 150;
                        field3Rent[i] = 450;
                        field4Rent[i] = 625;
                        fieldHRent[i] = 750;
                        fieldSet1[i] = 13;
                        fieldSet2[i] = 14;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 12:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldElectric.jpg"));
                        fieldPrice[i] = 150;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 29;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 13:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldMagenta2.jpg"));
                        fieldPrice[i] = 140;
                        fieldNoSetRent[i] = 10;
                        field1Rent[i] = 50;
                        field2Rent[i] = 150;
                        field3Rent[i] = 450;
                        field4Rent[i] = 625;
                        fieldHRent[i] = 750;
                        fieldSet1[i] = 11;
                        fieldSet2[i] = 14;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 14:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldMagenta3.jpg"));
                        fieldPrice[i] = 160;
                        fieldNoSetRent[i] = 12;
                        field1Rent[i] = 60;
                        field2Rent[i] = 180;
                        field3Rent[i] = 500;
                        field4Rent[i] = 700;
                        fieldHRent[i] = 900;
                        fieldSet1[i] = 11;
                        fieldSet2[i] = 13;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 15:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRailroad2.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 25;
                        field2Rent[i] = 50;
                        field3Rent[i] = 100;
                        field4Rent[i] = 200;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = true;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 16:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldOrange1.jpg"));
                        fieldPrice[i] = 180;
                        fieldNoSetRent[i] = 14;
                        field1Rent[i] = 70;
                        field2Rent[i] = 200;
                        field3Rent[i] = 550;
                        field4Rent[i] = 750;
                        fieldHRent[i] = 950;
                        fieldSet1[i] = 18;
                        fieldSet2[i] = 19;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 17:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldCommChest.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = true;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 18:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldOrange2.jpg"));
                        fieldPrice[i] = 180;
                        fieldNoSetRent[i] = 14;
                        field1Rent[i] = 70;
                        field2Rent[i] = 200;
                        field3Rent[i] = 550;
                        field4Rent[i] = 750;
                        fieldHRent[i] = 950;
                        fieldSet1[i] = 16;
                        fieldSet2[i] = 19;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 19:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldOrange3.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 16;
                        field1Rent[i] = 80;
                        field2Rent[i] = 220;
                        field3Rent[i] = 600;
                        field4Rent[i] = 800;
                        fieldHRent[i] = 1000;
                        fieldSet1[i] = 16;
                        fieldSet2[i] = 18;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 20:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldParkingLot.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 21:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRed1.jpg"));
                        fieldPrice[i] = 220;
                        fieldNoSetRent[i] = 18;
                        field1Rent[i] = 90;
                        field2Rent[i] = 250;
                        field3Rent[i] = 700;
                        field4Rent[i] = 875;
                        fieldHRent[i] = 1050;
                        fieldSet1[i] = 23;
                        fieldSet2[i] = 24;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 22:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldChance.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = true;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 23:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRed2.jpg"));
                        fieldPrice[i] = 220;
                        fieldNoSetRent[i] = 18;
                        field1Rent[i] = 90;
                        field2Rent[i] = 250;
                        field3Rent[i] = 700;
                        field4Rent[i] = 875;
                        fieldHRent[i] = 1050;
                        fieldSet1[i] = 21;
                        fieldSet2[i] = 24;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 24:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRed3.jpg"));
                        fieldPrice[i] = 240;
                        fieldNoSetRent[i] = 20;
                        field1Rent[i] = 100;
                        field2Rent[i] = 300;
                        field3Rent[i] = 750;
                        field4Rent[i] = 925;
                        fieldHRent[i] = 1100;
                        fieldSet1[i] = 21;
                        fieldSet2[i] = 23;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 25:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRailroad3.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 25;
                        field2Rent[i] = 50;
                        field3Rent[i] = 100;
                        field4Rent[i] = 200;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = true;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 26:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldYellow1.jpg"));
                        fieldPrice[i] = 260;
                        fieldNoSetRent[i] = 22;
                        field1Rent[i] = 110;
                        field2Rent[i] = 330;
                        field3Rent[i] = 800;
                        field4Rent[i] = 975;
                        fieldHRent[i] = 1150;
                        fieldSet1[i] = 27;
                        fieldSet2[i] = 29;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 27:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldYellow2.jpg"));
                        fieldPrice[i] = 260;
                        fieldNoSetRent[i] = 22;
                        field1Rent[i] = 110;
                        field2Rent[i] = 330;
                        field3Rent[i] = 800;
                        field4Rent[i] = 975;
                        fieldHRent[i] = 1150;
                        fieldSet1[i] = 26;
                        fieldSet2[i] = 29;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 28:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldWaterworks.jpg"));
                        fieldPrice[i] = 150;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 12;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 29:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldYellow3.jpg"));
                        fieldPrice[i] = 280;
                        fieldNoSetRent[i] = 24;
                        field1Rent[i] = 120;
                        field2Rent[i] = 360;
                        field3Rent[i] = 850;
                        field4Rent[i] = 1025;
                        fieldHRent[i] = 1200;
                        fieldSet1[i] = 26;
                        fieldSet2[i] = 27;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 30:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldGoToJail.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = true;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 31:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldGreen1.jpg"));
                        fieldPrice[i] = 300;
                        fieldNoSetRent[i] = 26;
                        field1Rent[i] = 130;
                        field2Rent[i] = 390;
                        field3Rent[i] = 900;
                        field4Rent[i] = 1100;
                        fieldHRent[i] = 1275;
                        fieldSet1[i] = 32;
                        fieldSet2[i] = 34;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 32:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldGreen2.jpg"));
                        fieldPrice[i] = 300;
                        fieldNoSetRent[i] = 26;
                        field1Rent[i] = 130;
                        field2Rent[i] = 390;
                        field3Rent[i] = 900;
                        field4Rent[i] = 1100;
                        fieldHRent[i] = 1275;
                        fieldSet1[i] = 31;
                        fieldSet2[i] = 34;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 33:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldCommChest.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = true;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 34:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldGreen3.jpg"));
                        fieldPrice[i] = 320;
                        fieldNoSetRent[i] = 28;
                        field1Rent[i] = 150;
                        field2Rent[i] = 450;
                        field3Rent[i] = 1000;
                        field4Rent[i] = 1200;
                        fieldHRent[i] = 1400;
                        fieldSet1[i] = 31;
                        fieldSet2[i] = 32;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 35:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldRailroad4.jpg"));
                        fieldPrice[i] = 200;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 25;
                        field2Rent[i] = 50;
                        field3Rent[i] = 100;
                        field4Rent[i] = 200;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = true;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 36:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldChance.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = true;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 37:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldBlue1.jpg"));
                        fieldPrice[i] = 350;
                        fieldNoSetRent[i] = 35;
                        field1Rent[i] = 175;
                        field2Rent[i] = 500;
                        field3Rent[i] = 1100;
                        field4Rent[i] = 1300;
                        fieldHRent[i] = 1500;
                        fieldSet1[i] = 39;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;

                    case 38:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldTax2.jpg"));
                        fieldPrice[i] = 0;
                        fieldNoSetRent[i] = 0;
                        field1Rent[i] = 0;
                        field2Rent[i] = 0;
                        field3Rent[i] = 0;
                        field4Rent[i] = 0;
                        fieldHRent[i] = 0;
                        fieldSet1[i] = 0;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = true;
                        fieldTaxCost[i] = 100;
                        break;

                    case 39:
                        fieldName[i] = sr.ReadLine();
                        fieldIcon[i] = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/" + MainWindow.currentThemeDir + @"/FieldBlue2.jpg"));
                        fieldPrice[i] = 400;
                        fieldNoSetRent[i] = 50;
                        field1Rent[i] = 200;
                        field2Rent[i] = 600;
                        field3Rent[i] = 1400;
                        field4Rent[i] = 1700;
                        fieldHRent[i] = 2000;
                        fieldSet1[i] = 37;
                        fieldSet2[i] = 0;
                        fieldChance[i] = false;
                        fieldCommChest[i] = false;
                        fieldRailroad[i] = false;
                        fieldExtra[i] = false;
                        fieldTax[i] = false;
                        fieldTaxCost[i] = 0;
                        break;
                }
            }

            // chanceAction
            // 0 - Player earns x money from bank
            // 1 - Player have to pay x money to bank
            // 2 - Player have to move to the exact field
            // 3 - Player have to move x fields
            // 4 - Player earns x money from other players
            // 5 - Player have to renovate his buildings
            // 6 - Player is arrested 
            // 7 - Player have to pay x or choose other card
            // 8 - Player have a free exit-prison card

            chanceText[0] = "Otrzymałeś kredyt budowlany: +150$";
            chanceAction[0] = 0;
            chanceXValue[0] = 150;

            chanceText[1] = "Mandat za przekroczenie prędkości: -15$";
            chanceAction[1] = 1;
            chanceXValue[1] = 15;

            chanceText[2] = "Zapłać grzywnę: -20$";
            chanceAction[2] = 1;
            chanceXValue[2] = 20;

            chanceText[3] = "Wygrałeś konkurs krzyżówkowy: +100$";
            chanceAction[3] = 0;
            chanceXValue[3] = 100;

            chanceText[4] = "Powrót do szkoły: -150$";
            chanceAction[4] = 1;
            chanceXValue[4] = 150;

            commChestText[0] = "Zapłać za wizytę u lekarza: -50$";
            commChestAction[0] = 1;
            commChestXValue[0] = 50;

            commChestText[1] = "Otrzymujesz zwrot podatku dochodowego: +20$";
            commChestAction[1] = 0;
            commChestXValue[1] = 20;

            commChestText[2] = "Błąd bankowy na twoją korzyść: +200$";
            commChestAction[2] = 0;
            commChestXValue[2] = 200;

            commChestText[3] = "Otrzymujesz odsetki od lokaty terminowej: +25$";
            commChestAction[3] = 0;
            commChestXValue[3] = 25;

            commChestText[4] = "Sprzedałeś obligacje: +100$";
            commChestAction[4] = 0;
            commChestXValue[4] = 100;

            commChestText[5] = "Zapłać rachunek za szpital: -100$";
            commChestAction[5] = 1;
            commChestXValue[5] = 100;

            commChestText[6] = "Otrzymujesz +50$ za sprzedane obligacje";
            commChestAction[6] = 0;
            commChestXValue[6] = 50;

            commChestText[7] = "Odziedziczyłeś w spadku +100$";
            commChestAction[7] = 0;
            commChestXValue[7] = 100;

            commChestText[8] = "Wygrana druga nagroda w konkusie piękności: +10$";
            commChestAction[8] = 0;
            commChestXValue[8] = 10;

            commChestText[9] = "Zapłać składkę ubezpieczeniową: -50$";
            commChestAction[9] = 1;
            commChestXValue[9] = 50;
        }
    }
}
