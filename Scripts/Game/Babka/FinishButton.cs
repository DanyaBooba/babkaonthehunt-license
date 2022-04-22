using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishButton : MonoBehaviour
{
    private InputControll controll;
    
    private LoadLevel level;
    private Platform platform;
    
    private GameObject buttonFinish;
    private GameObject buttonEnd;
    private GameObject pcLoad;

    private int whatload;
    
    private void Start()
    {
        level = GetComponent<LoadLevel>();
        platform = GetComponent<Platform>();
        
        pcLoad = GameObject.FindGameObjectWithTag("uiLoadPC");
        buttonFinish = GameObject.FindGameObjectWithTag("uiLoad");
        buttonEnd = GameObject.FindGameObjectWithTag("uiEnd");
    
        FinishDisable();
        EndDisable();
    }
    
    private void Awake()
    {
        controll = new InputControll();

        controll.Controll.ActionE.performed += context => LoadPC();
    }

    private void OnEnable()
    {
        controll.Enable();
    }

    private void OnDisable()
    {
        controll.Disable();
    }

    public void EndAnable()
    {
        SetActiveEnd(true);
    }

    public void EndDisable()
    {
        SetActiveEnd(false);
    }

    public void FinishAnable()
    {
        SetActiveFinish(true);
    }

    public void FinishDisable()
    {
        SetActiveFinish(false);
    }

    private void SetActiveEnd(bool active)
    {
        if (platform.isAndroid())
        {
            buttonEnd.SetActive(active);   
        }
        else
        {
            buttonEnd.SetActive(false);
            pcLoad.SetActive(active);
            
            if (active == false)
                whatload = 0;
            else
                whatload = 2;
        }
    }

    private void SetActiveFinish(bool active)
    {
        if (platform.isAndroid())
        {
            buttonFinish.SetActive(active);
        }
        else
        {
            buttonFinish.SetActive(false);
            pcLoad.SetActive(active);

            if (active == false)
                whatload = 0;
            else
                whatload = 1;
        }
    }
    
    #if !UNITY_ANDROID
    private void LoadPC()
    {
        if (whatload == 1)
        {
            level.LoadNext();
        }
        else if (whatload == 2)
        {
            level.LoadLevelID(0);
        }
    }
    #endif
}
