using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour {
    [Header("Set in inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
	// Use this for initialization
	void Start () {
        basketList = new List<GameObject>();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
	}

    public void AppleDestroyed()
    {
        // destroy all of the falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }

        // destroy one of the baskets
        // get the index of the last basket in the list
        int basketIndex = basketList.Count - 1;
        // get a reference to that basket's instance
        GameObject TBasketGO = basketList[basketIndex];
        // remove the basket from the list and destroy it
        basketList.RemoveAt(basketIndex);
        Destroy(TBasketGO);

        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
	
	// Update is called once per frame
	void Update () {


	}
}
