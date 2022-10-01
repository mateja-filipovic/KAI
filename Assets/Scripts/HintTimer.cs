using System.Collections;
using UnityEngine;

public class HintTimer : MonoBehaviour
{
    [SerializeField]
    public float _dissapearAfterSeconds = 3;
 
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_dissapearAfterSeconds);
        Destroy(gameObject);
    }
}
