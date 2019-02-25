using BeardedManStudios.Forge.Networking.Generated;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Unity
{
	public partial class NetworkManager : MonoBehaviour
	{
		public delegate void InstantiateEvent(INetworkBehavior unityGameObject, NetworkObject obj);
		public event InstantiateEvent objectInitialized;
		private BMSByte metadata = new BMSByte();

		public GameObject[] PlayerCardsNetworkObject = null;

		private void SetupObjectCreatedEvent()
		{
			Networker.objectCreated += CaptureObjects;
		}

		private void OnDestroy()
		{
		    if (Networker != null)
				Networker.objectCreated -= CaptureObjects;
		}
		
		private void CaptureObjects(NetworkObject obj)
		{
			if (obj.CreateCode < 0)
				return;
				
			if (obj is PlayerCardsNetworkObject)
			{
				MainThreadManager.Run(() =>
				{
					NetworkBehavior newObj = null;
					if (!NetworkBehavior.skipAttachIds.TryGetValue(obj.NetworkId, out newObj))
					{
						if (PlayerCardsNetworkObject.Length > 0 && PlayerCardsNetworkObject[obj.CreateCode] != null)
						{
							var go = Instantiate(PlayerCardsNetworkObject[obj.CreateCode]);
							newObj = go.GetComponent<PlayerCardsBehavior>();
						}
					}

					if (newObj == null)
						return;
						
					newObj.Initialize(obj);

					if (objectInitialized != null)
						objectInitialized(newObj, obj);
				});
			}
		}

		private void InitializedObject(INetworkBehavior behavior, NetworkObject obj)
		{
			if (objectInitialized != null)
				objectInitialized(behavior, obj);

			obj.pendingInitialized -= InitializedObject;
		}

		[Obsolete("Use InstantiatePlayerCards instead, its shorter and easier to type out ;)")]
		public PlayerCardsBehavior InstantiatePlayerCardsNetworkObject(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(PlayerCardsNetworkObject[index]);
			var netBehavior = go.GetComponent<PlayerCardsBehavior>();
			var obj = netBehavior.CreateNetworkObject(Networker, index);
			go.GetComponent<PlayerCardsBehavior>().networkObject = (PlayerCardsNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}

		public PlayerCardsBehavior InstantiatePlayerCards(int index = 0, Vector3? position = null, Quaternion? rotation = null, bool sendTransform = true)
		{
			var go = Instantiate(PlayerCardsNetworkObject[index]);
			var netBehavior = go.GetComponent<PlayerCardsBehavior>();

			NetworkObject obj = null;
			if (!sendTransform && position == null && rotation == null)
				obj = netBehavior.CreateNetworkObject(Networker, index);
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
					transformFlags |= (byte)(position != null ? 0x1 : 0x0);
					transformFlags |= (byte)(rotation != null ? 0x2 : 0x0);
					ObjectMapper.Instance.MapBytes(metadata, transformFlags);

					if (position != null)
						ObjectMapper.Instance.MapBytes(metadata, position.Value);

					if (rotation != null)
						ObjectMapper.Instance.MapBytes(metadata, rotation.Value);
				}

				obj = netBehavior.CreateNetworkObject(Networker, index, metadata.CompressBytes());
			}

			go.GetComponent<PlayerCardsBehavior>().networkObject = (PlayerCardsNetworkObject)obj;

			FinalizeInitialization(go, netBehavior, obj, position, rotation, sendTransform);
			
			return netBehavior;
		}
	}
}
