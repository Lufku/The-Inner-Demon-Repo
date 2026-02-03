using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;

    string[] dialogos = {
        "You look upset, what happened to you to be like that?",
        "Well... It is difficult to talk about that, it happened recently."
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
