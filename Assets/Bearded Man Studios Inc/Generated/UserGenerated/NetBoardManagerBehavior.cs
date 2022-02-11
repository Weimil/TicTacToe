using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
    [GeneratedRPC("{\"types\":[[\"int\"][\"int\"][\"string\"][]]")]
    [GeneratedRPCVariableNames("{\"types\":[[\"sprite\"][\"id\"][\"id\"][]]")]
    public abstract partial class NetBoardManagerBehavior : NetworkBehavior
    {
        public const byte RPC_SET_SPRITE = 0 + 5;
        public const byte RPC_SET_ID_PLAYER = 1 + 5;
        public const byte RPC_CHECK_ME_THIS_ONE = 2 + 5;
        public const byte RPC_GAME_OVER = 3 + 5;

        public NetBoardManagerNetworkObject networkObject = null;

        public override void Initialize(NetworkObject obj)
        {
            // We have already initialized this object
            if (networkObject != null && networkObject.AttachedBehavior != null)
                return;

            networkObject = (NetBoardManagerNetworkObject) obj;
            networkObject.AttachedBehavior = this;

            SetupHelperRpcs(networkObject);
            networkObject.RegisterRpc("SetSprite", SetSprite, typeof(int));
            networkObject.RegisterRpc("SetIdPlayer", SetIdPlayer, typeof(int));
            networkObject.RegisterRpc("CheckMeThisOne", CheckMeThisOne, typeof(string));
            networkObject.RegisterRpc("GameOver", GameOver);

            networkObject.onDestroy += DestroyGameObject;

            if (!obj.IsOwner)
            {
                if (!skipAttachIds.ContainsKey(obj.NetworkId))
                {
                    uint newId = obj.NetworkId + 1;
                    ProcessOthers(gameObject.transform, ref newId);
                }
                else
                {
                    skipAttachIds.Remove(obj.NetworkId);
                }
            }

            if (obj.Metadata != null)
            {
                byte transformFlags = obj.Metadata[0];

                if (transformFlags != 0)
                {
                    BMSByte metadataTransform = new BMSByte();
                    metadataTransform.Clone(obj.Metadata);
                    metadataTransform.MoveStartIndex(1);

                    if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
                        MainThreadManager.Run(() =>
                        {
                            transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
                            transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
                        });
                    else if ((transformFlags & 0x01) != 0)
                        MainThreadManager.Run(() =>
                        {
                            transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
                        });
                    else if ((transformFlags & 0x02) != 0)
                        MainThreadManager.Run(() =>
                        {
                            transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
                        });
                }
            }

            MainThreadManager.Run(() =>
            {
                NetworkStart();
                networkObject.Networker.FlushCreateActions(networkObject);
            });
        }

        protected override void CompleteRegistration()
        {
            base.CompleteRegistration();
            networkObject.ReleaseCreateBuffer();
        }

        public override void Initialize(NetWorker networker, byte[] metadata = null)
        {
            Initialize(new NetBoardManagerNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
        }

        private void DestroyGameObject(NetWorker sender)
        {
            MainThreadManager.Run(() =>
            {
                try
                {
                    Destroy(gameObject);
                }
                catch
                {
                }
            });
            networkObject.onDestroy -= DestroyGameObject;
        }

        public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
        {
            return new NetBoardManagerNetworkObject(networker, this, createCode, metadata);
        }

        protected override void InitializedTransform()
        {
            networkObject.SnapInterpolations();
        }

        /// <summary>
        /// Arguments:
        /// int sprite
        /// </summary>
        public abstract void SetSprite(RpcArgs args);

        /// <summary>
        /// Arguments:
        /// int id
        /// </summary>
        public abstract void SetIdPlayer(RpcArgs args);

        /// <summary>
        /// Arguments:
        /// string id
        /// </summary>
        public abstract void CheckMeThisOne(RpcArgs args);

        /// <summary>
        /// Arguments:
        /// </summary>
        public abstract void GameOver(RpcArgs args);

        // DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
    }
}