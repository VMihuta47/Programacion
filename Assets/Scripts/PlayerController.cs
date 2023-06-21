using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject bulletPosition;

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si se presiona la tecla de espacio, se dispara una bala
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = PoolObjects.singleton.Get("Bullet");
            //Si el objeto que he recibido se puede usar
            if (b != null)
            {
                //Igualamos la posición de esa bala con la de la nave
                b.transform.position = bulletPosition.transform.position;
                //Y activamos la bala
                b.SetActive(true);
            }
        }
        // Obtener las entradas de movimiento horizontal y vertical
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Normalizar la dirección para asegurarse de que la velocidad sea constante en todas las direcciones
        Vector2 direction = new Vector2(x, y).normalized;
        // Llamar a la función de movimiento
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // Obtener los límites de la cámara
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y - 0.0001f;

        // Obtener la posición actual del jugador
        Vector2 pos = transform.position;

        // Calcular la nueva posición sumando la dirección, velocidad y tiempo
        pos += direction * speed * Time.deltaTime;

        // Limitar la posición dentro de los límites de la cámara
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // Establecer la nueva posición del jugador
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si el jugador choca con un objeto con la etiqueta "Enemy", notifica a los observadores que el jugador ha muerto
            GameManager.instance.NotifyObservers("PlayerDead");
            // Desactiva el jugador
            this.gameObject.SetActive(false);
        }
    }
}
