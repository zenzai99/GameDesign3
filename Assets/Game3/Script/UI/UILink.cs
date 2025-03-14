﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILink : MonoBehaviour
{
    #region Singleton
    public static UILink instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public TMP_Text stateGameText;
    public TMP_Text statePlayerText;

    public Canvas mainCanvas;
    public GameObject debugConsole;
    public GameObject inventoryWindow;
    public GameObject teamWindow;

    public Transform panelMemberControl;

    public delegate void state();
    public state active;

    public bool haveWindowInOpen;

    // Update is called once per frame
    void Update()
    {
        stateGameText.text = "Game State: " + GameSystem.instance.active.Method.ToString().Substring(5);
        statePlayerText.text = "Player State: " + PlayerController.instance.active.Method.ToString().Substring(5);

        if (PlayerTeam.instance.NullCharacter(0))
        {
            Debug.LogWarning("Please input team leader slot.");
            OpenTeamWindow();
        }

        for (int i = 0; i < PlayerTeam.instance.Count; i++)
        {
            if (PlayerTeam.instance.NullCharacter(i))
            {
                panelMemberControl.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                panelMemberControl.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (inventoryWindow.activeInHierarchy ||
            teamWindow.activeInHierarchy)
        {
            haveWindowInOpen = true;
        }
        else
        {
            haveWindowInOpen = false;
        }

        // Inventory Window
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryWindow.activeInHierarchy)
            {
                OpenInventoryWindow();
            }
            else if (inventoryWindow.activeInHierarchy)
            {
                CloseInventoryWindow();
            }
        }

        // Team Window
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!teamWindow.activeInHierarchy)
            {
                OpenTeamWindow();
            }
            else if (teamWindow.activeInHierarchy)
            {
                CloseTeamWindow();
            }
        }

        // Close all window
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTeamWindow();
            CloseInventoryWindow();
        }

        //
        if (Input.GetKeyDown(KeyCode.F8) && debugConsole.activeInHierarchy)
        {
            debugConsole.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F8) && !debugConsole.activeInHierarchy)
        {
            debugConsole.SetActive(true);
        }

        // reset UI
        if (Input.GetKey(KeyCode.F1))
        {
            MoveInventoryWindow();
        }
        if (Input.GetKey(KeyCode.F2))
        {
            MoveTeamWindow();
        }
    }

    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    public void CloseTeamWindow()
    {
        teamWindow.SetActive(false);
    }

    public void OpenTeamWindow()
    {
        teamWindow.SetActive(true);
    }

    public void SetInstanceMemberPanel(Transform[] teamPosition)
    {
        for (int i = 0; i < panelMemberControl.childCount; i++)
        {
            if (panelMemberControl.GetChild(i) != null && !PlayerTeam.instance.NullCharacter(i))
            {
                panelMemberControl.GetChild(i).GetComponent<PanelMember>().targetCharacter = teamPosition[i].GetComponent<DummyCharacter>();
                panelMemberControl.GetChild(i).GetComponent<PanelMember>().UpdateInstance();
            }
        }
    }

    public float speed;
    public void MoveInventoryWindow()
    {
        // inventoryWindow.GetComponent<RectTransform>().position = Input.mousePosition;

        
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, Input.mousePosition, mainCanvas.worldCamera, out pos);
        inventoryWindow.GetComponent<RectTransform>().position = mainCanvas.transform.TransformPoint(pos);
        


        /*
        float x = 0; x += Input.GetAxisRaw("Mouse X")*Time.deltaTime*speed;
        float y = 0; y += Input.GetAxisRaw("Mouse Y") * Time.deltaTime* speed;

        Vector3 temp = inventoryWindow.GetComponent<RectTransform>().position;

        inventoryWindow.GetComponent<RectTransform>().position = new Vector3(temp.x + x, temp.y + y, 0);
        */

    }

    public void MoveTeamWindow()
    {
        // teamWindow.GetComponent<RectTransform>().position = Input.mousePosition;

        
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, Input.mousePosition, mainCanvas.worldCamera, out pos);
        teamWindow.GetComponent<RectTransform>().position = mainCanvas.transform.TransformPoint(pos);
        

    }



}
