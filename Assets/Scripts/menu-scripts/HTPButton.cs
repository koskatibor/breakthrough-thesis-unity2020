using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.menu_scripts
{
		class HTPButton : MonoBehaviour
		{
				public TextMesh HTPText;

				Color OriginalColor = new Color(255.0f, 255.0f, 255.0f);
				Color OnMouseOverColor = new Color(0.0f, 0.0f, 0.0f);

				private void OnMouseOver()
				{
						HTPText.color = OnMouseOverColor;
				}

				private void OnMouseExit()
				{
						HTPText.color = OriginalColor;
				}
		}
}
