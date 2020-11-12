using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
		public GameObject WinPanel;
		public Color WhiteWinColor;
		public Color BlackWinColor;

		public Text WinText;

		private GameObject ChessTable;
		private FieldController Controller;

		// Start is called before the first frame update
		void Start()
    {
				ChessTable = GameObject.Find("chess-table");
				Controller = ChessTable.GetComponent<FieldController>();
		}

    // Update is called once per frame
    void Update()
    {
				if (Controller.AILostGame || Controller.PlayerLostGame)
				{
						WinPanel.SetActive(true);						
						if (Controller.AILostGame)
						{
								WinText.text = "White Won!";
						}
						else
						{
								if (Controller.PlayerLostGame)
								{
										WinText.text = "Black Won!";
								}
						}
						Time.timeScale = 0;
				}        
    }
}
