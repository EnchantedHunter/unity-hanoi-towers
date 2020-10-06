using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
	public class ViewManager : MonoBehaviour {

		public event System.Action onNewHanoiTower = delegate { };

		[SerializeField]private Button beginHanoiTower;
		[SerializeField]private InputField blocksCount;
		[SerializeField]private Text result;

		private void Awake()
		{
			beginHanoiTower.onClick.AddListener(() => { onNewHanoiTower(); });
		}
		public int GetBlocksCount(){
			int count = 1;
			count = int.Parse(blocksCount.text.ToString());
			return count;
		}

		public void ShowResult(int iter){
			result.text = string.Format ("Решено за {0} итераций", iter);
		}

		public void ClearResult(){
			result.text = "";
		}
	}
}