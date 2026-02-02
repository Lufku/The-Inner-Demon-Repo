using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textoDialogo;

    string^[] dialogos = { 
        "You look upset, what happened to you to be like that?"
        };

        int indice = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       textoDialogo.text = dialogos[indice]; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 public void SiguienteDialogo()
     {
      if (indice < dialogos.Length)
      {
        textoDialogo.text = dialogos[indice];
      }
     }
}
