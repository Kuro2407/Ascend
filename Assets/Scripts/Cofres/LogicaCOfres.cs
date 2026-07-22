using System.Collections;
using TMPro;
using UnityEngine;

public class LogicaCOfres : MonoBehaviour
{
    private Transform player;
    private bool sePuedeAbrir = false;
    private Animator anim;
    private float distanciaHastaPlayer;
    public float distanciaMinAbrir;
    private bool cofreAbierto = false;
    public GameObject[] recompensas;
    public float fuerzaImpulso;
    public Vector2 direccionImpulso = new Vector2(1f,1f);

    public TextMeshPro e;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AbirCofre();
    }

    void AbirCofre()
    {
        distanciaHastaPlayer = Vector2.Distance(transform.position, player.position);
        sePuedeAbrir = (distanciaHastaPlayer <= distanciaMinAbrir) ? true : false;
        e.gameObject.SetActive(sePuedeAbrir && !cofreAbierto);

        if(sePuedeAbrir && !cofreAbierto && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CofreAbierto());
        }
    }

    IEnumerator CofreAbierto()
    {
        cofreAbierto = true;
        anim.SetBool("CofreAbierto", true);
        yield return new WaitForSeconds(0.5f);
        int randomObject = Random.Range(0, recompensas.Length);
        GameObject recompensa = Instantiate(recompensas[randomObject], transform.position, Quaternion.identity);
        Rigidbody2D rb = recompensa.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.AddForce(direccionImpulso.normalized * fuerzaImpulso, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaMinAbrir);

        Gizmos.color = Color.green;
        Vector3 origen = transform.position;
        Vector3 destino = origen + (Vector3)(direccionImpulso.normalized * 2f); // 2f = longitud visual de la flecha
        Gizmos.DrawLine(origen, destino);
        Gizmos.DrawSphere(destino, 0.05f); // marca la punta
    }
}
