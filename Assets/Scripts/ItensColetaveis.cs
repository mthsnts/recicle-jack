using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Collider2D circleCollider;
    public GameObject coletado;
    public int pontuacao;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            FindObjectOfType<AudioManager>().somColetarItem.Play();
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            coletado.SetActive(true);

            //GameController.instancia.qtdeLixo += pontuacao;
            GameController.instancia.atualizaLixoRestante();
            Destroy(gameObject, 0.3f);
        }    
    }


}
