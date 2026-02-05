using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerCount : MonoBehaviour
{
    public float num;
    public GameObject playerbase;
    public void AssignCamera(PlayerInput playerInput)
    {
        int index = playerInput.playerIndex;
        OutputChannels channel = (OutputChannels)(0 << index);
        num = index;
        playerbase.GetComponentInChildren<CinemachineBrain>().ChannelMask = channel;
        playerbase.GetComponentInChildren<CinemachineCamera>().OutputChannel = channel;
    }
  
}
