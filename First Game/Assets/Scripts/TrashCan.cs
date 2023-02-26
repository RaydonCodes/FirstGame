using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Animator animator;
    public GameObject[] foodPrefab;
    [HideInInspector] public bool hasBeenOpened;

    List<GameObject> foods = new List<GameObject>();

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator OpenTrashCan()
    {
        hasBeenOpened = true;
        animator.SetBool("IsOpened", true);
            
        for(int i = 0; i < 2; i++)
        {
            foods.Add(Instantiate(foodPrefab[Random.Range(0, foodPrefab.Length - 1)], gameObject.transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity));
            print(foods[i].name);
            Rigidbody2D rb = foods[i].AddComponent<Rigidbody2D>();

            float[] xForcePossibleValues = {Random.Range(1f, 2f), Random.Range(-1f, -2f)};
            float xforce = xForcePossibleValues[Random.Range(0, 2)];
            float yForce = Random.Range(1f, 2f);
            rb.AddForce(new Vector2(xforce, yForce), ForceMode2D.Impulse);
            print(xforce + "y:" + yForce);
            yield return new WaitForSeconds(.5f);
        }                       
        
    }
    
}
