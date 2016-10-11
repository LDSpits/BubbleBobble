using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

namespace CustomLibrary.Timers
{
    public class GoodTimer : UnityEngine.Object
    {
        float elapsedSeconds = 0;
        float _seconds;
        Component _comp;
        string _method;

        public GoodTimer(Component thisComponent, float seconds, string method)
        {
            _comp = thisComponent;
            _method = method;
            _seconds = seconds;
        }

        void Update()
        {
            elapsedSeconds += Time.deltaTime;
            if(elapsedSeconds > _seconds) {
                _comp.gameObject.GetComponent<MonoBehaviour>().Invoke(_method, 0);
            }
        }

        public void Start(float seconds)
        {
           
        }

        public void Stop()
        {

        }
    }
}
