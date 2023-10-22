using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollider : MonoBehaviour
{
    public AudioClip explosionAudioClip;
    public GameObject mineGameObject;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerRevive.AddListener(ResetMine);
    }
    public void ResetMine() {
        mineGameObject.GetComponent<MeshRenderer>().enabled = true;
        mineGameObject.GetComponent<Collider>().enabled = true;
        GetComponent<LineRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player")) {
            GameManager.SpawnLoudAudio(explosionAudioClip);
            mineGameObject.GetComponent<MeshRenderer>().enabled = false;
            mineGameObject.GetComponent<Collider>().enabled = false;
            GetComponent<LineRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
