using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;

    string[] dialogos = {
        "You look upset, what happened to you to be like that?",
        "Well... It is difficult to talk about that, it happened recently.",
        "I know you for a long time, you can tell me what´s the problem.",
        "Sighn... My sister died in a car accident and...",
        "So, you came here cause you want to be with your sister again, right?",
        "Maybe... is that posible?",
        "Yeah.",
        "Well.., we have to go to the cementery were she is burried, do some necromagic rituals and...",
        "No grave desecration, please.",
        "Bagh lame...",
        "I just don´t want a rotting corpse to act as my Sister.",
         "Then you prefere to go to the darkest pit of hell just to save your sister?",
        "...",
        "Ill take the shovel >:3.",
        "Wait... Sighn... Allright, ill go to hell.",
        "Seriusly?",
        "Well... I prefere not to go to a cementery at these moment.",
        "Just tell me... Whats is  the worst thing that can happend to me in that place?",
        "Uhhh... You won´t come back.",
        "But hey, you will be with your sister, soooo... thats something...",
        "That´s breathtaking",
        "Well just dont´t die and everything will be ok. Ill give some stuff just to keep away the reaper.",
        "Alright, Let´s start the ritual.",
        "(F*ck)",


        


    };

    int indice = 0;

    void Start()
    {
        textoDialogo.text = dialogos[indice];
    }

    public void SiguienteDialogo()
    {
        indice++;

        if (indice < dialogos.Length)
        {
            textoDialogo.text = dialogos[indice];
        }
    }
}
