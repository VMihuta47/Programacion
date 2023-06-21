using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObserver : MonoBehaviour
{
    public void OnNotify(string eventType)
    {
        if (eventType == "EnemyKilled")
        {
            // Si el evento es "EnemyKilled", se llama a la funci�n CheckHighScore en el UIManager
            //UIManager.uinstance.CheckHighScore();
            // Se llama a la funci�n AddPoints en el UIManager para aumentar la puntuaci�n
            UIManager.uinstance.AddPoints();
        }

        if (eventType == "PlayerDead")
        {
            // Si el evento es "PlayerDead", se inserta la puntuaci�n actual obtenida en la tabla de sqlite mediante la funci�n de la otra clase
            HighScore.instance.InsertScore(UIManager.uinstance.score);
            // Se recarga la escena actual para reiniciar el juego una vez el jugador haya muerto
            GameManager.instance.ReloadScene();
        }
    }
}
