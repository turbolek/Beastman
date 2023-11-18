using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekRange : MonoBehaviour
{

    private Action<PlayerController> _onPlayerEnterCallback;
    private Action<PlayerController> _onPlayerExitCallback;

    public void Init(Action<PlayerController> onEnterCallback, Action<PlayerController> onExitCallback)
    {
        _onPlayerEnterCallback = onEnterCallback;
        _onPlayerExitCallback = onExitCallback;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        _onPlayerEnterCallback?.Invoke(player);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        _onPlayerExitCallback?.Invoke(player);
    }
}
