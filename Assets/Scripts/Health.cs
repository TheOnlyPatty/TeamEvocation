using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Int for player health and number of hearts
    public int health;
    public int numOfHearts;

    //sprite for hearts and array for all heart sprites
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public AudioSource damageNoise;

    public float velocity;



    private void Update()
    {
      velocity = GetComponent<Rigidbody2D>().velocity.magnitude;

        //for each heart in the array of sprites
        for (int i = 0; i < hearts.Length; i++)

        {
            //checks to make sure health lines up with num of hearts
            if(health > numOfHearts)
            {
                health = numOfHearts;
            }

            //Changes heart sprite from full to half if player loses health
            if( i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //enables each heart in the array as long as the health is greater than the num of hearts
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }

        }
        if (health <= 0)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void takeDamage()
    {
        health--;
        damageNoise.Play();
    }


}
