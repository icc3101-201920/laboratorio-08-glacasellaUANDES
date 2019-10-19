using Laboratorio_7_OOP_201902.Cards;
using Laboratorio_7_OOP_201902.Enums;
using Laboratorio_7_OOP_201902.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorio_7_OOP_201902
{
    [Serializable]
    public class Deck : ICharacteristics
    {

        private List<Card> cards;

        public Deck()
        {

        }

        // Metodo que se pide en el enunciado usando linq
        public List<string> GetCharacteristics()
        {
            List<string> returningList = new List<string>();

            // Obtenemos la cantidad de cartas en cards. No tiene sentido hacer esto con linq, 
            // pero asi lo dice el enunciado ¯\_(ツ)_/¯
            IEnumerable<Card> cardsQuery =
                from card in this.cards
                select card;
            // Ahora vemos el tamano de cardsQuery jeje
            returningList.Add("Hay " + Convert.ToString(cardsQuery.Count()) + "cartas");


            // Obtenemos la cantidad de cartas melee en cards
            IEnumerable<Card> meleeCardsQuery =
                from card in this.cards
                where card.Type == EnumType.melee
                select card;
            // Ahora vemos el tamano de meleeCardsQuery
            returningList.Add("Hay " + Convert.ToString(meleeCardsQuery.Count()) + "cartas melee");

            // Obtenemos la cantidad de cartas range en cards
            IEnumerable<Card> rangeCardsQuery =
                from card in this.cards
                where card.Type == EnumType.range
                select card;
            // Ahora vemos el tamano de rangeCardsQuery
            returningList.Add("Hay " + Convert.ToString(rangeCardsQuery.Count()) + "cartas range");

            // Obtenemos la cantidad de cartas longRange en cards
            IEnumerable<Card> longrangeCardsQuery =
                from card in this.cards
                where card.Type == EnumType.longRange
                select card;
            // Ahora vemos el tamano de longrangeCardsQuery
            returningList.Add("Hay " + Convert.ToString(longrangeCardsQuery.Count()) + "cartas long range");


            // Obtenemos la cantidad de cartas buff en cards
            IEnumerable<Card> buffCardsQuery =
                from card in this.cards
                where card.Type == EnumType.buff || card.Type == EnumType.buffmelee || card.Type == EnumType.buffrange || card.Type == EnumType.bufflongRange
                select card;
            // Ahora vemos el tamano de buffCardsQuery
            returningList.Add("Hay " + Convert.ToString(buffCardsQuery.Count()) + "cartas buff");

            // Obtenemos la cantidad de cartas weather en cards
            IEnumerable<Card> weatherCardsQuery =
                from card in this.cards
                where card.Type == EnumType.weather
                select card;
            // Ahora vemos el tamano de weatherCardsQuery
            returningList.Add("Hay " + Convert.ToString(weatherCardsQuery.Count()) + "cartas weather");

            // Obtenemos el total de puntos de ataque de las cartas melee
            IEnumerable<CombatCard> totalcardsmelee =
                 (from card in this.cards
                  where card.Type == EnumType.melee
                  select card as CombatCard);
            int totalmelee = totalcardsmelee.Sum(x => x.AttackPoints);
            returningList.Add("El total de puntos de ataque de las cartas melee es " + Convert.ToString(totalmelee));


            // Obtenemos el total de puntos de ataque de las cartas range
            IEnumerable<CombatCard> totalcardsrange =
                 (from card in this.cards
                  where card.Type == EnumType.range
                  select card as CombatCard);
            int totalrange = totalcardsrange.Sum(x => x.AttackPoints);
            returningList.Add("El total de puntos de ataque de las cartas range es " + Convert.ToString(totalrange));


            // Obtenemos el total de puntos de ataque de las cartas long range
            IEnumerable<CombatCard> totalcardslongrange =
                 (from card in this.cards
                  where card.Type == EnumType.longRange
                  select card as CombatCard);
            int totallongrange = totalcardslongrange.Sum(x => x.AttackPoints);
            returningList.Add("El total de puntos de ataque de las cartas longRange es " + Convert.ToString(totallongrange));

            returningList.Add("El total de puntos de ataque del mazo es " + Convert.ToString(totallongrange + totalrange + totalmelee));

            return returningList;
        }



        public List<Card> Cards { get => cards; set => cards = value; }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void DestroyCard(int cardId)
        {
            cards.RemoveAt(cardId);
        }

        public void Shuffle()
        {
            Random random = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

    }
}
