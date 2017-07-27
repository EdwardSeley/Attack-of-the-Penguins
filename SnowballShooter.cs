using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                PenguinController penguin = hitObject.GetComponent<PenguinController>();
                if (penguin != null)
                {
                    Debug.Log("Penguin hit!");
                }
                StartCoroutine(SnowballIndicator(hit.point));
            }
        }
    }

    private IEnumerator SnowballIndicator(Vector3 pos)
    {
        GameObject snowball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        snowball.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(snowball);
    }
}
