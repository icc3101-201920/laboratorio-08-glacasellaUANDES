﻿using Laboratorio_7_OOP_201902.Cards;
using Laboratorio_7_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_7_OOP_201902.Static
{
    public static class Visualization
    {
        private const string separador = "============================================";
        // Metodo para mostrar el LoadGameMenu
        public static int LoadGameMenu(bool saved)
        {
            if (!saved)
            {
                return 2;
            }
            Console.Clear();
            ShowProgramMessage("LOAD GAME MENU");
            ShowListOptions(new List<string>() { "Cargar partida", "Nueva partida" });
            int option = GetUserInput(2);
            return option;
        }

        public static void ShowHand(Hand hand)
        {
            CombatCard combatCard;
            Console.WriteLine("Hand: ");
            for (int i = 0; i<hand.Cards.Count; i++)
            {
                if (hand.Cards[i] is CombatCard)
                {
                    combatCard = hand.Cards[i] as CombatCard;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"|({i}) {combatCard.Name} ({combatCard.Type}): {combatCard.AttackPoints} |");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"|({i}) {hand.Cards[i].Name} ({hand.Cards[i].Type}) |");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        // Metodo modificado para que muestre los datos de cada carta
        public static void ShowDecks(List<Deck> decks)
        {
            Console.WriteLine("Select one Deck:");
            for (int i = 0; i < decks.Count; i++)
            {
                Console.WriteLine($"({i}) Deck {i + 1}");
                // Recorremos las cartas del deck
                foreach (Card card in decks[i].Cards)
                {
                    Console.WriteLine(separador);
                    // Si es combatCard
                    if (card.Type == EnumType.melee || card.Type == EnumType.range || card.Type == EnumType.longRange)
                    {
                        // Parseamos, obtenemos las caracteristicas y las imprimimos
                        CombatCard parsed = (CombatCard)card;
                        List<string> characteristics = parsed.GetCharacteristics();
                        foreach (string chara in characteristics)
                        {
                            ShowProgramMessage(chara);
                        }
                    }
                    else
                    {
                        // Parseamos, obtenemos las caracteristicas y las imprimimos
                        SpecialCard parsed = (SpecialCard)card;
                        List<string> characteristics = parsed.GetCharacteristics();
                        foreach (string chara in characteristics)
                        {
                            ShowProgramMessage(chara);
                        }
                    }
                    Console.WriteLine(separador);
                }
            }
        }

        public static void ShowCaptains(List<SpecialCard> captains)
        {
            Console.WriteLine("Select one captain:");
            for (int i = 0; i < captains.Count; i++)
            {
                Console.WriteLine($"({i}) {captains[i].Name}: {captains[i].Effect}");
                // Recorremos las cartas de captains
                foreach (SpecialCard card in captains)
                {
                    // Obtenemos las caracteristicas y las mostramos
                    List<string> characteristics = card.GetCharacteristics();
                    Console.WriteLine(separador);
                    foreach (string chara in characteristics)
                    {
                        ShowProgramMessage(chara);
                    }
                    Console.WriteLine(separador);
                }
            }
        }

        public static int GetUserInput(int maxInput, bool stopper = false)
        {
            bool valid = false;
            int value;
            int minInput = stopper ? -1 : 0;
            while (!valid)
            {

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    if (value >= minInput && value <= maxInput)
                    {
                        return value;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The option ({value}) is not valid, try again");
                        Console.ResetColor();
                    }
                }
                else
                {
                    ConsoleError($"Input must be a number, try again");
                }
            }
            return -1;
        }

        public static void ConsoleError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowProgramMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowListOptions (List<string> options, string message = null)
        {
            if (message != null) Console.WriteLine($"{message}");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"({i}) {options[i]}");
            }
        }

        public static void ClearConsole()
        {
            Console.ResetColor();
            Console.Clear();
        }

        public static void ShowBoard(Board board, int player, int [] lifePoints, int[] attackPoints)
        {
            int theOtherPlayer = player == 0 ? 1 : 0;
            Console.WriteLine("Board:\n");
            Console.WriteLine($"Opponent - LifePoints: {lifePoints[theOtherPlayer]} - AttackPoints: {attackPoints[theOtherPlayer]}:");
            ShowLineBoard(board, EnumType.longRange, theOtherPlayer, board.PlayerCards[theOtherPlayer].ContainsKey(EnumType.bufflongRange));
            ShowLineBoard(board, EnumType.range, theOtherPlayer, board.PlayerCards[theOtherPlayer].ContainsKey(EnumType.buffrange));
            ShowLineBoard(board, EnumType.melee, theOtherPlayer, board.PlayerCards[theOtherPlayer].ContainsKey(EnumType.buffmelee));
            Console.Write($"\nWheater Cards:");
            foreach (Card card in board.WeatherCards)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"|{card.Name}|");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.WriteLine($"You - LifePoints: {lifePoints[player]} - AttackPoints: {attackPoints[player]}:");
            ShowLineBoard(board, EnumType.melee, player, board.PlayerCards[player].ContainsKey(EnumType.buffmelee));
            ShowLineBoard(board, EnumType.range, player, board.PlayerCards[player].ContainsKey(EnumType.buffrange));
            ShowLineBoard(board, EnumType.longRange, player, board.PlayerCards[player].ContainsKey(EnumType.bufflongRange));
            Console.WriteLine("\n");
        }

        public static void ShowLineBoard(Board board, EnumType line, int player, bool buff)
        {
            if (buff)
            {
                Console.Write($"(buffed) ({line.ToString()}) ");
            }
            else
            {
                Console.Write($"({line.ToString()}) "); 
            }
            Console.Write($"[{board.GetAttackPoints(line)[player]}]: ");
            if (board.PlayerCards[player].ContainsKey(line))
            {
                foreach (CombatCard card in board.PlayerCards[player][line])
                {
                    Console.Write($"|{card.AttackPoints}|");
                }
            }
            Console.WriteLine();
            
        }

    }
    
}
