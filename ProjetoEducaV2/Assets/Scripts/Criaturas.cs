using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criaturas : Habilidades
{
    public class Heroi : Criatura, AtaqueDuplo
    {
        public int contMortos;
        public int coin;
        public int ContMortos
        {
            get { return contMortos; }
            protected set
            {
                contMortos = value;
            }
        }
        public int Coin
        {
            get { return coin; }
            protected set
            {
                coin = value;
            }
        }
        public Heroi() : base("Heroi", 100, 25, 0)// nome=Heroi, vidaTotal=100, Ataque=1,Defesa=0
        {
            ContMortos = contMortos;
            Coin = coin;
        }
    }
    public class Slime : Tread
    {
        public Slime() : base(/*nome=*/"Slime", /*vida=*/50, /*dano=*/10, /*defesa=*/0,
                     /*disPersegue=*/15f, /*disAtaque=*/1f, /*velocidade=*/2f, /*DisPatrulha=*/5f, /*Coin=*/10)
        {
        }
    }
    public class Bat : Tread
    {
        public Bat() : base(/*nome=*/"Bat", /*vida=*/60, /*dano=*/15, /*defesa=*/2,
                     /*disPersegue=*/20f, /*disAtaque=*/1f, /*velocidade=*/4f, /*DisPatrulha=*/5f,  /*Coin=*/25)
        {

        }
    }
    public class Goblin : Tread
    {
        public Goblin() : base(/*nome=*/"Goblin", /*vida=*/80, /*dano=*/20, /*defesa=*/5,
                     /*disPersegue=*/10f, /*disAtaque=*/8f, /*velocidade=*/3f, /*DisPatrulha=*/15f,  /*Coin=*/50)
        {

        }
        public class BossGoblin : Tread
        {
            public BossGoblin() : base(/*nome=*/"Goblin King", /*vida=*/250, /*dano=*/1, /*defesa=*/20,
                         /*disPersegue=*/20f, /*disAtaque=*/3f, /*velocidade=*/2f, /*DisPatrulha=*/0f, /*Coin=*/1500)
            {

            }
        }
    }
}
