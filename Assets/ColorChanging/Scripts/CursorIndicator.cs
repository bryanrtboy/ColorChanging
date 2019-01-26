using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIndicator : MonoBehaviour
{
    Color m_paintColor = Color.white;
    Texture m_texture;

    bool m_isAttached = false;

    void Start()
    {
        if (ObjectManager.instance.m_cursor == null)
        {
            Debug.LogError("No cursor tex available!");
            Destroy(this);
        }

        m_paintColor = this.GetComponent<MeshRenderer>().material.color;
        m_texture = this.GetComponent<MeshRenderer>().material.mainTexture;
    }
    void OnMouseEnter()
    {
        ObjectManager.instance.m_cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        ObjectManager.instance.m_cursor.material.color = m_paintColor;
        if (m_texture != null)
            ObjectManager.instance.m_cursor.material.mainTexture = m_texture;
        else
            ObjectManager.instance.m_cursor.material.mainTexture = ObjectManager.instance.m_defaultCursorTexture;

        ObjectManager.instance.m_cursor.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        if (m_isAttached)
        {
            ObjectManager.instance.m_cursorIsAttached = true;
            m_isAttached = false;
        }
        else if (!ObjectManager.instance.m_cursorIsAttached)
        {
            ObjectManager.instance.m_cursor.gameObject.SetActive(false);
        }


    }

    void OnMouseDown()
    {
        m_isAttached = true;
    }
}
