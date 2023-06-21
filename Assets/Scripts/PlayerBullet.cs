using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //Variable que almacena la velocidad de la bala
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtiene la posici�n actual de la bala
        Vector2 position = transform.position;
        // Calcula la nueva posici�n en funci�n de la velocidad y el tiempo
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        // Actualiza la posici�n de la bala
        transform.position = position;
        // Obtiene los l�mites superiores de la c�mara
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // Comprueba si la bala ha salido de la pantalla y la desactiva si es as�
        if (transform.position.y > max.y)
        {
            this.gameObject.SetActive(false);
        }
    }

    //Funcion para detectar colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si la bala choca contra un objeto con el tag Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Notifica a los observadores de que se ha matado a un enemigo
            GameManager.instance.NotifyObservers("EnemyKilled");
            //Si la bala choca contra un enemigo, lo mata y se desactiva la bala y el enemigo para que puedan volver a ser activados
            collision.gameObject.SetActive(false);
            //Se desactiva el prefab
            this.gameObject.SetActive(false);
        }
    }
}
