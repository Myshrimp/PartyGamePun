using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ShrimpFPS.Interfaces
{
    public abstract class Subject
    {
        public List<IObserver> observers;

        public abstract void Register(IObserver obs);

        public void NotifyObservers(SubjectEvents evt)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify(evt);
            }
        }
            
    }

    public enum SubjectEvents
    {
        OnStartMultiplayer,OnConnectedToMaster,OnChoseATeam
    }
}