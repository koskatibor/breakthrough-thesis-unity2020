using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.menu_scripts
{
		public class PlayButton : MonoBehaviour
		{
				public GameObject MainMenuPanel;
				public GameObject PlayCanvasPanel;
						
				public TextMesh PlayText;				

				Color OriginalColor = new Color(255.0f, 255.0f, 255.0f);
				Color OnMouseOverColor = new Color(0.0f, 0.0f, 0.0f);
				
				private void OnMouseOver()
				{						
						PlayText.color = OnMouseOverColor;
						if (Input.GetMouseButtonDown(0))
						{
								//SceneManager.LoadScene("PlayerVSAI", LoadSceneMode.Single);
								MainMenuPanel.active = false;
								PlayCanvasPanel.active = true;
						}
				}

				private void OnMouseExit()
				{
						PlayText.color = OriginalColor;
				}

		}
}
