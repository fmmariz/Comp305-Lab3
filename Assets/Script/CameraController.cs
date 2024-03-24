using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Objects")]
    [SerializeField] private GameObject offset;

    [Header("Parallax")]
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject far;

    private Material closeMaterial;
    private Material farMaterial;

    void Start()
    {
        closeMaterial = close.GetComponent<SpriteRenderer>().material;
        farMaterial = far.GetComponent<SpriteRenderer>().material;
        closeMaterial.SetFloat("_Distance", 0.05f);
        farMaterial.SetFloat("_Distance", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        var offsetPosition = offset.transform.position;
        var currentPosition = transform.position;
        Vector3 vector3 = Vector2.MoveTowards(currentPosition, offsetPosition, 0.1f);
        transform.position = new Vector3(vector3.x, currentPosition.y, currentPosition.z);
        closeMaterial.SetVector("_Offset", new Vector2(transform.position.x, transform.position.y));
        farMaterial.SetVector("_Offset", new Vector2(transform.position.x, transform.position.y));

    }

}
