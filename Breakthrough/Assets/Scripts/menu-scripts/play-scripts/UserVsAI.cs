using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.menu_scripts.play_scripts
{
		public class UserVsAI : MonoBehaviour
		{
				public TextMesh UserVsAIText;

				Color OriginalColor = new Color(255.0f, 255.0f, 255.0f);
				Color OnMouseOverColor = new Color(0.0f, 0.0f, 0.0f);

				private void OnMouseOver()
				{
						UserVsAIText.color = OnMouseOverColor;
						if (Input.GetMouseButtonDown(0))
						{
								SceneManager.LoadScene("PlayerVSAI", LoadSceneMode.Single);
						}
				}

				private void OnMouseExit()
				{
						UserVsAIText.color = OriginalColor;
				}
		}
}
