﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using NetComm;

namespace Monopoly
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client client;
        Random rng = new Random();
        bool connectedToServer = false;
        DispatcherTimer wait = new DispatcherTimer();
        public static int cheat = 0;
        byte diceScore;
        MediaPlayer sfx = new MediaPlayer();
        string sfxfile = @"Resources\music.wav";
        public class Game
        {
            public bool multiplayer = false;
            public string[] playername = new string[5] { "Gracz 1", "Gracz 2", "Gracz 3", "Gracz 4", "Mr. Nobody" };
            public byte[] playerlocation = new byte[4] { 0, 0, 0, 0 };
            public int[] playercash = new int[4] { 1500, 1500, 1500, 1500 };
            public byte[] playerRailroadOwned = new byte[4] { 0, 0, 0, 0 };
            public byte[] playerArrestedTurns = new byte[4] { 0, 0, 0, 0 };
            public byte clientplayer = 0;
            public byte turn = 0;
            public byte dice1;
            public byte dice2;
            public byte selectedField = 0;
            public int currentFieldPrice = 0;
            public bool currentFieldForSale = false;
            public byte[] fieldHouse = new byte[40];
            public byte[] fieldOwner = new byte[40];
            public byte[] fieldPlayers = new byte[41];
            public int taxmoney = 0;
        }

        BoardLocations boardLocations = new BoardLocations();
        BoardData boardData = new BoardData();
        Game game = new Game();
        public MainWindow()
        {
            for (int i = 0; i < 40; i++)
            {
                game.fieldOwner[i] = 4;
                game.fieldHouse[i] = 0;
                game.fieldPlayers[i] = 0;
            }
            game.fieldPlayers[0] = 4;
            boardData.gameDataWriter();
            wait.Interval = TimeSpan.FromMilliseconds(300);
            wait.Tick += JumpingAnimation_Tick;
            InitializeComponent();
            sfx.Open(new Uri(sfxfile, UriKind.Relative));
            sfx.Volume = 0.5;
            sfx.Play();
        }

        // SERVER CODE
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void client_DataReceived(byte[] Data, string ID)
        {
            ConnectionStatus.Header = ASCIIEncoding.ASCII.GetString(Data);
            sfx.Stop();
        }

        private void btnConnectToServer_Click(object sender, RoutedEventArgs e)
        {
            client = new NetComm.Client();
            client.Connect("localhost", 2020, "Test");
            client.DataReceived += new Client.DataReceivedEventHandler(client_DataReceived);
            connectedToServer = true;
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            client.SendData(ASCIIEncoding.ASCII.GetBytes("You are nice"));
        }

        // GAME CODE
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            game.turn++;
            EndTurn();
        }
        private void EndTurn()
        {
            Button_EndTurn.IsEnabled = false;
            if (game.turn > 3)
            {
                game.turn = 0;
            }
            if (game.turn == 0 && game.playerArrestedTurns[game.turn] == 0)
            {
                EnableMove();
            }
            else if (game.turn == 0 && game.playerArrestedTurns[game.turn] != 0)
            {
                DisableMove();
            }
            if (game.turn == 1 && game.playerArrestedTurns[game.turn] == 0)
            {
                EnableMove();
            }
            else if (game.turn == 1 && game.playerArrestedTurns[game.turn] != 0)
            {
                DisableMove();
            }
            if (game.turn == 2 && game.playerArrestedTurns[game.turn] == 0)
            {
                EnableMove();
            }
            else if (game.turn == 2 && game.playerArrestedTurns[game.turn] != 0)
            {
                DisableMove();
            }
            if (game.turn == 3 && game.playerArrestedTurns[game.turn] == 0)
            {
                EnableMove();
            }
            else if (game.turn == 3 && game.playerArrestedTurns[game.turn] != 0)
            {
                DisableMove();
            }
        }

        private void EnableMove()
        {
            if (game.clientplayer == game.turn)
                Button_ThrowDice.IsEnabled = true;
            else
                ThrowDiceAndMove();
        }

        private void DisableMove()
        {
            game.playerArrestedTurns[game.turn]--;
            if (game.clientplayer == game.turn)
            {
                Button_EndTurn.IsEnabled = true;
                if(game.playerArrestedTurns[game.turn] == 0)
                {
                    game.playerlocation[game.turn] = 10;
                }
            }
            else
            {
                if (game.playerArrestedTurns[game.turn] == 0)
                {
                    game.playerlocation[game.turn] = 10;
                }
                game.turn++;
                EndTurn();
            }
        }

        private void ThrowDiceAndMove()
        {
            game.dice1 = Convert.ToByte(rng.Next(1, 7));
            game.dice2 = Convert.ToByte(rng.Next(1, 7));
            Dice1.Content = game.dice1;
            Dice2.Content = game.dice2;
            diceScore = Convert.ToByte(game.dice1 + game.dice2);
            DiceScore.Content = diceScore;
            wait.Start();
        }

        private void JumpingAnimation_Tick(object sender, EventArgs e)
        {
            if (diceScore > 0)
            {
                game.playerlocation[game.turn]++;
                game.fieldPlayers[game.playerlocation[game.turn] - 1]--;
                game.fieldPlayers[game.playerlocation[game.turn]]++;
                if (game.playerlocation[game.turn] >= 40)
                {
                    game.fieldPlayers[40]--;
                    game.fieldPlayers[0]++;
                    game.playerlocation[game.turn] = 0;
                    game.playercash[game.turn] = game.playercash[game.turn] + 200;
                    GameLog.Text += game.playername[game.turn] + " otrzymuje 200$ za przejście przez start!" + Environment.NewLine;
                    PlayerStatusRefresh();
                }
                Jump();
                diceScore--;
            }
            else
            {
                wait.Stop();
                game.selectedField = game.playerlocation[game.turn];
                OverviewRefresh();
                FieldCheck();
                if (game.turn != 0)
                {
                    game.turn++;
                    if(game.turn > 3)
                    {
                        game.turn = 0;
                    }
                    EndTurn();
                }
                else
                {
                    Button_EndTurn.IsEnabled = true;
                }
            }
        }

        private void FieldCheck()
        {
            byte currentPlayerLocation = game.playerlocation[game.turn];
            int rent = boardData.fieldNoSetRent[currentPlayerLocation];
            if (boardData.fieldChance[currentPlayerLocation] == true)
            {
                byte chanceCard = Convert.ToByte(rng.Next(0, 1));
                if (game.turn == game.clientplayer)
                {
                    MessageBox.Show(boardData.chanceText[chanceCard], "Monopoly", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (!doChanceCard(chanceCard))
                    {
                        MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    else
                    {
                        GameLog.Text += game.playername[game.turn] + " otrzymuje kartę szansy: " + boardData.chanceText[chanceCard] + "!" + Environment.NewLine;
                    }
                }
                else
                {
                    if (!game.multiplayer)
                    {
                        if (doChanceCard(chanceCard))
                        {
                            GameLog.Text += game.playername[game.turn] + " otrzymuje kartę szansy: " + boardData.chanceText[chanceCard] + "!" + Environment.NewLine;
                        }
                        else
                        {
                            MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }

            }
            else if (boardData.fieldCommChest[currentPlayerLocation] == true)
            {
                byte commChestCard = Convert.ToByte(rng.Next(0, 1));
                if (game.turn == game.clientplayer)
                {
                    MessageBox.Show(boardData.commChestText[commChestCard], "Monopoly", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (!doCommChestCard(commChestCard))
                    {
                        MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    else
                    {
                        GameLog.Text += game.playername[game.turn] + " otrzymuje kartę kasy społecznej: " + boardData.commChestText[commChestCard] + "!" + Environment.NewLine;
                    }
                }
                else
                {
                    if (!game.multiplayer)
                    {
                        if (doCommChestCard(commChestCard))
                        {
                            GameLog.Text += game.playername[game.turn] + " otrzymuje kartę kasy społecznej: " + boardData.commChestText[commChestCard] + "!" + Environment.NewLine;
                        }
                        else
                        {
                            MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }
            }
            else if (boardData.fieldRailroad[currentPlayerLocation] == true)
            {
                if (game.fieldOwner[currentPlayerLocation] == 4)
                {
                    if (game.turn == game.clientplayer)
                    {
                        MessageBoxResult result = MessageBox.Show("Czy chcesz kupić " + boardData.fieldName[currentPlayerLocation] + "?", "Monopoly", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                if (!buyField(currentPlayerLocation))
                                {
                                    MessageBox.Show("Nie stać Cię na ten dworzec!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    game.playerRailroadOwned[game.turn]++;
                                    GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                }
                                break;

                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    else
                    {
                        if (!game.multiplayer)
                        {
                            if (buyField(currentPlayerLocation))
                            {
                                game.playerRailroadOwned[game.turn]++;
                                GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                            }
                        }
                    }
                }
                else if (game.fieldOwner[currentPlayerLocation] != game.turn)
                {
                        if (game.playerRailroadOwned[game.fieldOwner[currentPlayerLocation]] == 1)
                        {
                            rent = boardData.field1Rent[currentPlayerLocation];
                        }
                        else if (game.playerRailroadOwned[game.fieldOwner[currentPlayerLocation]] == 2)
                        {
                            rent = boardData.field2Rent[currentPlayerLocation];
                        }
                        else if (game.playerRailroadOwned[game.fieldOwner[currentPlayerLocation]] == 3)
                        {
                            rent = boardData.field3Rent[currentPlayerLocation];
                        }
                        else if (game.playerRailroadOwned[game.fieldOwner[currentPlayerLocation]] >= 4)
                        {
                            rent = boardData.field4Rent[currentPlayerLocation];
                        }
                    if (game.turn == game.clientplayer)
                    {
                        MessageBox.Show("Stanąłeś na dworcu gracza " + game.fieldOwner[currentPlayerLocation] + ". Musisz mu zapłacić: " + rent, "Monopoly", MessageBoxButton.OK, MessageBoxImage.Warning);
                        if (!payRent(currentPlayerLocation, rent))
                        {
                            MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                    else
                    {
                        if (!game.multiplayer)
                        {
                            if (!payRent(currentPlayerLocation, rent))
                            {
                                MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            else if (boardData.fieldTax[currentPlayerLocation] == true)
            {
                if (game.turn == game.clientplayer)
                {
                    MessageBox.Show("Musisz zapłacić podatek w wysokości " + boardData.fieldTaxCost[currentPlayerLocation] + "$.", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    if (!payTax(currentPlayerLocation))
                    {
                        MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    else
                    {
                        GameLog.Text += game.playername[game.turn] + " płaci podatek w wysokości " + boardData.fieldTaxCost[currentPlayerLocation] + "!" + Environment.NewLine;
                    }
                }
                else
                {
                    if (!game.multiplayer)
                    {
                        if (payTax(currentPlayerLocation))
                        {
                            GameLog.Text += game.playername[game.turn] + " płaci podatek w wysokości " + boardData.fieldTaxCost[currentPlayerLocation] + "!" + Environment.NewLine;
                        }
                        else
                        {
                            MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }
            }
            else if (boardData.fieldExtra[currentPlayerLocation] == true)
            {
                switch (currentPlayerLocation)
                {
                    case 10:
                        GameLog.Text += game.playername[game.turn] + " odwiedza więźniów! Jaki miły z niego człowiek!" + Environment.NewLine;
                        break;

                    case 20:
                        game.playercash[game.turn] = game.playercash[game.turn] + game.taxmoney;
                        PlayerStatusRefresh();
                        GameLog.Text += game.playername[game.turn] + " zdobywa " + game.taxmoney + "$!" + Environment.NewLine;
                        if (game.turn == game.clientplayer)
                        {
                            MessageBox.Show("Zdobywasz " + game.taxmoney + "$!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        game.taxmoney = 0;
                        break;

                    case 30: //Go To Jail
                        if (game.turn == game.clientplayer)
                        {
                            MessageBox.Show("Idziesz do więzienia!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        game.playerlocation[game.turn] = 40;
                        game.playerArrestedTurns[game.turn] = 2;
                        Jump();
                        GameLog.Text += game.playername[game.turn] + " zostaje aresztowany!" + Environment.NewLine;
                        break;

                    case 12: //Electric Company
                        if (game.fieldOwner[currentPlayerLocation] == 4)
                        {
                            if (game.turn == game.clientplayer)
                            {
                                MessageBoxResult result = MessageBox.Show("Czy chcesz kupić elektrownię?", "Monopoly", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                switch (result)
                                {
                                    case MessageBoxResult.Yes:
                                        if (!buyField(currentPlayerLocation))
                                        {
                                            MessageBox.Show("Nie stać Cię na elektrownię!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                        else
                                        {
                                            GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                        }
                                        break;

                                    case MessageBoxResult.No:
                                        break;
                                }
                            }
                            else
                            {
                                if (!game.multiplayer)
                                {
                                    if (buyField(currentPlayerLocation))
                                    {
                                        GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else if (game.fieldOwner[currentPlayerLocation] != game.turn)
                        {
                            int calculatedMoney = calculateExtraFieldMultiplier(currentPlayerLocation);
                            if (game.turn == game.clientplayer)
                            {
                                MessageBox.Show("Stanąłeś na elektrowni gracza " + game.fieldOwner[currentPlayerLocation] + ". Musisz mu zapłacić: " + calculatedMoney, "Monopoly", MessageBoxButton.OK, MessageBoxImage.Warning);
                                if (!payExtraFieldMultiplier(calculatedMoney, currentPlayerLocation))
                                {
                                    MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (!game.multiplayer)
                                {
                                    if (!payExtraFieldMultiplier(calculatedMoney, currentPlayerLocation))
                                    {
                                        MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                        this.Close();
                                    }
                                }
                            }
                        }
                        break;

                    case 28: //Waterworks
                        if (game.fieldOwner[currentPlayerLocation] == 4)
                        {
                            if (game.turn == game.clientplayer)
                            {
                                MessageBoxResult result = MessageBox.Show("Czy chcesz kupić wodociągi?", "Monopoly", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                switch (result)
                                {
                                    case MessageBoxResult.Yes:
                                        if (!buyField(currentPlayerLocation))
                                        {
                                            MessageBox.Show("Nie stać Cię na wodociągi!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                        else
                                        {
                                            GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                        }
                                        break;

                                    case MessageBoxResult.No:
                                        break;
                                }
                            }
                            else
                            {
                                if (!game.multiplayer)
                                {
                                    if (buyField(currentPlayerLocation))
                                    {
                                        GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else if (game.fieldOwner[currentPlayerLocation] != game.turn)
                        {
                            int calculatedMoney = calculateExtraFieldMultiplier(currentPlayerLocation);
                            if (game.turn == game.clientplayer)
                            {
                                MessageBox.Show("Stanąłeś na wodociągach gracza " + game.fieldOwner[currentPlayerLocation] + ". Musisz mu zapłacić: " + calculatedMoney, "Monopoly", MessageBoxButton.OK, MessageBoxImage.Warning);
                                if (!payExtraFieldMultiplier(calculatedMoney, currentPlayerLocation))
                                {
                                    MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (!game.multiplayer)
                                {
                                    if (!payExtraFieldMultiplier(calculatedMoney, currentPlayerLocation))
                                    {
                                        MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                        this.Close();
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            else // For normal estates
            {
                if (game.fieldOwner[currentPlayerLocation] == 4)
                {
                    if (game.turn == game.clientplayer)
                    {
                        MessageBoxResult result = MessageBox.Show("Czy chcesz kupić dzielnicę " + boardData.fieldName[currentPlayerLocation] + "?", "Monopoly", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                if (!buyField(currentPlayerLocation))
                                {
                                    MessageBox.Show("Nie stać Cię na tą dzielnicę!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                                }
                                break;

                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    else
                    {
                        if (!game.multiplayer)
                        {
                            if (buyField(currentPlayerLocation))
                            {
                                GameLog.Text += game.playername[game.turn] + " kupuje " + boardData.fieldName[currentPlayerLocation] + "!" + Environment.NewLine;
                            }
                        }
                    }
                }
                else if (game.fieldOwner[currentPlayerLocation] != game.turn)
                {
                    if(game.fieldOwner[currentPlayerLocation] == game.fieldOwner[boardData.fieldSet1[currentPlayerLocation]] && game.fieldOwner[currentPlayerLocation] == game.fieldOwner[boardData.fieldSet2[currentPlayerLocation]] || boardData.fieldSet2[currentPlayerLocation] == 0)
                    {
                        rent = boardData.fieldNoSetRent[currentPlayerLocation] * 2;
                        if(game.fieldHouse[currentPlayerLocation] == 1)
                        {
                            rent = boardData.field1Rent[currentPlayerLocation];
                        }
                        else if(game.fieldHouse[currentPlayerLocation] == 2)
                        {
                            rent = boardData.field2Rent[currentPlayerLocation];
                        }
                        else if(game.fieldHouse[currentPlayerLocation] == 3)
                        {
                            rent = boardData.field3Rent[currentPlayerLocation];
                        }
                        else if(game.fieldHouse[currentPlayerLocation] == 4)
                        {
                            rent = boardData.field4Rent[currentPlayerLocation];
                        }
                        else if(game.fieldHouse[currentPlayerLocation] >= 5)
                        {
                            rent = boardData.fieldHRent[currentPlayerLocation];
                        }
                    }
                    if (game.turn == game.clientplayer)
                    {
                        MessageBox.Show("Stanąłeś na dzielnicy gracz " + game.playername[game.fieldOwner[currentPlayerLocation]] + ". Musisz mu zapłacić: " + rent, "Monopoly", MessageBoxButton.OK, MessageBoxImage.Warning);
                        if (!payRent(currentPlayerLocation, rent))
                        {
                            MessageBox.Show("Bankrutujesz!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                    else
                    {
                        if (!game.multiplayer)
                        {
                            if (!payRent(currentPlayerLocation, rent))
                            {
                                MessageBox.Show("Przeciwnik bankrutuje!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            PlayerStatusRefresh();
        }
        private bool buyField(byte currentPlayerLocation)
        {
            if (game.playercash[game.turn] >= boardData.fieldPrice[currentPlayerLocation])
            {
                game.playercash[game.turn] = game.playercash[game.turn] - boardData.fieldPrice[currentPlayerLocation];
                game.fieldOwner[currentPlayerLocation] = game.turn;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool payRent(byte currentPlayerLocation, int rent)
        {
            if (game.playercash[game.turn] >= rent)
            {
                game.playercash[game.turn] = game.playercash[game.turn] - rent;
                game.playercash[game.fieldOwner[currentPlayerLocation]] = game.playercash[game.fieldOwner[currentPlayerLocation]] + rent;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool payTax(byte currentPlayerLocation)
        {
            if (game.playercash[game.turn] >= boardData.fieldTaxCost[currentPlayerLocation])
            {
                game.playercash[game.turn] = game.playercash[game.turn] - boardData.fieldTaxCost[currentPlayerLocation];
                game.taxmoney = game.taxmoney + boardData.fieldTaxCost[currentPlayerLocation];
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool doChanceCard(byte chanceCard)
        {
            if (boardData.chanceAction[chanceCard] == 0)
            {
                // No function here currently
                return true;
            }
            if (boardData.chanceAction[chanceCard] == 1)
            {
                if (game.playercash[game.turn] >= boardData.chanceXValue[chanceCard])
                {
                    game.playercash[game.turn] = game.playercash[game.turn] - boardData.chanceXValue[chanceCard];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Błąd związany z kartą szansy! Dalsza gra może zawierać błędy! Zrestartuj aplikację i zgłoś błąd twórcy!", "Ups...", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new System.InvalidOperationException("Błąd związany z kartą szansy! Dalsza gra może zawierać błędy! Zrestartuj aplikację i zgłoś błąd twórcy!");
            }
        }

        private bool doCommChestCard(byte commChestCard)
        {
            if (boardData.commChestAction[commChestCard] == 0)
            {
                game.playercash[game.turn] = game.playercash[game.turn] + boardData.commChestXValue[commChestCard];
                return true;
            }
            if (boardData.commChestAction[commChestCard] == 1)
            {
                if (game.playercash[game.turn] >= boardData.commChestXValue[commChestCard])
                {
                    game.playercash[game.turn] = game.playercash[game.turn] - boardData.commChestXValue[commChestCard];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Błąd związany z kartą kasy społecznej! Dalsza gra może zawierać błędy! Zrestartuj aplikację i zgłoś błąd twórcy!", "Ups...", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new System.InvalidOperationException("Błąd związany z kartą kasy społecznej! Dalsza gra może zawierać błędy! Zrestartuj aplikację i zgłoś błąd twórcy!");
            }
        }
        private int calculateExtraFieldMultiplier(byte currentPlayerLocation)
        {
            if (game.fieldOwner[boardData.fieldSet1[currentPlayerLocation]] != game.turn)
            {
                return game.dice1 + game.dice2 * 4;
            }
            else
            {
                return game.dice1 + game.dice2 * 10;
            }
        }

        private bool payExtraFieldMultiplier(int calculatedMoney, byte currentPlayerLocation)
        {
            if (game.playercash[game.turn] >= calculatedMoney)
            {
                game.playercash[game.turn] = game.playercash[game.turn] - calculatedMoney;
                game.playercash[game.fieldOwner[currentPlayerLocation]] = game.playercash[game.fieldOwner[currentPlayerLocation]] + calculatedMoney;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool buyHouse(byte selectedField)
        {
            if (game.fieldOwner[selectedField] == game.fieldOwner[boardData.fieldSet1[selectedField]] && game.fieldOwner[selectedField] == game.fieldOwner[boardData.fieldSet2[selectedField]] || boardData.fieldSet2[selectedField] == 0)
            {
                if(selectedField > 0 && selectedField < 9)
                {
                    if (game.fieldHouse[selectedField] < 6)
                    {
                        if (game.playercash[game.turn] >= 50)
                        {
                            game.playercash[game.turn] = game.playercash[game.turn] - 50;
                            game.fieldHouse[selectedField]++;
                            GameLog.Text += game.playername[game.clientplayer] + "kupuje budynek w dzielnicy " + boardData.fieldName[game.selectedField];
                            return true;
                        }
                        return false;
                    }
                }
                else if(selectedField > 10 && selectedField < 20)
                {
                    if (game.fieldHouse[selectedField] < 6)
                    {
                        if (game.playercash[game.turn] >= 100)
                        {
                            game.playercash[game.turn] = game.playercash[game.turn] - 100;
                            game.fieldHouse[selectedField]++;
                            GameLog.Text += game.playername[game.clientplayer] + "kupuje budynek w dzielnicy " + boardData.fieldName[game.selectedField];
                            return true;
                        }
                        return false;
                    }
                }
                else if(selectedField > 20 && selectedField < 30)
                {
                    if (game.fieldHouse[selectedField] < 6)
                    {
                        if (game.playercash[game.turn] >= 150)
                        {
                            game.playercash[game.turn] = game.playercash[game.turn] - 150;
                            game.fieldHouse[selectedField]++;
                            GameLog.Text += game.playername[game.clientplayer] + "kupuje budynek w dzielnicy " + boardData.fieldName[game.selectedField];
                            return true;
                        }
                        return false;
                    }
                }
                else if(selectedField > 30 && selectedField < 40)
                {
                    if (game.fieldHouse[selectedField] < 6)
                    {
                        if (game.playercash[game.turn] >= 200)
                        {
                            game.playercash[game.turn] = game.playercash[game.turn] - 200;
                            game.fieldHouse[selectedField]++;
                            GameLog.Text += game.playername[game.clientplayer] + "kupuje budynek w dzielnicy " + boardData.fieldName[game.selectedField];
                            return true;
                        }
                        return false;
                    }
                }
                return false;
            }
            return false;
        }

        // UI Programming
        // //////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button_ThrowDice.IsEnabled = false;
            ThrowDiceAndMove();
            // Connection Test
        }

        private void OverviewRefresh()
        {
            Overview_Picture.Source = boardData.fieldIcon[game.selectedField];
            Overview_Name.Content = boardData.fieldName[game.selectedField];
            Overview_Price.Content = boardData.fieldPrice[game.selectedField] + " $";
            Overview_Houses.Content = game.fieldHouse[game.selectedField];
            Overview_Owner.Content = game.playername[game.fieldOwner[game.selectedField]];
            Overview_NoSetRent.Content = boardData.fieldNoSetRent[game.selectedField] + " $";
            Overview_1Rent.Content = boardData.field1Rent[game.selectedField] + " $";
            Overview_2Rent.Content = boardData.field2Rent[game.selectedField] + " $";
            Overview_3Rent.Content = boardData.field3Rent[game.selectedField] + " $";
            Overview_4Rent.Content = boardData.field4Rent[game.selectedField] + " $";
            Overview_HRent.Content = boardData.fieldHRent[game.selectedField] + " $";
        }
        private void PlayerStatusRefresh()
        {
            Label_Player1Cash.Content = game.playercash[0] + "$";
            Label_Player2Cash.Content = game.playercash[1] + "$";
            Label_Player3Cash.Content = game.playercash[2] + "$";
            Label_Player4Cash.Content = game.playercash[3] + "$";
        }
        private void Jump()
        {
            int xcord = 0;
            int ycord = 0;
            if(game.fieldPlayers[game.playerlocation[game.turn]] <= 1)
            {
                xcord = boardLocations.playerlocation(true, game.playerlocation[game.turn]);
                ycord = boardLocations.playerlocation(false, game.playerlocation[game.turn]);
            }
            else if (game.fieldPlayers[game.playerlocation[game.turn]] == 2)
            {
                xcord = boardLocations.playerlocation(true, game.playerlocation[game.turn]) + 22;
                ycord = boardLocations.playerlocation(false, game.playerlocation[game.turn]);
            }
            else if (game.fieldPlayers[game.playerlocation[game.turn]] == 3)
            {
                xcord = boardLocations.playerlocation(true, game.playerlocation[game.turn]);
                ycord = boardLocations.playerlocation(false, game.playerlocation[game.turn]) + 22;
            }
            else if(game.fieldPlayers[game.playerlocation[game.turn]] >= 4)
            {
                xcord = boardLocations.playerlocation(true, game.playerlocation[game.turn]) + 22;
                ycord = boardLocations.playerlocation(false, game.playerlocation[game.turn]) + 22;
            }
            switch (game.turn)
            {
                case 0:
                    Canvas.SetLeft(Player1, xcord);
                    Canvas.SetTop(Player1, ycord);
                break;

                case 1:
                    Canvas.SetLeft(Player2, xcord);
                    Canvas.SetTop(Player2, ycord);
                break;

                case 2:
                    Canvas.SetLeft(Player3, xcord);
                    Canvas.SetTop(Player3, ycord);
                    break;

                case 3:
                    Canvas.SetLeft(Player4, xcord);
                    Canvas.SetTop(Player4, ycord);
                    break;
            }
        }
        private void Field1_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 0;
            OverviewRefresh();
        }

        private void Field2_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 1;
            OverviewRefresh();
        }

        private void Field3_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 2;
            OverviewRefresh();
        }

        private void Field4_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 3;
            OverviewRefresh();
        }

        private void Field5_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 4;
            OverviewRefresh();
        }

        private void Field6_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 5;
            OverviewRefresh();
        }

        private void Field7_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 6;
            OverviewRefresh();
        }

        private void Field8_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 7;
            OverviewRefresh();
        }

        private void Field9_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 8;
            OverviewRefresh();
        }

        private void Field10_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 9;
            OverviewRefresh();
        }

        private void Field11_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 10;
            OverviewRefresh();
        }

        private void Field12_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 11;
            OverviewRefresh();
        }

        private void Field13_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 12;
            OverviewRefresh();
        }

        private void Field14_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 13;
            OverviewRefresh();
        }

        private void Field15_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 14;
            OverviewRefresh();
        }

        private void Field16_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 15;
            OverviewRefresh();
        }

        private void Field17_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 16;
            OverviewRefresh();
        }

        private void Field18_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 17;
            OverviewRefresh();
        }

        private void Field19_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 18;
            OverviewRefresh();
        }

        private void Field20_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 19;
            OverviewRefresh();
        }

        private void Field21_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 20;
            OverviewRefresh();
        }

        private void Field22_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 21;
            OverviewRefresh();
        }

        private void Field23_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 22;
            OverviewRefresh();
        }

        private void Field24_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 23;
            OverviewRefresh();
        }

        private void Field25_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 24;
            OverviewRefresh();
        }

        private void Field26_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 25;
            OverviewRefresh();
        }

        private void Field27_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 26;
            OverviewRefresh();
        }

        private void Field28_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 27;
            OverviewRefresh();
        }

        private void Field29_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 28;
            OverviewRefresh();
        }

        private void Field30_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 29;
            OverviewRefresh();
        }

        private void Field31_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 30;
            OverviewRefresh();
        }

        private void Field32_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 31;
            OverviewRefresh();
        }

        private void Field33_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 32;
            OverviewRefresh();
        }

        private void Field34_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 33;
            OverviewRefresh();
        }

        private void Field35_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 34;
            OverviewRefresh();
        }

        private void Field36_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 35;
            OverviewRefresh();
        }

        private void Field37_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 36;
            OverviewRefresh();
        }

        private void Field38_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 37;
            OverviewRefresh();
        }

        private void Field39_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 38;
            OverviewRefresh();
        }

        private void Field40_MouseEnter(object sender, MouseEventArgs e)
        {
            game.selectedField = 39;
            OverviewRefresh();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheatWindow cheatWindow = new CheatWindow();
            cheatWindow.ShowDialog();
            Button_ThrowDice.IsEnabled = false;
            diceScore = Convert.ToByte(cheat);
            DiceScore.Content = diceScore;
            wait.Start();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Spotify spotify = new Spotify();
            //spotify.Show();
        }

        private void Field1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(game.turn == game.clientplayer)
            {
                buyHouse(1);
            }
        }

        private void Field3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field4_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field7_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field9_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field10_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field12_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field13_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field14_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field15_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field16_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field17_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field18_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field19_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field20_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void Field21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field22_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field23_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field24_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field25_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field26_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field27_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field28_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field29_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field30_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field31_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Na tym polu nie możesz kupować domów!", "Monopoly", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Field32_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field33_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field34_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field35_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field36_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field37_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field38_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field39_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Field40_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
