using System;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using ShrimpFPS.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
namespace ShrimpFPS.Runtime
{
    public class NetworkInitializer : MonoBehaviourPunCallbacks
    {
        #region NetworkPlayerUI

        [SerializeField] private Text playerNameText;
        
        #endregion
        [SerializeField] private PhotonView photonView;
        [SerializeField] private Transform[] components;
        [SerializeField] private GameObject playerCam;
        [SerializeField] private GameObject TP_Body;

        public string playerName;
        private ISubject mapInit;
        public void Start()
        {
            if (!photonView.IsMine)
            {
                DisableNetworkComponents();//禁用Owner才能调用的组件
                TP_Body.SetActive(true);//启用第三人称
            }
            else
            {
                playerName = PhotonNetwork.LocalPlayer.NickName;
                Hashtable hash = new Hashtable();
                hash["playerName"] = playerName;
                hash["health"] = 100.0f;
                hash["score"] = 0;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }

            StartCoroutine(nameof(SetPlayerUI));
        }

        public void ChooseTeam(int teamID)
        {
            Hashtable hash = new Hashtable();
            hash["playerName"] = playerName;
            hash["team"] = teamID;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        private void DisableNetworkComponents()
        {
            playerCam.SetActive(false);
            foreach (var trans in components)
            {
                if (trans.GetComponent<INetworkOwnership>() != null)
                {
                    trans.GetComponent<INetworkOwnership>().AbandonControll();
                    Debug.Log("Abandon controll");  
                }
                foreach (Transform subTrans in trans)
                {
                    if (subTrans.GetComponent<INetworkOwnership>() != null)
                    {
                        subTrans.GetComponent<INetworkOwnership>().AbandonControll();
                        Debug.Log("sub trans abandon controll");  
                    }
                }
            }
        }

        IEnumerator SetPlayerUI()
        {
            while (!photonView.Owner.CustomProperties.ContainsKey("playerName")
                   ||!photonView.Owner.CustomProperties.ContainsKey("team"))
            {
                yield return 0;
            }

            if (playerNameText != null)
            {
                Player player = photonView.Owner;
                playerName = (string)player.CustomProperties["playerName"];
                playerNameText.text = playerName;
                switch ((int)player.CustomProperties["team"])
                {
                    case 0:
                        playerNameText.color = Color.red;
                        break;
                    case 1:
                        playerNameText.color = Color.blue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}