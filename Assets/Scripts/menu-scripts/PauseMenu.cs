using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
		public GameObject PauseMenuPanel;
		private GameObject ChessTable;
		private FieldController Controller;
		private GameObject ScriptHolder;		

		#region PauseMenuButtons
		public Button ResumeButton;
		public Button RestartButton;
		public Button HowToPlayButton;		
		public Button QuitButton;
		#endregion
		private void Start()
		{
				SetPauseMenuControls();
				ChessTable = GameObject.Find("chess-table");
				ScriptHolder = GameObject.Find("ScriptHolder");
				Controller = ChessTable.GetComponent<FieldController>();				
		}

		private void Update()
		{
				CheckForPauseMenu();
		}

		private void CheckForPauseMenu()
		{
				if (Input.GetKeyDown(KeyCode.Escape) && !(Controller.PlayerLostGame || Controller.AILostGame))
				{
						if (!PauseMenuPanel.activeSelf)
						{
								if (Time.timeScale != 0)
								{
										Time.timeScale = 0;
										PauseMenuPanel.SetActive(true);
								}								
						}
						else
						{
								PauseMenuPanel.SetActive(false);
								Time.timeScale = 1;
						}
				}
		}

		private void SetPauseMenuControls()
		{
				ResumeButton.onClick.AddListener(ResumeButtonAction);
				RestartButton.onClick.AddListener(RestartButtonAction);
				HowToPlayButton.onClick.AddListener(HowToPlayButtonAction);				
				QuitButton.onClick.AddListener(QuitButtonAction);
		}

		#region PauseMenuButtonActions
		public void ResumeButtonAction()
		{
				PauseMenuPanel.SetActive(false);
				Time.timeScale = 1;
		}

		public void RestartButtonAction()
		{
				Time.timeScale = 1;
				SceneManager.LoadScene("AIVSAI");
		}

		public void HowToPlayButtonAction()
		{
				PauseMenuPanel.SetActive(false);
				Time.timeScale = 1;
		}		

		public void QuitButtonAction()
		{
				Time.timeScale = 1;				
				SceneManager.LoadScene("Main");
		}
		#endregion
}
