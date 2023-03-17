using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criaturas: Habilidades
{ 
    public class Heroi: Criatura, AtaqueDuplo
    {
        public int contMortos;
        public int ContMortos
        {
            get { return contMortos; }
            protected set
            {
                contMortos = value;
            }
        }
        public Heroi() : base("Heroi", 100, 1, 0)// nome=Heroi, vidaTotal=100, Ataque=1,Defesa=0
        {
            ContMortos = contMortos;
        }
    }
    public class Slime : Criatura
    {
        public Slime() : base("Slime", 2, 1, 0)
        {
        }
    }
    public class Goblin : Criatura
    {
        public Goblin() : base("Goblin", 100, 1, 0)
        {
        }
    }
}