using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    public Vector3 offset;
    public Vector3 minvalue, maxValue;
    // Start is called before the first frame update

    /// <summary>
    /// LateUpdate se actualiza al final de todos los updates
    /// </summary>
    private void LateUpdate()
    {
        //Definimos valores mínimos y máximos para x, y , z


        Vector3 desiredPosition = target.position + offset;
        //verificar si la posición deseada está dentro o no de los límites del escenario y limitar los valores
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, minvalue.x, maxValue.x),
            Mathf.Clamp(desiredPosition.y, minvalue.y, maxValue.y),
            Mathf.Clamp(desiredPosition.z, minvalue.z, maxValue.z));


        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, speed);
        //smoothPosition.y = 0f;
        transform.position = smoothPosition;
    }
}
