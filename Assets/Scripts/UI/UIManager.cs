using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private IUI[] m_menuList;

    private void Awake()
    {
        m_menuList = GetComponentsInChildren<IUI>(true);
    }

    private void Start()
    {
        for (int i = 0; i < m_menuList.Length; i++)
        {
            m_menuList[i].Setup(this);
        }
    }

    public void ShowMenu<T>() where T : IUI
    {
        for (int i = 0; i < m_menuList.Length; i++)
        {
            IUI menu = m_menuList[i];
            if (menu is T)
            {
                menu.Show();
            }
            else
            {
                menu.Hide();
            }
        }
    }
}