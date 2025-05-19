using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public Transform actionBarParent;
    public GameObject actionBarSlotPrefab;
    public GameObject winScreen;
    public GameObject loseScreen;

    private List<Figure> actionBar = new List<Figure>();
    public bool IsGameActive { get; private set; } = true;

    private void Awake() {
        Instance = this;
    }

    public void OnFigureClicked(Figure figure) {
        if (!IsGameActive) return;

        if (actionBar.Count >= 7) {
            LoseGame();
            return;
        }

        figure.GetComponent<Collider2D>().enabled = false;
        actionBar.Add(figure);
        figure.transform.SetParent(actionBarParent);
        figure.transform.localPosition = new Vector3(actionBar.Count * 80, 0, 0); // Позиция в ActionBar

        CheckForTriplet();
    }

    private void CheckForTriplet() {
        if (actionBar.Count < 3) return;

        for (int i = 0; i < actionBar.Count - 2; i++) {
            if (actionBar[i].data.IsSame(actionBar[i + 1].data) &&
                actionBar[i].data.IsSame(actionBar[i + 2].data)) {
                // Удаляем тройку
                Destroy(actionBar[i].gameObject);
                Destroy(actionBar[i + 1].gameObject);
                Destroy(actionBar[i + 2].gameObject);

                actionBar.RemoveAt(i + 2);
                actionBar.RemoveAt(i + 1);
                actionBar.RemoveAt(i);

                RearrangeActionBar();
                CheckVictory();
                return;
            }
        }

        if (actionBar.Count >= 7) {
            LoseGame();
        }
    }

    private void RearrangeActionBar() {
        for (int i = 0; i < actionBar.Count; i++) {
            actionBar[i].transform.localPosition = new Vector3(i * 80, 0, 0);
        }
    }

    private void CheckVictory() {
        if (FindObjectsOfType<Figure>().Length == actionBar.Count) {
            WinGame();
        }
    }

    private void WinGame() {
        IsGameActive = false;
        winScreen.SetActive(true);
    }

    private void LoseGame() {
        IsGameActive = false;
        loseScreen.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}