using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [Header("set dynamically")]
    public Text scoreGT;
    // Use this for initialization
    private void Start()
    {
        // find score counter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // get the text
        scoreGT = scoreGO.GetComponent<Text>();
        // set starting score to 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update () {
        // Get the current screen postion of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;
        // the camera's position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        // Convert the point from 2D screen space inot 3D world space
        Vector3 mousePos3D = Camera.main.ScreenToViewportPoint(mousePos2D);

        // move the x position of this basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        // find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);

            // make scoregt an int
            int score = int.Parse(scoreGT.text);
            score += 100;
            // convert score back to text to display
            scoreGT.text = score.ToString();
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
    }
}
