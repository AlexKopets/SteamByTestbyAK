using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameObject figurePrefab;
    public Transform spawnArea;
    public int totalFigures = 30;

    public List<Sprite> shapes;
    public List<Color> borderColors;
    public List<Sprite> animals;

    private void Start() {
        GenerateFigures();
    }

    public void GenerateFigures() {
        List<FigureData> pool = new List<FigureData>();

        // Создаем по 3 экземпляра каждого уникального сочетания
        int types = totalFigures / 3;
        for (int i = 0; i < types; i++) {
            FigureData fd = new FigureData {
                shape = (Shape)Random.Range(0, 3),
                borderColor = Random.ColorHSV(),
                animal = (AnimalType)Random.Range(0, 3)
            };

            for (int j = 0; j < 3; j++) {
                pool.Add(fd);
            }
        }

        pool = Shuffle(pool);

        StartCoroutine(DropFigures(pool));
    }

    private IEnumerator DropFigures(List<FigureData> pool) {
        foreach (var data in pool) {
            Vector3 dropPos = new Vector3(Random.Range(-3f, 3f), 6f, 0f);
            GameObject figObj = Instantiate(figurePrefab, dropPos, Quaternion.identity);
            Figure fig = figObj.GetComponent<Figure>();
            fig.data = data;
            // TODO: обновить визуал по data
            yield return new WaitForSeconds(0.1f);
        }
    }

    private List<FigureData> Shuffle(List<FigureData> list) {
        for (int i = 0; i < list.Count; i++) {
            var tmp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = tmp;
        }
        return list;
    }

    public void Reshuffle() {
        foreach (var fig in FindObjectsOfType<Figure>()) {
            Destroy(fig.gameObject);
        }
        GenerateFigures();
    }
}