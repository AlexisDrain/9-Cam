using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLazersGenerator : MonoBehaviour
{
    public List<GameObject> lazerBundles = new List<GameObject>();
    public AudioClip lazerEnableSFX;
    public Transform laserOriginFront;
    public Transform laserOriginBack;

    private int currentLazer = 0;
    // Start is called before the first frame update
    void Start()
    {
        ResetLazers();
        GameManager.playerRevive.AddListener(ResetLazers);
    }

    // Update is called once per frame
    void ResetLazers()
    {

        for (int i = 0; i < laserOriginFront.childCount; i++) {
            Destroy(laserOriginFront.GetChild(i).gameObject);
        }
        for (int i = 0; i < laserOriginBack.childCount; i++) {
            Destroy(laserOriginBack.GetChild(i).gameObject);
        }

        StopCoroutine(LazerTimeline());
        StartCoroutine(LazerTimeline());
    }

    private IEnumerator LazerTimeline() {
        GameObject.Instantiate(lazerBundles[currentLazer], Vector3.zero, Quaternion.identity, laserOriginFront);
        GameManager.SpawnLoudAudio(lazerEnableSFX);
        currentLazer += 1;
        yield return new WaitForSeconds(3f);
        GameObject.Instantiate(lazerBundles[currentLazer], Vector3.zero, Quaternion.identity, laserOriginBack);
        GameManager.SpawnLoudAudio(lazerEnableSFX);
        currentLazer += 1;
        yield return new WaitForSeconds(3f);
        GameObject.Instantiate(lazerBundles[currentLazer], Vector3.zero, Quaternion.identity, laserOriginFront);
        GameManager.SpawnLoudAudio(lazerEnableSFX);
        currentLazer += 1;
    }
}
