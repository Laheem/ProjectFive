using GTANetworkAPI;
using ProjectFive.AccountManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.CharacterManager.Dto
{
    class CharacterLogInArgs : EventArgs
    {
        public Player client;
        public Character targetCharacter;
        public Account targetAccount;


        public CharacterLogInArgs(Player client, Character targetCharacter, Account targetAccount)
        {
            this.client = client;
            this.targetCharacter = targetCharacter;
            this.targetAccount = targetAccount;
        }
    }
}
