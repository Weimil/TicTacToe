using System;
using System.Linq;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.UI;

public class Game : GameNetworkBehavior
{
    [SerializeField] private Sprite[] _sprites;
    private GameObject _buttonParent;
    private Button[] _buttons;
    private Text[] _buttonsText;
    private GameObject _gameOverParent;
    private Button _restart;
    private int _playerId;


    private void Start()
    {
        _gameOverParent = GameObject.Find("GameOverParent");
        _buttonParent = GameObject.Find("ButtonParent");
        _restart = _gameOverParent.GetComponentInChildren<Button>();
        _buttons = _buttonParent.GetComponentsInChildren<Button>();
        _buttonsText = new Text[_buttons.Length];
        _restart.onClick.AddListener(ExecuteRestartGame);
        for (int i = 0; i < _buttons.Length; i++)
        {
            int nI = i;
            _buttonsText[i] = _buttons[i].GetComponentInChildren<Text>();
            _buttonsText[i].color = new Color(0, 0, 0, 0);
            _buttons[i].onClick.AddListener(delegate { ExecuteTurn(nI); });
        }

        GameDefaults();
    }

    private void ExecuteTurn(int buttonId)
    {
        if (_buttonsText[buttonId].text != "0") return;
        if (networkObject.IsServer)
        {
            if (_playerId == 1)
                networkObject.SendRpc(RPC_TURN, Receivers.All, buttonId, _playerId);
        }
        else
        {
            if (_playerId == 2)
                networkObject.SendRpc(RPC_TURN, Receivers.All, buttonId, _playerId);
        }
    }

    public override void Turn(RpcArgs args)
    {
        int button = args.GetNext<int>();
        int playerId = args.GetNext<int>();
        string playerIdStr = Convert.ToString(playerId);
        _playerId = playerId == 1 ? 2 : 1;
        _buttons[button].image.sprite = _sprites[playerId];
        _buttonsText[button].text = playerIdStr;
        if (IsGameOver(playerIdStr))
        {
            _gameOverParent.SetActive(true);
            _buttonParent.SetActive(false);
        }
    }

    private void ExecuteRestartGame()
    {
        networkObject.SendRpc(
            RPC_RESTART_GAME,
            Receivers.All
        );
    }

    public override void RestartGame(RpcArgs args) 
    {
        GameDefaults();
    }

    private bool IsGameOver(string str)
    {
        if (_buttonsText.Any(text => text.text == "0"))
            return
                _buttonsText[0].text == str && _buttonsText[1].text == str && _buttonsText[2].text == str ||
                _buttonsText[3].text == str && _buttonsText[4].text == str && _buttonsText[5].text == str ||
                _buttonsText[6].text == str && _buttonsText[7].text == str && _buttonsText[8].text == str ||
                
                _buttonsText[0].text == str && _buttonsText[3].text == str && _buttonsText[6].text == str ||
                _buttonsText[1].text == str && _buttonsText[4].text == str && _buttonsText[7].text == str ||
                _buttonsText[2].text == str && _buttonsText[5].text == str && _buttonsText[8].text == str ||
                
                _buttonsText[2].text == str && _buttonsText[4].text == str && _buttonsText[6].text == str ||
                _buttonsText[0].text == str && _buttonsText[4].text == str && _buttonsText[8].text == str;

        return true;
    }

    private void GameDefaults()
    {
        foreach (Button button in _buttons) button.image.sprite = _sprites[0];
        foreach (Text buttonText in _buttonsText) buttonText.text = "0";
        _playerId = 1;
        _buttonParent.SetActive(true);
        _gameOverParent.SetActive(false);
    }
}