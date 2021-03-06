using BeardedManStudios.Forge.Networking.Generated;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Unity
{
    public partial class NetworkManager : MonoBehaviour
    {
        public delegate void InstantiateEvent(INetworkBehavior unityGameObject, NetworkObject obj);

        public event InstantiateEvent objectInitialized;
        protected BMSByte metadata = new BMSByte();

        public GameObject[] ChatManagerNetworkObject = null;
        public GameObject[] CubeForgeGameNetworkObject = null;
        public GameObject[] ExampleProximityPlayerNetworkObject = null;
        public GameObject[] GameNetworkNetworkObject = null;
        public GameObject[] NetBoardManagerNetworkObject = null;
        public GameObject[] NetworkCameraNetworkObject = null;
        public GameObject[] TestNetworkObject = null;

        protected virtual void SetupObjectCreatedEvent()
        {
            Networker.objectCreated += CaptureObjects;
        }

        protected virtual void OnDestroy()
        {
            if (Networker != null)
                Networker.objectCreated -= CaptureObjects;
        }

        private void CaptureObjects(NetworkObject obj)
        {
            if (obj.CreateCode < 0)
                return;

            if (obj is ChatManagerNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (ChatManagerNetworkObject.Length > 0 && ChatManagerNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(ChatManagerNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<ChatManagerBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is CubeForgeGameNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (CubeForgeGameNetworkObject.Length > 0 && CubeForgeGameNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(CubeForgeGameNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<CubeForgeGameBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is ExampleProximityPlayerNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (ExampleProximityPlayerNetworkObject.Length > 0 &&
                            ExampleProximityPlayerNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(ExampleProximityPlayerNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<ExampleProximityPlayerBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is GameNetworkNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (GameNetworkNetworkObject.Length > 0 && GameNetworkNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(GameNetworkNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<GameNetworkBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is NetBoardManagerNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (NetBoardManagerNetworkObject.Length > 0 &&
                            NetBoardManagerNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(NetBoardManagerNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<NetBoardManagerBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is NetworkCameraNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (NetworkCameraNetworkObject.Length > 0 && NetworkCameraNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(NetworkCameraNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<NetworkCameraBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
            else if (obj is TestNetworkObject)
                MainThreadManager.Run(() =>
                {
                    NetworkBehavior newObj = null;
                    if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
                        if (TestNetworkObject.Length > 0 && TestNetworkObject[obj.CreateCode] != null)
                        {
                            GameObject go = Instantiate(TestNetworkObject[obj.CreateCode]);
                            newObj = go.GetComponent<TestBehavior>();
                        }

                    if (newObj == null)
                        return;

                    newObj.Initialize(obj);

                    if (objectInitialized != null)
                        objectInitialized(newObj, obj);
                });
        }

        protected virtual void InitializedObject(INetworkBehavior behavior, NetworkObject obj)
        {
            if (objectInitialized != null)
                objectInitialized(behavior, obj);

            obj.pendingInitialized -= InitializedObject;
        }

        [Obsolete("Use InstantiateChatManager instead, its shorter and easier to type out ;)")]
        public ChatManagerBehavior InstantiateChatManagerNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(ChatManagerNetworkObject[index]);
            ChatManagerBehavior netBehavior = go.GetComponent<ChatManagerBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<ChatManagerBehavior>().networkObject = (ChatManagerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateCubeForgeGame instead, its shorter and easier to type out ;)")]
        public CubeForgeGameBehavior InstantiateCubeForgeGameNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(CubeForgeGameNetworkObject[index]);
            CubeForgeGameBehavior netBehavior = go.GetComponent<CubeForgeGameBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<CubeForgeGameBehavior>().networkObject = (CubeForgeGameNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateExampleProximityPlayer instead, its shorter and easier to type out ;)")]
        public ExampleProximityPlayerBehavior InstantiateExampleProximityPlayerNetworkObject(int index = 0,
            Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(ExampleProximityPlayerNetworkObject[index]);
            ExampleProximityPlayerBehavior netBehavior = go.GetComponent<ExampleProximityPlayerBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<ExampleProximityPlayerBehavior>().networkObject = (ExampleProximityPlayerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateGameNetwork instead, its shorter and easier to type out ;)")]
        public GameNetworkBehavior InstantiateGameNetworkNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(GameNetworkNetworkObject[index]);
            GameNetworkBehavior netBehavior = go.GetComponent<GameNetworkBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<GameNetworkBehavior>().networkObject = (GameNetworkNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateNetBoardManager instead, its shorter and easier to type out ;)")]
        public NetBoardManagerBehavior InstantiateNetBoardManagerNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(NetBoardManagerNetworkObject[index]);
            NetBoardManagerBehavior netBehavior = go.GetComponent<NetBoardManagerBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<NetBoardManagerBehavior>().networkObject = (NetBoardManagerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateNetworkCamera instead, its shorter and easier to type out ;)")]
        public NetworkCameraBehavior InstantiateNetworkCameraNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(NetworkCameraNetworkObject[index]);
            NetworkCameraBehavior netBehavior = go.GetComponent<NetworkCameraBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<NetworkCameraBehavior>().networkObject = (NetworkCameraNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        [Obsolete("Use InstantiateTest instead, its shorter and easier to type out ;)")]
        public TestBehavior InstantiateTestNetworkObject(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(TestNetworkObject[index]);
            TestBehavior netBehavior = go.GetComponent<TestBehavior>();
            NetworkObject obj = netBehavior.CreateNetworkObject(Networker, index);
            go.GetComponent<TestBehavior>().networkObject = (TestNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of ChatManager
        /// </summary>
        /// <returns>
        /// A local instance of ChatManagerBehavior
        /// </returns>
        /// <param name="index">The index of the ChatManager prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public ChatManagerBehavior InstantiateChatManager(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(ChatManagerNetworkObject[index]);
            ChatManagerBehavior netBehavior = go.GetComponent<ChatManagerBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<ChatManagerBehavior>().networkObject = (ChatManagerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of CubeForgeGame
        /// </summary>
        /// <returns>
        /// A local instance of CubeForgeGameBehavior
        /// </returns>
        /// <param name="index">The index of the CubeForgeGame prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public CubeForgeGameBehavior InstantiateCubeForgeGame(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(CubeForgeGameNetworkObject[index]);
            CubeForgeGameBehavior netBehavior = go.GetComponent<CubeForgeGameBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<CubeForgeGameBehavior>().networkObject = (CubeForgeGameNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of ExampleProximityPlayer
        /// </summary>
        /// <returns>
        /// A local instance of ExampleProximityPlayerBehavior
        /// </returns>
        /// <param name="index">The index of the ExampleProximityPlayer prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public ExampleProximityPlayerBehavior InstantiateExampleProximityPlayer(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(ExampleProximityPlayerNetworkObject[index]);
            ExampleProximityPlayerBehavior netBehavior = go.GetComponent<ExampleProximityPlayerBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<ExampleProximityPlayerBehavior>().networkObject = (ExampleProximityPlayerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of GameNetwork
        /// </summary>
        /// <returns>
        /// A local instance of GameNetworkBehavior
        /// </returns>
        /// <param name="index">The index of the GameNetwork prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public GameNetworkBehavior InstantiateGameNetwork(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(GameNetworkNetworkObject[index]);
            GameNetworkBehavior netBehavior = go.GetComponent<GameNetworkBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<GameNetworkBehavior>().networkObject = (GameNetworkNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of NetBoardManager
        /// </summary>
        /// <returns>
        /// A local instance of NetBoardManagerBehavior
        /// </returns>
        /// <param name="index">The index of the NetBoardManager prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public NetBoardManagerBehavior InstantiateNetBoardManager(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(NetBoardManagerNetworkObject[index]);
            NetBoardManagerBehavior netBehavior = go.GetComponent<NetBoardManagerBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<NetBoardManagerBehavior>().networkObject = (NetBoardManagerNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of NetworkCamera
        /// </summary>
        /// <returns>
        /// A local instance of NetworkCameraBehavior
        /// </returns>
        /// <param name="index">The index of the NetworkCamera prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public NetworkCameraBehavior InstantiateNetworkCamera(int index = 0, Vector3? position = null,
            Quaternion? rotation = null, bool sendTransform = true)
        {
            GameObject go = Instantiate(NetworkCameraNetworkObject[index]);
            NetworkCameraBehavior netBehavior = go.GetComponent<NetworkCameraBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<NetworkCameraBehavior>().networkObject = (NetworkCameraNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }

        /// <summary>
        /// Instantiate an instance of Test
        /// </summary>
        /// <returns>
        /// A local instance of TestBehavior
        /// </returns>
        /// <param name="index">The index of the Test prefab in the NetworkManager to Instantiate</param>
        /// <param name="position">Optional parameter which defines the position of the created GameObject</param>
        /// <param name="rotation">Optional parameter which defines the rotation of the created GameObject</param>
        /// <param name="sendTransform">Optional Parameter to send transform data to other connected clients on Instantiation</param>
        public TestBehavior InstantiateTest(int index = 0, Vector3? position = null, Quaternion? rotation = null,
            bool sendTransform = true)
        {
            GameObject go = Instantiate(TestNetworkObject[index]);
            TestBehavior netBehavior = go.GetComponent<TestBehavior>();

            NetworkObject obj = null;
            if (!sendTransform && position == null && rotation == null)
            {
                obj = netBehavior.CreateNetworkObject(Networker, index);
            }
            else
            {
                metadata.Clear();

                if (position == null && rotation == null)
                {
                    byte transformFlags = 0x1 | 0x2;
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);
                    ObjectMapper.Instance.MapBytes(metadata, go.transform.position, go.transform.rotation);
                }
                else
                {
                    byte transformFlags = 0x0;
                    transformFlags |= (byte) (position != null ? 0x1 : 0x0);
                    transformFlags |= (byte) (rotation != null ? 0x2 : 0x0);
                    ObjectMapper.Instance.MapBytes(metadata, transformFlags);

                    if (position != null)
                        ObjectMapper.Instance.MapBytes(metadata, position.Value);

                    if (rotation != null)
                        ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
                }

                obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
            }

            go.GetComponent<TestBehavior>().networkObject = (TestNetworkObject) obj;

            FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);

            return netBehavior;
        }
    }
}