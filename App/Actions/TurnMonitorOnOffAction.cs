using App.MotionDetector;
using Dear;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Actions {
    public class TurnMonitorOnOffAction {

        public static int TimeoutInMinutes = 30;
        private DateTime _lastMovement = DateTime.Now;
        private MrWindows _win = new MrWindows();
        private bool _monitorOn = true;

        public async Task Start() {
            while(true) {
                await Task.Delay(1000);
                if(!_monitorOn) {
                    return;
                }
                if((DateTime.Now - _lastMovement).TotalMinutes > TimeoutInMinutes) {
                    //_win.Screen.TurnOff();
                    Debug.WriteLine("Monitor OFF");
                    _monitorOn = false;
                }
            }
        }

        public void Proccess(MotionDetected message) {
            _lastMovement = DateTime.Now;
            _win.Screen.TurnOn();
            _monitorOn = true;
            Debug.WriteLine("Monitor ON");
        }
    }
}
