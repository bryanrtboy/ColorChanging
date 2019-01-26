using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance = null;

    public MeshRenderer m_cursor;
    public float m_cursorZoffset = -7f;

    [HideInInspector]
    public bool m_cursorIsAttached;
    [HideInInspector]
    public Texture m_defaultCursorTexture;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        m_cursor.gameObject.SetActive(false);
        m_defaultCursorTexture = m_cursor.material.mainTexture;

    }

    void Update()
    {
        if (m_cursorIsAttached)
        {
            m_cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z + m_cursorZoffset));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_cursorIsAttached = false;
            m_cursor.gameObject.SetActive(false);
            m_cursor.material.mainTexture = m_defaultCursorTexture;
        }
    }


}
