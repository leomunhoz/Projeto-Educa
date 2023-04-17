using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criaturas : Habilidades
{
    public class Heroi : Criatura, AtaqueDuplo
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
    public class Slime : Tread
    {
        public Slime() : base(/*nome=*/"Slime", /*vida=*/2, /*dano=*/1, /*defesa=*/0,
                     /*disPersegue=*/5f, /*disAtaque=*/1f, /*velocidade=*/2f, /*DisPatrulha=*/10f)
        {
        }
    }
    public class Goblin : Tread
    {
        public Goblin() : base(/*nome=*/"Goblin", /*vida=*/100, /*dano=*/1, /*defesa=*/0,
                     /*disPersegue=*/5f, /*disAtaque=*/3f, /*velocidade=*/2f, /*DisPatrulha=*/5f)
        {

        }
    }
}
