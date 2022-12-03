using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossController : BossController
{
    
    float value;
    [SerializeField] private float countdown = 5f;
    PullTowards pull;
    PushAway push;

    float timeRemaining;
    float duration;
    // Start is called before the first frame update
    void Start()
    {
        pull = GetComponent<PullTowards>();
        push = GetComponent<PushAway>();
        timeRemaining = countdown;
        value = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            if (value == 0)
            {
                pull.enabled = true;
            }
            else
            {
                push.enabled = true;
            }
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        pull.enabled = false;
        push.enabled = false;
        value = Random.Range(0, 2);
        timeRemaining = countdown;
    }

}
