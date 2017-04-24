using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float fallSpeed = 1;
    private Renderer renderer;

    private void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update () {
        float timeFloatSpeed = fallSpeed * Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - timeFloatSpeed, this.transform.position.z);
	}

    void DestroyIfNotVisible() {
        //FIXME
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "paddle") {
            Debug.Log("Grabbed a power up!");
            Destroy(gameObject);
        }
    }
}
