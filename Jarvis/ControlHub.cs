﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Jarvis.Core.Device;

namespace Jarvis
{
    public class ControlHub : Hub
    {
        public ControlHub()
        {
            Service.Events.Instance.NewCommandReceived += Instance_NewCommandReceived;
        }

        void Instance_NewCommandReceived(object sender, Service.CommandEventArgs e)
        {
            Clients.All.OnCommand(e.Command);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {   
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public void OnCommandCompleted(Service.Impl.JarvisResponse response) {
            Service.JarvisService.TranslateResponse(response);
            //if (response.StatusCode == Service.ResponseCodes.DEVICE_LIST)
            //{
            //    // TODO: Store device lists 
            //}
            //else
            //{
            //    Service.Jarvis.TranslateResponse(response);
            //}
            
        }

        public void Init(Device[] devices)
        {
            try
            {
                DeviceRegistry.Instance.Store("", devices);
            }
            catch (Exception ex)
            {

            }
        }

        public void Setup(object[] devices)
        {

        }
    }
}