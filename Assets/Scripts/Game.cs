using System;
using System.Linq;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.UI;

public class Game : GameNetworkBehavior
{
    [SerializeField] private Sprite[] sprites;
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
        _restart.onClick.AddListener(() => networkObject.SendRpc(RPC_RESTART_GAME, Receivers.All));
        _restart.GetComponentInChildren<Text>().text = "Restart";
        for (int i = 0; i < _buttons.Length; i++)
        {
            int nI = i;
            _buttonsText[i] = _buttons[i].GetComponentInChildren<Text>();
            _buttonsText[i].color = new Color(0, 0, 0, 0);
            _buttons[i].onClick.AddListener(() => ExecuteTurn(nI));
        }
        GameDefaults();
    }

    private void ExecuteTurn(int buttonId)
    {
        if (_buttonsText[buttonId].text != "0") return;
        switch (_playerId)
        {
            case 1 when networkObject.IsServer:
            case 2 when !networkObject.IsServer:
                networkObject.SendRpc(RPC_TURN, Receivers.All, buttonId, _playerId);
                break;
        }
    }

    public override void Turn(RpcArgs args)
    {
        int buttonId = args.GetNext<int>();
        int playerId = args.GetNext<int>();
        string playerIdStr = Convert.ToString(playerId);
        _playerId = playerId == 1 ? 2 : 1;
        _buttons[buttonId].image.sprite = sprites[playerId];
        _buttonsText[buttonId].text = playerIdStr;
        if (!IsGameOver(playerIdStr)) return;
        foreach (Button button in _buttons) button.enabled = false;
        Invoke(nameof(Win), 2f);
    }

    private void Win()
    {
        _buttonParent.SetActive(false);
        _gameOverParent.SetActive(true);
        _gameOverParent.GetComponentInChildren<Text>().text = _playerId == 1 ? "0" : "X" + "Wins!";
    }

    
    
    public override void RestartGame(RpcArgs args) => GameDefaults();

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
        foreach (Button button in _buttons)
        {
            button.enabled = true;
            button.image.sprite = sprites[0];
        }
        foreach (Text buttonText in _buttonsText) buttonText.text = "0";
        _playerId = 1;
        _buttonParent.SetActive(true);
        _gameOverParent.SetActive(false);
    }
}