using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    [SerializeField] TalkText _talkText;
    [SerializeField] PlayerV2 _playerV2;

    private PlayerControls _input;
    private bool _interactPressed;

    private bool convoStarted = false;
    private int convCount = 0;
    private bool _wasPressed = false;

    public string[] convo;
    
    private void Awake()
    {
        _input = new PlayerControls();
        _input.CharacterControls.Interact.performed += ctx => _interactPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.Enable();
    }
    private void Start()
    {
        convo = new string[] {"Hey Sun.",
            "How\'s the mission coming along?",
            "Try not to die out there!" };

    }
    // Update is called once per frame
    void Update()
    {
        if (_interactPressed && !_wasPressed && convoStarted && _playerV2.getCanTalk())
        {
            if (convCount == convo.Length)
            {
                _talkText.updateText("");
            }
            else
            {
                _talkText.updateText(convo[convCount]);
                convoStarted = true;

                if (convCount < convo.Length)
                {
                    convCount++;
                }
            }
            _wasPressed = true;
        }
        if (!_interactPressed && _wasPressed)
        {
            _wasPressed = false;
        }
        else if (_playerV2.getCanTalk() && !convoStarted && convCount != convo.Length)
        {
            _talkText.updateText("Press E to talk.");
            convoStarted = true;
        }
        else if(!_playerV2.getCanTalk())
        {
            _talkText.updateText("");
            convoStarted = false;
        }

    }
}
