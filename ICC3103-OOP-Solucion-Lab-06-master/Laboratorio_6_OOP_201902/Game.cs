using Laboratorio_6_OOP_201902.Cards;
using Laboratorio_6_OOP_201902.Enums;
using Laboratorio_6_OOP_201902.Static;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Laboratorio_6_OOP_201902
{
    public class Game
    {
        //Constantes
        private const int DEFAULT_CHANGE_CARDS_NUMBER = 3;
        private const string DEFAULT_SAVING_PATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\bins\";

        //Atributos
        private Player[] players;
        private Player activePlayer;
        private List<Deck> decks;
        private List<SpecialCard> captains;
        private Board boardGame;
        internal int turn;

        // Metodo para guardar la partida
        private void SaveGame()
        {
            // Serializamos todos los atributos de game en archivos distintos en el default_saving_path
            BinaryFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "players.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.players);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "activeplayer.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.activePlayer);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "decks.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.decks);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "captains.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.captains);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "board.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.boardGame);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "turn.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.turn);
            stream.Close();
        }

        // Metodo para cargar la partida
        private void LoadGame()
        {
            // Deserializamos todos los atributos de game desde el default_saving_path
            BinaryFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "players.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.players = (Player[])formatter.Deserialize(stream);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "activeplayer.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.activePlayer= (Player)formatter.Deserialize(stream);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "decks.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.activePlayer = (List<Deck>)formatter.Deserialize(stream);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "captains.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.activePlayer = (List<SpecialCard>)formatter.Deserialize(stream);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "board.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.boardGame = (Board)formatter.Deserialize(stream);
            stream.Close();

            Stream stream = new FileStream(this.DEFAULT_SAVING_PATH + "turn.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            this.turn = (int)formatter.Deserialize(stream);
            stream.Close();
        }


        // Metodo para verificar si ya hay una partida guardada o no
        private bool Saved()
        {
            if (File.Exists(DEFAULT_SAVING_PATH + "players.bin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }








        //Constructor
        public Game()
        {
            Random random = new Random();
            decks = new List<Deck>();
            captains = new List<SpecialCard>();
            AddDecks();
            AddCaptains();
            players = new Player[2] { new Player(), new Player() };
            ActivePlayer = Players[random.Next(2)];
            boardGame = new Board();
            //Add board to players
            players[0].Board = boardGame;
            players[1].Board = boardGame;
            turn = 0;
        }
        //Propiedades
        public Player[] Players
        {
            get
            {
                return this.players;
            }
        }
        public Player ActivePlayer
        {
            get
            {
                return this.activePlayer;
            }
            set
            {
                activePlayer = value;
            }
        }
        public List<Deck> Decks
        {
            get
            {
                return this.decks;
            }
        }
        public List<SpecialCard> Captains
        {
            get
            {
                return this.captains;
            }
        }
        public Board BoardGame
        {
            get
            {
                return this.boardGame;
            }
        }
  

        //Metodos
        public bool CheckIfEndGame()
        {
            if (players[0].LifePoints == 0 || players[1].LifePoints == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetWinner()
        {
            if (players[0].LifePoints == 0 && players[1].LifePoints > 0)
            {
                return 1;
            }
            else if (players[1].LifePoints == 0 && players[0].LifePoints > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        
        public void Play()
        {
            int userInput = 0;
            int firstOrSecondUser = ActivePlayer.Id == 0 ? 0 : 1;

            // Preguntamos al usuario que quiere hacer, si cargar la partida o iniciar una nueva
            int playerChoose = this.LoadGameMenu();
            // Si retorna 1, quiere cargar el juego, por lo que no se debe realizar el turno 0 de configuracion
            if (playerChose == 1) turn = 1;

            //turno 0 o configuracion
            if (turn == 0)
            {
                for (int _ = 0; _<Players.Length; _++)
                {
                    ActivePlayer = Players[firstOrSecondUser];
                    Visualization.ClearConsole();
                    //Mostrar mensaje de inicio
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id+1} select Deck and Captain:");
                    //Preguntar por deck
                    Visualization.ShowDecks(this.Decks);
                    userInput = Visualization.GetUserInput(this.Decks.Count - 1);
                    Deck deck = new Deck();
                    deck.Cards = new List<Card>(Decks[userInput].Cards);
                    ActivePlayer.Deck = deck;
                    //Preguntar por capitan
                    Visualization.ShowCaptains(Captains);
                    userInput = Visualization.GetUserInput(this.Captains.Count - 1);
                    ActivePlayer.ChooseCaptainCard(new SpecialCard(Captains[userInput].Name, Captains[userInput].Type, Captains[userInput].Effect));
                    //Asignar mano
                    ActivePlayer.FirstHand();
                    //Mostrar mano
                    Visualization.ShowHand(ActivePlayer.Hand);
                    //Mostar opciones, cambiar carta o pasar
                    Visualization.ShowListOptions(new List<string>() { "Change Card", "Pass" }, "Change 3 cards or ready to play:");
                    userInput = Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {
                        Visualization.ClearConsole();
                        Visualization.ShowProgramMessage($"Player {ActivePlayer.Id+1} change cards:");
                        Visualization.ShowHand(ActivePlayer.Hand);
                        for (int i = 0; i < DEFAULT_CHANGE_CARDS_NUMBER; i++)
                        {
                            Visualization.ShowProgramMessage($"Input the numbers of the cards to change (max {DEFAULT_CHANGE_CARDS_NUMBER}). To stop enter -1");
                            userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count, true);
                            if (userInput == -1) break;
                            ActivePlayer.ChangeCard(userInput);
                            Visualization.ShowHand(ActivePlayer.Hand);
                        }
                    }
                    firstOrSecondUser = ActivePlayer.Id == 0 ? 1 : 0;
                }
                turn += 1;
                // Guardamos la partida
                this.SaveGame();
            }
        }

        // Metodo para mostrar el menu al usuario, verificando si hay algo guardado antes o no
        // Si no hay nada guardado, retornamos 2, que corresponde a la opcion nueva partida
        public int LoadGameMenu()
        {
            if (!this.Saved())
            {
                return 2;
            }
            Console.Clear();
            Visualization.ShowProgramMessage("LOAD GAME MENU");
            Visualization.ShowListOptions(new List<string>() { "Cargar partida", "Nueva partida" });
            int option = Visualization.GetUserInput(2);
            return option;
        }


        public void AddDecks()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Decks.txt";
            StreamReader reader = new StreamReader(path);
            int deckCounter = 0;
            List<Card> cards = new List<Card>();


            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string [] cardDetails = line.Split(",");

                if (cardDetails[0] == "END")
                {
                    decks[deckCounter].Cards = new List<Card>(cards);
                    deckCounter += 1;
                }
                else
                {
                    if (cardDetails[0] != "START")
                    {
                        if (cardDetails[0] == nameof(CombatCard))
                        {
                            cards.Add(new CombatCard(cardDetails[1], (EnumType) Enum.Parse(typeof(EnumType),cardDetails[2]), cardDetails[3], Int32.Parse(cardDetails[4]), bool.Parse(cardDetails[5])));
                        }
                        else
                        {
                            cards.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
                        }
                    }
                    else
                    {
                        decks.Add(new Deck());
                        cards = new List<Card>();
                    }
                }

            }
            
        }
        public void AddCaptains()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Captains.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] cardDetails = line.Split(",");
                captains.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
            }
        }
    }
}
