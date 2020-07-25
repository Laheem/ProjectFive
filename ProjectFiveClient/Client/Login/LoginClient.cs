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
            setLoginCamera();

        }


        public void destroyLoginBrowser(object[] args)
        {
            attachCameraBackToPlayer();
            if(browser != null)
            {
                browser.Destroy();
            }
        }

        public void setLoginCamera()
        {
            loginCamera = RAGE.Game.Cam.CreateCameraWithParams(01, -587.90314f, 712.3231f, 187.77089f, 180, 180, 160, 2, false, 0);
            RAGE.Game.Cam.SetCamActive(loginCamera, true);
            RAGE.Game.Cam.SetCamFov(loginCamera, 5.0f);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, true, loginCamera);

        }

        public void attachCameraBackToPlayer()
        {
            RAGE.Game.Cam.SetCamActive(loginCamera, false);
            RAGE.Game.Cam.DestroyCam(loginCamera, false);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, true, 0);
        }
    }
}
