// Assets/Plugins/WebGL/mqtt.jslib
mergeInto(LibraryManager.library, (function ()
{
	const g_Cores = new Map();
	const g_TextDecoder = new TextDecoder("utf-8");
	const g_TextEncoder = new TextEncoder();


	// 바이트 배열을 문자열로 변환.
	function ByteArrayToString(bytes)
	{
		let str = "";
		for (let i = 0; i < bytes.length; i++)
		{
			str += String.fromCharCode(bytes[i]);
		}

		const text = btoa(str);
		return text;
	}

	// 문자열을 바이트 배열로 변환.
	function StringToByteArray(text)
	{
		const str = atob(text);
		const bytes = new Uint8Array(str.length);
		for (let i = 0; i < str.length; i++)
		{
			bytes[i] = str.charCodeAt(i);
		}

		return bytes;
	}

	// 바이트배열을 UTF8 문자열로 변환.
	function BytesToUTF8(bytes)
	{
		return g_TextDecoder.decode(bytes);
	}

	// UTF8 문자열을 바이트 배열로 변환.
	function UTF8ToBytes(str)
	{
		return g_TextEncoder.encode(str);
	}

	// 코어 생성.
	function CORE_Create(coreId)
	{
		const core =
		{
			client: null,
			connected: false
		};

		g_Cores.set(coreId, core);
		return core;
	}

	// 코어 반환.
	function CORE_Get(coreId)
	{
		const core = g_Cores.get(coreId);
		return core;
	}

	// 코어 파괴.
	function CORE_Destroy(coreId)
	{
		const core = CORE_Get(coreId);
		if (!core)
		{
			return;
		}

		try
		{
			if (core.client)
			{
				core.client.end(true);
			}
		}
		catch (e)
		{
		}

		g_Cores.delete(coreId);
	}

	// [JS ==> C#] MQTT_Connected 함수 호출.
	function MQTT_Connected(coreId)
	{
		SendMessage(coreId, "MQTT_Connected", "");
	}

	// [JS ==> C#] MQTT_Disconnected 함수 호출.
	function MQTT_Disconnected(coreId)
	{
		SendMessage(coreId, "MQTT_Disconnected", "");
	}

	// [JS ==> C#] MQTT_Received 함수 호출.
	function MQTT_Received(coreId, json)
	{
		SendMessage(coreId, "MQTT_Received", json);
	}

	// [JS ==> C#] MQTT_Published 함수 호출.
	function MQTT_Published(coreId, json)
	{
		SendMessage(coreId, "MQTT_Published", json);
	}

	// [JS ==> C#] MQTT_Subscribed 함수 호출.
	function MQTT_Subscribed(coreId, topic)
	{
		SendMessage(coreId, "MQTT_Subscribed", topic);
	}

	// [JS ==> C#] MQTT_Unsubscribed 함수 호출.
	function MQTT_Unsubscribed(coreId, topic)
	{
		SendMessage(coreId, "MQTT_Unsubscribed", topic);
	}

	// C#에서 호출하는 함수 목록.
	return
	{
		// [C# ==> JS] 브로커 접속 여부 반환.
		MQTT_IsConnected: function (coreIdPtr)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const core = CORE_Get(coreId);
			return (core && core.connected) ? 1 : 0;
		},

		// [C# ==> JS] 브로커 접속.
		MQTT_Connect: function (coreIdPtr, urlPtr, clientIdPtr)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const url = UTF8ToString(urlPtr);
			const clientId = UTF8ToString(clientIdPtr);

			// JS에서 MQTT 라이브러리 없음.
			if (typeof mqtt === "undefined")
			{
				MQTT_Disconnected(coreId);
				return;
			}

			// 코어 재생성.
			CORE_Destroy(coreId);
			const core = CORE_Create(coreId);

			try
			{
				const options =
				{
					clientId: clientId,
					clean: true,
					reconnectPeriod: 1000
				};

				// 접속.
				core.client = mqtt.connect(url, options);

				// 접속됨.
				core.client.on("connect", function ()
				{
					core.connected = true;
					MQTT_Connected(coreId);
				});

				// 접속 해제됨.
				core.client.on("close", function ()
				{
					core.connected = false;
					MQTT_Disconnected(coreId);
				});

				// 오류 발생됨.
				core.client.on("error", function (_)
				{
				});

				// 메시지 수신됨.
				core.client.on("message", function (topic, payload, packet)
				{
					const bytes = (payload instanceof Uint8Array) ? payload : new Uint8Array(payload);

					// 바이트를 문자열로 변환.
					//const json = ByteArrayToString(bytes);
					const json = BytesToUTF8(bytes);

					//const qos = (packet && (packet.qos | 0)) || 0;
					//const retain = (packet && packet.retain) ? 1 : 0;
					//const dup = (packet && packet.dup) ? 1 : 0;
					//const message =
					//	topic + "|" +
					//	text + "|" +
					//	qos + "|" +
					//	retain + "|" +
					//	dup;

					MQTT_Received(coreId, json);
				});
			}
			catch (e)
			{
				core.connected = false;
				MQTT_Disconnected(coreId);
			}
		},

		// [C# ==> JS] 브로커 접속 해제.
		MQTT_Disconnect: function (coreIdPtr)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const core = CORE_Get(coreId);
			if (!core)
			{
				return;
			}

			try
			{
				if (core.client)
				{
					core.client.end(true);
				}
			}
			catch (e)
			{
			}

			core.client = null;
			core.connected = false;

			MQTT_Disconnected(coreId);
		},

		// [C# ==> JS] 토픽 구독.
		MQTT_Subscribe: function (coreIdPtr, topicPtr, qos)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const core = CORE_Get(coreId);
			if (!core || !core.client)
			{
				return;
			}

			const topic = UTF8ToString(topicPtr);
			core.client.subscribe(topic,
			{
				qos: (qos | 0)
			});

			MQTT_Subscribed(coreId, topic);
		},

		// [C# ==> JS] 토픽 구독 해제.
		MQTT_Unsubscribe: function (coreIdPtr, topicPtr)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const core = CORE_Get(coreId);
			if (!core || !core.client)
			{
				return;
			}

			const topic = UTF8ToString(topicPtr);
			core.client.unsubscribe(topic);
			MQTT_Unsubscribed(coreId, topic);
		},

		// [C# ==> JS] 메시지 발행.
		MQTT_Publish: function (coreIdPtr, topicPtr, jsonPtr, qos, retain)
		{
			const coreId = UTF8ToString(coreIdPtr);
			const core = CORE_Get(coreId);
			if (!core || !core.client)
			{
				return;
			}

			const topic = UTF8ToString(topicPtr);
			const json = UTF8ToString(jsonPtr);
			const bytes = UTF8ToBytes(json);

			core.client.publish(topic, bytes,
			{
				qos: (qos | 0),
				retain: !!retain
			});
		
			MQTT_Published(coreId, json);
		}
	};
})()));
