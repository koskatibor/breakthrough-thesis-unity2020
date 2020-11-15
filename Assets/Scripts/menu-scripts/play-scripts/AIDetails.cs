using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.menu_scripts.play_scripts
{
		public class AIDetails : MonoBehaviour
		{				
				public bool UseStepIgnore = false;
				public int MaxLevel = 2;
				public int MaxLevelWhite = 2;

				public int[] WhiteRowBonuses = new int[] { 1, 2, 4, 8, 16, 20, 24, 1000 };
				public int[] BlackRowBonuses = new int[] { 1, 2, 4, 8, 16, 20, 24, 1000 };

				public int WhiteSafeFieldBonus = 4;
				public int BlackSafeFieldBonus = 4;
				public int WhiteSafeFieldBonusOnSide = 4;
				public int BlackSafeFieldBonusOnSide = 4;

				public int WhiteRowFormationBonus = 8;
				public int BlackRowFormationBonus = 8;
				public int WhiteRowFormationBonusOnSide = 16;
				public int BlackRowFormationBonusOnSide = 16;

				public int WhiteColumnFormationBonus = 4;
				public int BlackColumnFormationBonus = 4;
				public int WhiteColumnFormationBonusOnSide = 6;
				public int BlackColumnFormationBonusOnSide = 6;

				public int WhiteDiagonalFormationBonus_SecondaryFormation = 4;
				public int BlackDiagonalFormationBonus_SecondaryFormation = 4;
				public int WhiteDiagonalFormationBonusOnSide_SecondaryFormation = 4;
				public int BlackDiagonalFormationBonusOnSide_SecondaryFormation = 4;

				public int WhitePenaltyForDangerousField = -20;
				public int BlackPenaltyForDangerousField = -20;
				public int WhitePenaltyForDangerousFieldOnSide = -24;
				public int BlackPenaltyForDangerousFieldOnSide = -24;

				public int WhitePawnInDangerDefenderBonus = 16;
				public int BlackPawnInDangerDefenderBonus = 16;
				public int WhitePawnInDangerDefenderBonusOnSide = 16;
				public int BlackPawnInDangerDefenderBonusOnSide = 16;
		}
}
