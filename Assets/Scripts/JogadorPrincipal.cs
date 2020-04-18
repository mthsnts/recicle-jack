using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorPrincipal : MonoBehaviour
{
    public float velocidadePersonagem;
    public float forcaPulo;
    public bool estaPulando;
    public bool puloDuplo;

    private Rigidbody2D rigidBody;
    private Animator animacao;

    // Start is called before the first frame update
    void Start()
    {
        //armazena o componente rigidBody na variável para realizar o pulo
        rigidBody = GetComponent<Rigidbody2D>();
        //inicializa o componente de animação
        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarPersonagem();
        Pular();
    }

    void MovimentarPersonagem(){
        //movimento recebe valores zero pois o movimento é lateral (Horizontal: setas A e -> e D <-)
        Vector3 movimento = new Vector3(Input.GetAxis("Horizontal"),0f,0f);
        /*transform position recebe o valor do movimento (valor -1 esquerda e 1 direita) 
        e multiplica pela velocidade para dar velocidade*/
        transform.position += movimento * Time.deltaTime * velocidadePersonagem;
        // animação andando para a direita
        if(Input.GetAxis("Horizontal") > 0f){
            animacao.SetBool("andar",true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        // animação andando para a esquerda
        if(Input.GetAxis("Horizontal") < 0f){
            animacao.SetBool("andar",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f){
            animacao.SetBool("andar",false);
        }

    }

    void Pular(){
        //Lista de botões Unity: Edit > Project Settings > InputManager > Axes > Jump(padrão é space)
        if(Input.GetButtonDown("Jump")){
            //adiciona a força do pulo e o pulo duplo
            if(!estaPulando){
                rigidBody.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                puloDuplo = true;
                animacao.SetBool("pular",true);
            } else {
                //se entrar no pulo duplo ele limita para não poder pular mais de 2 vezes
                if(puloDuplo){
                    rigidBody.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                    puloDuplo = false;
                }
            }
            
        }
    }

    //quando o personagem tocou algo, este método é chamado
    void OnCollisionEnter2D(Collision2D colisao)
    {
        // 8 = código da Layer Chao cadastrada no Unity
        if(colisao.gameObject.layer == 8){
            //se jogador tocou algo, não está pulando
            estaPulando = false;
            animacao.SetBool("pular",false);
        }

        //verifica se jogador tocou os espinhos, caso sim aciona o gameover e destrói o objeto do personagem
        if(colisao.gameObject.tag == "espinhos"){
            GameController.instancia.chamaGameOver();
            Destroy(gameObject);
        }
    }
    //quando o personagem para de tocar algo, este método é chamado
    void OnCollisionExit2D(Collision2D colisao)
    {
         // 8 = código da Layer Chao cadastrada no Unity
        if(colisao.gameObject.layer == 8){
            //personagem não está no chão, então está pulando
            estaPulando = true;
        }
    }
}
