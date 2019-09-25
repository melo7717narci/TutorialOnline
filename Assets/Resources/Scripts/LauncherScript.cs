using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LauncherScript : Photon.PunBehaviour
{
    #region Public Variables

    #endregion

    #region Private Variables

    string _gameVersion = "test";

    #endregion

    #region Public Methods

    public void Connect()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
            Debug.Log("Connected to Photon");
        }
    }

    #endregion

    #region Photon Callback

    public override void OnJoinedLobby()
    {
        Debug.Log("Entered the lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Failed to enter the room");
        PhotonNetwork.CreateRoom("TestRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entered the room");
        PhotonNetwork.LoadLevel("Battle");
    }
    #endregion

}
