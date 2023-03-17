using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class Criatura
{
    public int vidaTotal;
    public int poder;
    public int vida;
    public string Nome { get; private set; }
    public int VidaTotal
    {
        get { return vidaTotal; }
        protected set
        {
            if (vidaTotal < 0)
                vidaTotal = 0;
            else
                vidaTotal = value;
        }
    }
    public int Dano { get; private set; }
    public int Defesa { get; private set; }
    public int Poder
    {
        get { return poder; }
        protected set
        {
            poder = (10 * Defesa) + VidaTotal;
        }
    }

    public Criatura(string nome, int vidaTotal, int dano, int defesa)
    {
        Nome = nome;
        VidaTotal = vidaTotal;
        Dano = dano;
        Defesa = defesa;
        vida = VidaTotal;
    }
    public void Ataque(Criatura criatura)
    {
        int bonus;
        Criatura atual = this;
        if (atual.vida > 0)
        {

            Debug.Log("Vida atual: " + criatura.Nome + "  " + criatura.vida);
            Debug.Log(atual.Nome + " atacou a " + criatura.Nome);
            bonus = Bonus(atual, criatura);
            criatura.vida -= bonus;
            if (criatura.vida <= 0)
                Debug.Log(criatura.Nome + " Desmaiou");
            else
            {
                Debug.Log(atual.Nome + " causou " + bonus + " de dano!");
                Debug.Log(criatura.Nome + " está com " + criatura.vida + " vida!");
            }
        }
        else
            Debug.Log(atual.Nome + " não pode atacar esta fora de combate!");
    }
    public int Bonus(Criatura ataque, Criatura defesa)
    {
        int bonus = ataque.Dano;
        if (ataque is Habilidades.AtaqueDuplo)
        {
            Debug.Log("Ataque duplo!! ");
        }
        return Mathf.Max((ataque.Dano + bonus) - defesa.Defesa, 0);
    }

}
