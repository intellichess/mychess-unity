
using UnityEngine;

public class Launcher : Photon.PunBehaviour {

    public string version = "0.1";
    public PhotonLogLevel networkLogging = PhotonLogLevel.ErrorsOnly;

    private void Awake()
    {
        PhotonNetwork.logLevel = networkLogging;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    public void MasterServerConnect()
    {
        Debug.Log("trying to connect to master server");
        PhotonNetwork.ConnectUsingSettings(this.version);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("connected successfully");
        
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log(cause.ToString());
    }





}
