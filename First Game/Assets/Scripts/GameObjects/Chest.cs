using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    public GameObject[] foodPrefab;
    [HideInInspector] public bool hasBeenOpened;
    public GameObject popOutPoint;

    public int foodAmount;

    List<GameObject> foods = new List<GameObject>();    
    

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator OpenChest()
    {
        hasBeenOpened = true;

        // Change sprite to next animation, changing its shadows
        animator.SetBool("IsOpened", true);
        gameObject.transform.Find("SecondAnimation").gameObject.SetActive(true);
        gameObject.transform.Find("FirstAnimation").gameObject.SetActive(false);
        
        for(int i = 0; i < foodAmount; i++)
        {
            // Instiantate a new burger and get its components
            foods.Add(Instantiate(foodPrefab[Random.Range(0, foodPrefab.Length - 1)], gameObject.transform.position + popOutPoint.transform.localPosition, Quaternion.identity));
            Rigidbody2D rb = foods[i].AddComponent<Rigidbody2D>();
            Food food = foods[i].GetComponent<Food>();

            // Timer so you can't eat food instantly
            food.canBeEaten = false;
            StartCoroutine(food.MakeFoodEdible(.5f));

            // Get a random force values (pop out animation)
            float[] xForcePossibleValues = {Random.Range(1f, 2f), Random.Range(-1f, -2f)};
            float xforce = xForcePossibleValues[Random.Range(0, 2)];
            float yForce = Random.Range(1f, 2f);
            rb.AddForce(new Vector2(xforce, yForce), ForceMode2D.Impulse);

            // Wait so burgers don't overlap
            yield return new WaitForSeconds(.5f);
        }
    }
    
}
