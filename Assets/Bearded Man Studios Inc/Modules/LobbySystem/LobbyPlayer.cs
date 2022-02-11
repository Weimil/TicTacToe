using UnityEngine;
using System.Collections;
using System;

namespace BeardedManStudios.Forge.Networking.Unity.Lobby
{
    public class LobbyPlayer : IClientMockPlayer
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private uint _networkID;

        public uint NetworkId
        {
            get => _networkID;

            set => _networkID = value;
        }

        private int _avatarID;

        public int AvatarID
        {
            get => _avatarID;
            set => _avatarID = value;
        }

        private int _teamID;

        public int TeamID
        {
            get => _teamID;
            set => _teamID = value;
        }

        private bool _created;

        public bool Created
        {
            get => _created;
            set => _created = value;
        }
    }
}