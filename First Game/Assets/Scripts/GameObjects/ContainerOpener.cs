using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerOpener : MonoBehaviour
{
    public Animator animator;
    public GameObject[] itemPrefab;
    [HideInInspector] public bool hasBeenOpened;
    public GameObject popOutPoint;

    public int itemAmount;

    List<GameObject> items = new List<GameObject>();    

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public IEnumerator OpenChest()
    {
        hasBeenOpened = true;

        // Change sprite to next animation, changing its shadows
        animator.SetBool("isOpened", true);
        gameObject.transform.Find("SecondAnimation").gameObject.SetActive(true);
        gameObject.transform.Find("FirstAnimation").gameObject.SetActive(false);
        
        for (int i = 0; i < itemAmount; i++)
        {
            // ** Change food when new items are made like gold and shit ** - Raydon
            // Instiantate a new item and get its components
            items.Add(Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length - 1)], gameObject.transform.position + popOutPoint.transform.localPosition, Quaternion.identity)); // If rarity is added we will have to change this code. - Raydon
            Rigidbody2D rb = items[i].AddComponent<Rigidbody2D>();
            Food food = items[i].GetComponent<Food>();

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
