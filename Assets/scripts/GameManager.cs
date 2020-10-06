using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{
	public class GameManager : MonoBehaviour {
		private class History
		{
			public int start { get ;private set; }
			public int end { get ;private set; }

			public History(int start , int end){
				this.start = start;
				this.end = end;
			}
		}

		private ViewManager view;
		private TowerStack[] towers;
		private List<History> events;
		private bool beginLoop = false;
		private const float size = 0.65f;
		private const float stepTime = 0.5f;

		[SerializeField]private GameObject blockPrefab;

		void Start () {
			view = FindObjectOfType<ViewManager> ();
			view.onNewHanoiTower += OnNewHanoiTower;
			towers = FindObjectsOfType<TowerStack> ();
		}

		void OnNewHanoiTower () {
			events = new List<History> ();
			try{ iTween.Stop ();} catch{}
			ClearTowers ();
			view.ClearResult ();

			var blocksCount = view.GetBlocksCount ();
			CreateBlockInFirstTower (blocksCount);
			SolveHanoi(blocksCount, 0, 1, 2);
			beginLoop = true;
		}

		private void SolveHanoi(int n, int start, int auxiliary, int end) {
			if (n == 1) {
				Debug.Log(start + " -> " + end);
				events.Add(new History(start, end));
			} else {
				SolveHanoi(n - 1, start, end, auxiliary);
				Debug.Log(start + " -> " + end);
				events.Add(new History(start, end));
				SolveHanoi(n - 1, auxiliary, start, end);
			}
		}

		public void Update(){
			if (beginLoop) {
				StartCoroutine (DoMoving());
			}
		}

		IEnumerator DoMoving()
		{
			beginLoop = false;

			foreach (History his in events) {
				var block = towers [his.start].GetBlock ();
				Vector3 startUpperPlace = towers [his.start].GetUpperPlace ();
				Vector3 endUpperPlace = towers [his.end].GetUpperPlace ();
				Vector3 endVacancyPlace = towers [his.end].GetVacancyPlace ();
				towers [his.end].Add (block);

				iTween.MoveTo (block, startUpperPlace, stepTime);
				yield return new WaitForSeconds(stepTime);

				iTween.MoveTo (block, endUpperPlace, stepTime);
				yield return new WaitForSeconds(stepTime);

				iTween.MoveTo (block, endVacancyPlace, stepTime);
				yield return new WaitForSeconds(stepTime);

			}
			view.ShowResult (events.Count);

		}

		private void CreateBlockInFirstTower(int blocks){
			var blockYConstScale = blockPrefab.transform.localScale.y;
			var scale = new Vector3 (blocks*size, blockYConstScale , blocks*size);

			for (int i = 0; i < blocks; i++) {
				GameObject blockInstance = (GameObject)Instantiate (blockPrefab);
				blockInstance.transform.localScale = scale;
				scale.Set (scale.x - size, blockYConstScale, scale.z - size);
				blockInstance.transform.position = towers [0].GetVacancyPlace ();
				towers [0].Add (blockInstance);
			}
		}

		private void ClearTowers(){
			foreach (TowerStack tower in towers) {
				tower.GetComponent<TowerStack> ().Clear ();
			}
		}
	}

}

