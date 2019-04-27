using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

	public event System.Action<PlayerScript> OnLocalPlayerJoined;
	private GameObject gameobject;

	// Sigelton
	private static GameManager m_Instance;
	public static GameManager Instance {
		get {
			if (m_Instance == null) {
				
				m_Instance = new GameManager();
				m_Instance.gameobject = new GameObject("_gameManager");
				m_Instance.gameobject.AddComponent<InputController>();
                m_Instance.gameobject.AddComponent<Timer>();
                m_Instance.gameobject.AddComponent<Respawner>();
			}
			return m_Instance;
		}
	}

	private InputController m_InputController;
	public InputController InputController{
		get {
			if (m_InputController == null) {
				m_InputController = gameobject.GetComponent<InputController>();

			}
			return m_InputController;
		}	
	}

    private Timer m_timer;
    public Timer Timer
    {
        get
        {
            if (m_timer == null)
                m_timer = gameobject.GetComponent<Timer>();
            return m_timer;
        }
    }

    private Respawner m_respawner;
    public Respawner Respawner
    {
        get
        {
            if (m_respawner == null)
                m_respawner = gameobject.GetComponent<Respawner>();
            return m_respawner;
        }
    }

    private PlayerScript m_Loacal_Player;
	public PlayerScript LocalPlayer {
		get {
			return m_Loacal_Player;
		}
		set {
			m_Loacal_Player = value;
            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(m_Loacal_Player);
		}
	}
}
