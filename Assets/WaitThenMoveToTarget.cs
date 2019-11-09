using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenMoveToTarget : MonoBehaviour
{

    // Set your parameters in the Inspector.
    public Animator meshAnim;
    public float waitSeconds = 60f;
    public Vector3 targetOffset = Vector3.forward * 10f;
    public float speed = 1f;
    bool esq;

    // Make Start a coroutine that begins 
    // as soon as our object is enabled.
    IEnumerator Start()
    {
        
        // First, wait our defined duration.
        yield return new WaitForSeconds(waitSeconds);

        // Then, pick our destination point offset from our current location.
        Vector3 targetPosition = transform.position + targetOffset;

        if (esq)
        {
            meshAnim.Play("PassoEsq");
        }

        // Loop until we're within Unity's vector tolerance of our target.
        while (transform.position != targetPosition)
        {

            // Move one step toward the target at our given speed.
            transform.position = Vector3.MoveTowards(
                  transform.position,
                  targetPosition,
                  speed * Time.deltaTime
             );

            // Wait one frame then resume the loop.
            yield return null;
        }

        // We have arrived. Ensure we hit it exactly.
        transform.position = targetPosition;
        esq = !esq;
    }
}
