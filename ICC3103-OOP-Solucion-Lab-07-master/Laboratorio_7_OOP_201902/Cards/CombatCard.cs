using Laboratorio_7_OOP_201902.Enums;
using Laboratorio_7_OOP_201902.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_7_OOP_201902.Cards
{
    [Serializable]
    public class CombatCard : Card, ICharacteristics
    {
        //Atributos
        private int attackPoints;
        private bool hero;

        //Constructor
        public CombatCard(string name, EnumType type, string effect, int attackPoints, bool hero)
        {
            Name = name;
            Type = type;
            Effect = effect;
            AttackPoints = attackPoints;
            Hero = hero;
        }


        public List<string> GetCharacteristics()
        {
            List<string> returninglist = new List<string>() { "Name: " + this.name , "Type: " + Convert.ToString(this.type),
            "Effect: " + this.effect, "Attack Points: " + this.attackPoints, "Hero: " + Convert.ToString(this.hero)};
            return returninglist;
        }


        //Propiedades
        public int AttackPoints
        {
            get
            {
                return this.attackPoints;
            }
            set
            {
                this.attackPoints = value;
            }
        }
        public bool Hero
        { get
            {
                return this.hero;
            }
            set
            {
                this.hero = value;
            }
        }

        
    }
}
