using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLazersGenerator : MonoBehaviour
{
    public List<GameObject> lazerBundles = new List<GameObject>();
    public List<Sprite> countdownSprites = new List<Sprite>();

    public SpriteRenderer doorLED;
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
        doorLED.sprite = countdownSprites[currentLazer];
        StartCoroutine("LazerTimeline");
    }

    private IEnumerator LazerTimeline() {
        yield return new WaitForSeconds(2f);
        GameObject lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        lazerCurrent = GameObject.Instantiate(lazerBundles[currentLazer], laserOriginFront.position, Quaternion.identity, laserOriginFront); // front
        currentLazer += 1;
        doorLED.sprite = countdownSprites[currentLazer];
        yield return new WaitForSeconds(4f);
        Destroy(lazerCurrent);
        doorLED.sprite = countdownSprites[currentLazer+1];

        doorOpen = true;
        doorAnimator.SetTrigger("OpenDoorNHandle");
    }
}
