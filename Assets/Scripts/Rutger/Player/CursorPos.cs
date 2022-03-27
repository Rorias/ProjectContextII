using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPos : MonoBehaviour
{
    public Canvas canvas;
    public Vector3 offset;
    float scale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NoRealCursor());
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas)
        {
            scale = (float)Screen.width / canvas.renderingDisplaySize.x;
        }
        GetComponent<RectTransform>().anchoredPosition = (Input.mousePosition * scale) + offset;
    }

    IEnumerator NoRealCursor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Cursor.visible = false;
        }
    }
}
