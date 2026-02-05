using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum LadoPersonaje
{
    Izquierda,
    Derecha
}

[System.Serializable]
public class LineaDialogo
{
    [TextArea(2, 4)]
    public string texto;
    public Sprite sprite;
    public LadoPersonaje lado;
}

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoDialogo;
    public Image personajeIzquierda;
    public Image personajeDerecha;

    [Header("Velocidad")]
    public float velocidadLetra = 0.03f; // segundos por letra

    [Header("Diálogos")]
    public LineaDialogo[] dialogos = new LineaDialogo[]
    {
        new LineaDialogo { texto = "It´s everything ok?", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Well... It is difficult to talk about that, it happened recently.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "I know you for a long time, you can tell me what´s the problem.", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Sighn... My sister died in a car accident and...", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "So, you came here cause you want to be with your sister again, right?", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Maybe... is that posible?", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Yeah.", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Well.., we have to go to the cementery were she is burried, do some necromagic rituals and...", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "No grave desecration, please.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Bagh lame...", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "I just don´t want a rotting corpse to act as my Sister.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Then you prefere to go to the darkest pit of hell just to save your sister?", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "...", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Ill take the shovel >:3.", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Wait... Sighn... Allright, ill go to hell.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Seriusly?", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Well... I prefere not to go to a cementery at these moment.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Just tell me... Whats is the worst thing that can happend to me in that place?", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Uhhh... You won´t come back.", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "But hey, you will be with your sister, soooo... thats something...", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "That´s breathtaking", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "Well just dont´t die and everything will be ok. Ill give some stuff just to keep away the reaper.", lado = LadoPersonaje.Izquierda },
        new LineaDialogo { texto = "Alright, Let´s start the ritual.", lado = LadoPersonaje.Derecha },
        new LineaDialogo { texto = "(F*ck)", lado = LadoPersonaje.Derecha }
    };

    private int indice = 0;
    private Coroutine escrituraCoroutine;

    void Start()
    {
        if (dialogos.Length > 0)
            MostrarLinea();
    }

    public void SiguienteDialogo()
    {
        // Si aún se está escribiendo, muestra todo al instante
        if (escrituraCoroutine != null)
        {
            StopCoroutine(escrituraCoroutine);
            textoDialogo.text = dialogos[indice].texto;
            escrituraCoroutine = null;
            return;
        }

        // Avanza al siguiente diálogo
        indice++;

        if (indice < dialogos.Length)
        {
            MostrarLinea();
        }
        else
        {
            Debug.Log("Fin del diálogo");
        }
    }

    void MostrarLinea()
    {
        // Texto letra por letra
        Escritura(dialogos[indice].texto);

        // Cambiar sprite y resaltar
        if (dialogos[indice].sprite != null)
        {
            if (dialogos[indice].lado == LadoPersonaje.Izquierda)
            {
                personajeIzquierda.enabled = true;
                personajeIzquierda.sprite = dialogos[indice].sprite;
                personajeIzquierda.color = Color.white;
                personajeIzquierda.SetAllDirty();

                personajeDerecha.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
            else
            {
                personajeDerecha.enabled = true;
                personajeDerecha.sprite = dialogos[indice].sprite;
                personajeDerecha.color = Color.white;
                personajeDerecha.SetAllDirty();

                personajeIzquierda.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }

    void Escritura(string texto)
    {
        if (escrituraCoroutine != null)
            StopCoroutine(escrituraCoroutine);

        escrituraCoroutine = StartCoroutine(EscribirTexto(texto));
    }

    private IEnumerator EscribirTexto(string texto)
    {
        textoDialogo.text = "";
        foreach (char letra in texto)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadLetra);
        }
        escrituraCoroutine = null;
    }
}

