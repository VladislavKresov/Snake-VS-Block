using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;
    [SerializeField] private Vector2Int _bonusRange;

    private int _bounsSize;

    private void Start()
    {
        _bounsSize = Random.Range(_bonusRange.x, _bonusRange.y);
        _view.text = _bounsSize.ToString();
    }

    public int Collect()
    {
        Destroy(gameObject);
        return _bounsSize;
    }
}
