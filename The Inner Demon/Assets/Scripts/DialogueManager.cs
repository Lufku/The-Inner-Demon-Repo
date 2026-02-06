using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;

    string[] dialogos = {
        "You look upset, what happened to you to be like that?",
        "Well... It is difficult to talk about that, it happened recently.",
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
<<<<<<< Updated upstream
=======
        else
        {
            SceneManager.LoadScene("PreLevel1");
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
>>>>>>> Stashed changes
    }
}
