using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigidBody;
    private Vector2 moveVelocity;
    private Animator anim;
    private SpriteRenderer render;
    private int widthFactor = 3;
    private int heightFactor = 8;
    public GameObject inventory;
    private List<string> itemsText = new List<string>();
    public GameObject deathAnim;
    public GameObject typer;
    public GameObject restart;
    private bool pause;

    void Start(){
        pause = false;
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        //resizeToScreen(); //PlayerWrapper object in the inspector properly sizes the player sprite based on resolution!!!
    }

    public void addItem(string newItem){
        itemsText.Add(newItem);
    }
    public void removeItem(string oldItem){
        for (int i = 0; i < itemsText.Count; i++){
            if (itemsText[i] == oldItem){
                itemsText.RemoveAt(i);
            }
        }
    }
    public List<string> getInventory(){
        return itemsText;
    }

    public IEnumerator killPlayer(){
        pause = true;
        moveVelocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        render.enabled = false;
        deathAnim.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        deathAnim.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        inventory.SetActive(false);
        typer.GetComponent<actionTyper>().receiveActionBypass("\nYou ran out of room.");
        yield return new WaitForSeconds(3.0f);
        restart.GetComponent<restartScript>().resetWorld();
        pause = false;
    }

    void resizeToScreen(){
        render = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1,1,1);
 
        float width = render.sprite.bounds.size.x;
        float height = render.sprite.bounds.size.y;
 
 
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
 
        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth / widthFactor;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight / heightFactor;
        //transform.localScale.y = worldScreenHeight / height;
    }

    void Update(){
        if (!pause){
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();
            moveVelocity = moveInput * speed;

            if (moveInput.x > 0){
                render.flipX = false;
            }
            if (moveInput.x < 0){
                render.flipX = true;
            }
            if (moveInput.y > 0){
                anim.SetBool("movingDown", false);
                anim.SetBool("movingUp", true);
            }
            if (moveInput.y < 0){
                anim.SetBool("movingUp", false);
                anim.SetBool("movingDown", true);
            }
            if (moveInput.y == 0){
                anim.SetBool("movingUp", false);
                anim.SetBool("movingDown", false);
            }
            if (moveInput.x != 0){
                anim.SetBool("moving", true);
            }
            else{
                anim.SetBool("moving", false);
            }
            if(Input.GetKeyDown(KeyCode.I)){
                inventory.SetActive(true);
                inventory.GetComponent<inventoryBehaviour>().open(itemsText);
                this.enabled = false;
            }
        }
    }

    public void stopMoving(){
        anim.SetBool("moving", false);
        anim.SetBool("movingDown", false);
        anim.SetBool("movingUp", false);
    }

    public void setPause(bool newPause){
        //this.stopMoving();
        pause = newPause;
    }

    public bool getPause(){
        return pause;
    }

    void FixedUpdate(){
        rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime);
    }
}
