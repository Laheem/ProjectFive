using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Login
{
    class LoginHandler : Script
    {

        LoginController loginController = new LoginController();

        [Command("login", SensitiveInfo = true)]
        public void LoginUser(Player player, String password)
        {
            loginController.SignInAccount(player, password);
        }
    }
}
