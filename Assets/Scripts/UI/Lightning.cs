
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private void Stop()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StopLightningAnim();
    }
}
