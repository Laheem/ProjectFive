using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFiveClient.Login
{
    class LoginClient : RAGE.Events.Script
    {
        private const String LOGIN_FILE_PATH = "package://cef/html/login.html";
        private int loginCamera;
        RAGE.Ui.HtmlWindow browser;

        LoginClient()
        {
            RAGE.Events.Add("loginSuccess", destroyLoginBrowser);
            RAGE.Events.Add("loginFailed", onLoginFailure);
            RAGE.Events.Add("login", createLoginBrowser);
            RAGE.Events.Add("attemptLogin", onPasswordSubmitted);
        }

        private void onPasswordSubmitted(object[] args)
        {
            RAGE.Events.CallRemote("attemptLogin", args[0]);
        }

        private void onLoginFailure(object[] args)
        {
            throw new NotImplementedException();
        }

        public void createLoginBrowser(object[] args)
        {
            RAGE.Chat.Output("It looks like we're getting here...");
            browser = new RAGE.Ui.HtmlWindow(LOGIN_FILE_PATH);
            browser.Active = true;
            RAGE.Ui.Cursor.Visible = true;
            Player.LocalPlayer.FreezePosition(true);

        }


        public void destroyLoginBrowser(object[] args)
        {
            attachCameraBackToPlayer();
            if(browser != null)
            {
                RAGE.Ui.Cursor.Visible = false;
                browser.Destroy();
            }
        }

        public void setLoginCamera()
        {
            loginCamera = RAGE.Game.Cam.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", -587.90314f, 712.3231f, 187.77089f, 180, 180, 160, 2, false, 1);
            RAGE.Game.Cam.SetCamActive(loginCamera, true);
            RAGE.Game.Cam.SetCamFov(loginCamera, 60.0f);
        }

        public void attachCameraBackToPlayer()
        {
            RAGE.Game.Cam.SetCamActive(loginCamera, false);
            RAGE.Game.Cam.DestroyCam(loginCamera, false);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, true, 0);
        }
    }
}
