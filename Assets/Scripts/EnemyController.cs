using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variable est�tica para la velocidad de los enemigos
    public static float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtiene la posici�n actual del enemigo
        Vector2 postion = transform.position;
        // Calcula la nueva posici�n en funci�n de la velocidad y el tiempo
        postion = new Vector2(postion.x, postion.y - enemySpeed * Time.deltaTime);
        transform.position = postion;
        // Obtiene los l�mites inferiores de la c�mara
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // Comprueba si el enemigo est� fuera de la pantalla y lo desactiva si es as�
        if (transform.position.y < min.y)
        {
            this.gameObject.SetActive(false);
        }
    }
}
