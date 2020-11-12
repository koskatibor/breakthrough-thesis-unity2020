using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.menu_scripts.play_scripts
{
		public class AIVsAI : MonoBehaviour
		{
				public TextMesh AIVsAIText;

				Color OriginalColor = new Color(255.0f, 255.0f, 255.0f);
				Color OnMouseOverColor = new Color(0.0f, 0.0f, 0.0f);

				private void OnMouseOver()
				{
						AIVsAIText.color = OnMouseOverColor;
						if (Input.GetMouseButtonDown(0))
						{
								SceneManager.LoadScene("AIVSAI", LoadSceneMode.Single);
						}
				}

				private void OnMouseExit()
				{
						AIVsAIText.color = OriginalColor;
				}
		}
}
