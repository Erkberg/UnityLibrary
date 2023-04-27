using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuTemplate : MonoBehaviour
{
    [Header("State")]
    public State state; 

    [Header("Refs")]
    public GameObject holder;
    public TextMeshProUGUI startContinueRestartButtonText;
    public GameObject quitButton;

    [Header("Config")]
    public bool openOnStart = true;
    public bool pauseTimeWhileOpen = true;
    public string legacyInputSystemMenuButton;
    [Space]
    public string startText = "Start";
    public string continueText = "Continue";
    public string restartText = "Restart";

    public enum State
    {
        Start,
        Pause,
        End
    }

    private void Start()
    {
        if(openOnStart)
        {
            Open();
        }

#if UNITY_WEBGL
        quitButton.SetActive(false);
#endif
    }

    private void Update()
    {
        CheckMenuButton();
    }

    private void CheckMenuButton()
    {
        if(!string.IsNullOrEmpty(legacyInputSystemMenuButton))
        {
            if(Input.GetButtonDown(legacyInputSystemMenuButton))
            {
                OnMenuButtonDown();
            }
        }
        else
        {
            // check input system package
        }
    }

    private void OnMenuButtonDown()
    {
        if(IsOpen())
        {
            if(state == State.Pause)
            {
                Close();
            }            
        }
        else
        {
            Open();
        }
    }

    public void SetState(State state)
    {
        this.state = state;
    }

    public void Open()
    {
        AdjustButtonsToState();
        holder.SetActive(true);

        if(pauseTimeWhileOpen)
        {
            Time.timeScale = 0f;
        }
    }

    public void Close()
    {
        holder.SetActive(false);

        if (pauseTimeWhileOpen)
        {
            Time.timeScale = 1f;
        }
    }

    private void AdjustButtonsToState()
    {
        switch (state)
        {
            case State.Start:
                startContinueRestartButtonText.text = startText;
                break;

            case State.Pause:
                startContinueRestartButtonText.text = continueText;
                break;

            case State.End:
                startContinueRestartButtonText.text = restartText;
                break;
        }
    }

    public void OnStartContinueRestartButtonClicked()
    {
        switch (state)
        {
            case State.Start:
                Close();
                state = State.Pause;
                break;

            case State.Pause:
                Close();
                break;

            case State.End:
                break;
        }
    }

    public bool IsOpen()
    {
        return holder.activeSelf;
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
