using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorizer : MonoBehaviour
{
    public string m_renderTag = "Paintable";
    List<MeshRenderer> m_renderers;
    Color m_currentColor = Color.white;
    Texture m_currentTexture;

    void Awake()
    {
        MeshRenderer[] mrs = this.GetComponentsInChildren<MeshRenderer>();
        m_renderers = new List<MeshRenderer>();

        foreach (MeshRenderer m in mrs)
        {
            if (m.tag == m_renderTag)
            {
                m_renderers.Add(m);
                m_currentColor = m.material.color;
                if (m.material.mainTexture != null)
                    m_currentTexture = m.material.mainTexture;
            }
        }
    }

    void OnMouseEnter()
    {
        if (!ObjectManager.instance.m_cursorIsAttached)
            return;

        ApplyColorOrMaterial(true, false);
    }

    void OnMouseExit()
    {
        if (!ObjectManager.instance.m_cursorIsAttached)
            return;

        ApplyColorOrMaterial(false, false);

    }

    void OnMouseDown()
    {
        if (!ObjectManager.instance.m_cursorIsAttached)
            return;

        ApplyColorOrMaterial(true, true);
    }

    void ApplyColorOrMaterial(bool showCursorColors, bool save)
    {
        if (showCursorColors)
        {
            foreach (MeshRenderer m in m_renderers)
            {
                //Only apply a texture if the cursor texture is not the default dot...
                if (ObjectManager.instance.m_cursor.material.mainTexture != ObjectManager.instance.m_defaultCursorTexture)
                {
                    //Only apply to materials that already use a texture
                    if (m.material.mainTexture != null)
                    {
                        m.material.mainTexture = ObjectManager.instance.m_cursor.material.mainTexture;
                        if (save)
                            m_currentTexture = m.material.mainTexture;
                    }
                }
                else
                {
                    //Don't apply color to things that have a material
                    if (m.material.mainTexture == null)
                    {
                        m.material.color = ObjectManager.instance.m_cursor.material.color;
                        if (save)
                        {
                            m.material.color = ObjectManager.instance.m_cursor.material.color;
                            m_currentColor = m.material.color;
                        }
                    }
                }

            }

        }
        else
        {
            foreach (MeshRenderer m in m_renderers)
            {
                m.material.color = m_currentColor;
                if (m.material.mainTexture != null)
                    m.material.mainTexture = m_currentTexture;
            }

        }

    }

}
