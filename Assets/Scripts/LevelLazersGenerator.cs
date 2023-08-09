using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLazersGenerator : MonoBehaviour
{
    public List<GameObject> lazerBundles = new List<GameObject>();

    public Animator doorAnimator;
    public Transform laserOriginFront;
    public Transform laserOriginBack;

    private int currentLazer = 0;
    private bool doorOpen = false;
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
        if(doorOpen) {
            doorOpen = false;
            doorAnimator.SetTrigger("CloseDoor");
        }

        StopCoroutine("LazerTimeline");
        currentLazer = 0;
        StartCoroutine("LazerTimeline");
    }

    private IEnumerator LazerTimeline() {
        yield return new WaitForSeconds(2f);
        GameObject lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);

        doorOpen = true;
        doorAnimator.SetTrigger("OpenDoor");
    }
}
