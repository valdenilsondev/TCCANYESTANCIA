using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class movimentoplayer : MonoBehaviour {

    private inimigoR inimigoR;
    private playermovement _playerMovimento;
    private button _button;


    private Rigidbody2D rb;
    private Animator anim;

    [Header("Conf Movimentação Player")]
    public float speed = 5f;
    public float jumpForce;

    [Header("Conf Lançamento Projetil")]
    public GameObject projetil;
    public GameObject bombaM;
    public Transform posicaoProjetil;
    
    public float forcaTiro;
    public float isAtirar;

    [Header("Conf Pulo Player ")]
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask layerGround;

    [Header("Conf Coletáveis")]
    public int moedas;
    public TextMeshProUGUI textoMoedas;
    private int maca;
    public TextMeshProUGUI textomaca;
    private int agua;
    public TextMeshProUGUI textoagua;

    [Header("Paineis Informações")]
    public GameObject historia;

    [Header("Conf Tempo Fase")]
    public float tempoDecrescente;
    public float tempoDecorrido;
    public float tempoInicial;
    public TextMeshProUGUI tempoFase;

    [Header("Conf Tela Game Over")]
    public bool morrer;
    public GameObject painelGameOver;

    void Start() {

        inimigoR = FindObjectOfType(typeof(inimigoR)) as inimigoR;
        _playerMovimento = FindObjectOfType(typeof(playermovement)) as playermovement;
        _button = FindObjectOfType(typeof(button)) as button;
  
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        painelGameOver.SetActive(false);
        morrer = false;

        tempoInicial = 100;

      
    }

    void Update() {

        tempoFaseJogo();

        if (tempoDecorrido <= 0) {

            painelGameOver.SetActive(true);
            tempoDecorrido = 0;
            _playerMovimento.speed = 0;

        }

        anim.SetBool("Jump", isGrounded);


        atirar();
    }

    private void FixedUpdate() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, layerGround);

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "inimigo" && morrer == false) {

            anim.SetBool("Die", true);
            jumpForce = 0;
            _playerMovimento.speed = 0;
            morrer = true;

        }

        if (collision.gameObject.tag == "inimigoR" && morrer == false) {

            anim.SetTrigger("Die");
            jumpForce = 0;
            _playerMovimento.speed = 0;
            morrer = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Moedas") {

            moedas += 1;
            textoMoedas.text = moedas.ToString();
            Destroy(col.gameObject);

        }

        if (col.gameObject.tag == "inimigoR" && morrer == false) {

            anim.SetTrigger("Die");
            jumpForce = 0;
            morrer = true;

        }

        if (col.gameObject.tag == "maça") {

            maca += 1;
            textomaca.text = maca.ToString();
            Destroy(col.gameObject);

        }

        if (col.gameObject.tag == "agua") {

            agua += 1;
            textoagua.text = agua.ToString();
            Destroy(col.gameObject);

        }

    }

    #region 

    public void tempoFaseJogo() {

        tempoDecrescente += Time.deltaTime;

        tempoDecorrido = tempoInicial - tempoDecrescente;

        tempoFase.text = tempoDecorrido.ToString("0");

    }

    public void tiro() {

        anim.SetTrigger("Tiro");

    }

    public void bomba() {

        Instantiate(bombaM, posicaoProjetil.position, Quaternion.identity);

    }

    public void TiroPlayerParado() {

        GameObject temp = Instantiate(projetil);
        temp.transform.position = posicaoProjetil.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(forcaTiro, 0);

    }

    public void pulo() {

        if (isGrounded == true) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    public void atirar() {

        
      

    }

    public void PainelGameOver() {

        painelGameOver.SetActive(true);

    }

    #endregion
}


