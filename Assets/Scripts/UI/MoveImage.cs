using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveImage : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TurnImageOff());
    }

    IEnumerator TurnImageOff()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
