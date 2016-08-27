using UnityEngine;
using System.Collections;

public class ArmorDamageTranslation : MonoBehaviour {

		
	public float DamageReduction(string damageType, string enemyArmorType, float damage){
		if (enemyArmorType.CompareTo ("Unarmored") == 0 || damageType.CompareTo ("Chaos") == 0) {
			return damage;
		}
			
		if (damageType.CompareTo ("Pierce") == 0) {
			if (enemyArmorType.CompareTo ("Medium") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Heavy") == 0) {
				return .8f * damage;
			}
			if (enemyArmorType.CompareTo ("Light") == 0) {
				return 1.3f * damage;
			}
			if (enemyArmorType.CompareTo ("Fortified") == 0) {
				return .7f * damage;
			}
		}
		if (damageType.CompareTo ("Normal") == 0) {
			if (enemyArmorType.CompareTo ("Medium") == 0) {
				return 1.2f * damage;
			}
			if (enemyArmorType.CompareTo ("Heavy") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Light") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Fortified") == 0) {
				return .8f * damage;
			}
		}
		if (damageType.CompareTo ("Magic") == 0) {
			if (enemyArmorType.CompareTo ("Medium") == 0) {
				return .8f * damage;
			}
			if (enemyArmorType.CompareTo ("Heavy") == 0) {
				return 1.2f * damage;
			}
			if (enemyArmorType.CompareTo ("Light") == 0) {
				return 1.1f * damage;
			}
			if (enemyArmorType.CompareTo ("Fortified") == 0) {
				return .7f * damage;
			}
		}
		if (damageType.CompareTo ("Siege") == 0) {
			if (enemyArmorType.CompareTo ("Medium") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Heavy") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Light") == 0) {
				return .9f * damage;
			}
			if (enemyArmorType.CompareTo ("Fortified") == 0) {
				return 1.25f * damage;
			}
		}


		return 0;
	}
}
